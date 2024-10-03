using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.DTOs.Booking;
using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.Common.Models.ServiceResult;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.SAL.Services.Abstractions;

namespace ConferenceRoomBooking.SAL.Services;

public class BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, 
    IServiceRepository serviceRepository, IUserRepository userRepository, IMapper mapper) : IBookingService
{
    private const int START_WORK_HOUR = 6;
    private const int END_WORK_HOUR = 23; //TODO: add class with constants or config
    
    public async Task<ServiceValueResult<BookingDto>> CreateBookingAsync(CreateBookingDto createBookingDto)
    {
        var startTime = createBookingDto.BookingDate.TimeOfDay;
        var endTime = startTime + createBookingDto.Duration;
        // Validations
        var userModel = await userRepository.GetByIdAsync(createBookingDto.UserId);
        if (userModel == null)
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest, "User not found."); //TODO: we need authorization
        
        if (startTime < TimeSpan.FromHours(START_WORK_HOUR) || startTime > TimeSpan.FromHours(END_WORK_HOUR - 1)  //TODO: add manager layer for validations
                                                            || endTime > TimeSpan.FromHours(END_WORK_HOUR) 
                                                            || createBookingDto.Duration.TotalHours < 1)
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest, 
                $"Booking time must be between {TimeSpan.FromHours(START_WORK_HOUR)} to {TimeSpan.FromHours(END_WORK_HOUR - 1)}, and the minimum duration is one hour.");
        
        if (createBookingDto.BookingDate.Date == DateTime.Today && startTime < DateTime.UtcNow.TimeOfDay.Add(TimeSpan.FromHours(1)))
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest, 
                "Booking must be made at least one hour in advance.");

        var roomModel = await roomRepository.GetByIdAsync(createBookingDto.RoomId);
        if (roomModel == null)
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest, "Room not found.");

        var selectedServices = await serviceRepository.GetServicesByIdsAsync(createBookingDto.SelectedServiceIds);
        if (selectedServices.Count != createBookingDto.SelectedServiceIds.Count || 
            !selectedServices.All(selectedService => roomModel.AvailableServices.Any(s => s.Id == selectedService.Id)))
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest, 
                $"Invalid service ID or the selected service is not available for {roomModel.Name}.");
        
        var existingBookings = await bookingRepository.GetRoomBookingsOnDateAsync(createBookingDto.RoomId, 
            createBookingDto.BookingDate.Date);
        var conflicts = existingBookings.Where(b => 
            (createBookingDto.BookingDate < b.EndTime) && 
            (createBookingDto.BookingDate + createBookingDto.Duration > b.StartTime)).ToList();

        if (conflicts.Any())
        {
            var conflictMessages = conflicts.Select(b => 
                $"Room \"{roomModel.Name}\" is already booked from {b.StartTime} to {b.EndTime} (Booking ID: {b.Id}).");
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest, 
                $"The following conflicts were found: {string.Join(", ", conflictMessages)}");
        }
        
        var rentalCost = CalculateRentalCost(roomModel.BasePricePerHour, createBookingDto.BookingDate, createBookingDto.Duration, selectedServices.Select(s => s.Price).ToList());

        var bookingModel = mapper.Map<BookingModel>(createBookingDto);
        bookingModel.Id = new Guid();
        bookingModel.TotalPrice = rentalCost;
        bookingModel.StartTime = createBookingDto.BookingDate;
        bookingModel.EndTime = createBookingDto.BookingDate + createBookingDto.Duration;
        var createdBooking = await bookingRepository.AddAsync(bookingModel);
        
        var createdBookingDto = mapper.Map<BookingDto>(createdBooking);
        return new ServiceValueResult<BookingDto>(createdBookingDto);
    }
    
    public decimal CalculateRentalCost(decimal basePrice, DateTime bookingDate, TimeSpan duration, List<decimal> selectedServicePrices)
    {
        // Get the booking hour
        var bookingHour = bookingDate.Hour;

        // Determine the coefficient based on time
        var coefficient = 1.0m; // Base rate
        if (bookingHour >= 18 && bookingHour < 23)
        {
            coefficient = 0.8m; // 20% discount for evening hours
        }
        else if (bookingHour >= 6 && bookingHour < 9)
        {
            coefficient = 0.9m; // 10% discount for morning hours
        }
        else if (bookingHour >= 12 && bookingHour < 14)
        {
            coefficient = 1.15m; // 15% surcharge for peak hours
        }

        // Calculate the cost
        var selectedServiceTotalPrice = new decimal();
        if (selectedServicePrices.Any())
            selectedServiceTotalPrice = selectedServicePrices.Sum();
        
        var hourlyRate = basePrice * coefficient;
        var totalCost = hourlyRate * (decimal)duration.TotalHours + selectedServiceTotalPrice;
        return totalCost;
    }
}