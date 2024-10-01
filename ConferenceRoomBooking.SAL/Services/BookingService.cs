using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.DTOs.Booking;
using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.Common.Models.ServiceResult;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.SAL.Services.Abstractions;

namespace ConferenceRoomBooking.SAL.Services;

public class BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, 
    IServiceRepository serviceRepository, IMapper mapper) : IBookingService
{
    public async Task<ServiceValueResult<BookingDto>> CreateBookingAsync(BookingDto bookingDto)
    {
        var bookingModel = mapper.Map<BookingModel>(bookingDto);
        var createdBooking = await bookingRepository.AddAsync(bookingModel);
        return new ServiceValueResult<BookingDto>(mapper.Map<BookingDto>(createdBooking));
    }
    
    public async Task<ServiceValueResult<BookingDto>> CreateBookingAsync(CreateBookingDto createBookingDto)
    {
        var startTime = createBookingDto.BookingDate.TimeOfDay;
        var endTime = startTime + createBookingDto.Duration;
        // Validations
        if (startTime < TimeSpan.FromHours(6) || endTime > TimeSpan.FromHours(22) || createBookingDto.Duration.TotalHours < 1)
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest);

        var roomModel = await roomRepository.GetByIdAsync(createBookingDto.RoomId);
        if (string.IsNullOrEmpty(roomModel.Name)) //TODO: need to test and check it
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest);

        var selectedServices = await serviceRepository.GetServicesByIdsAsync(createBookingDto.SelectedServiceIds);

        if (!selectedServices.All(selectedService => roomModel.AvailableServices.Any(s => s.Id == selectedService.Id)))
            return new ServiceValueResult<BookingDto>(ResponseType.BadRequest);
        
        var rentalCost = CalculateRentalCost(roomModel.BasePricePerHour, createBookingDto.BookingDate, createBookingDto.Duration);

        var bookingModel = mapper.Map<BookingModel>(createBookingDto);
        bookingModel.Id = new Guid();
        bookingModel.TotalPrice = rentalCost;
        var createdBooking = await bookingRepository.AddAsync(bookingModel);
        
        var createdBookingDto = mapper.Map<BookingDto>(createdBooking);
        return new ServiceValueResult<BookingDto>(createdBookingDto);
    }
    
    private decimal CalculateRentalCost(decimal basePrice, DateTime bookingDate, TimeSpan duration)
    {
        // Определяем начальное и конечное время бронирования
        var startTime = bookingDate.Date.Add(bookingDate.TimeOfDay);
        var endTime = startTime.Add(duration);
    
        var totalCost = 0.0m;

        // Используем массив для хранения диапазонов и коэффициентов
        var timeRanges = new (TimeSpan start, TimeSpan end, decimal multiplier)[]
        {
            (new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), 0.9m),
            (new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), 1.0m),
            (new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0), 1.15m),
            (new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), 1.0m),
            (new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0), 0.8m)
        };

        // Проходим по временным диапазонам и вычисляем стоимость
        foreach (var (start, end, multiplier) in timeRanges)
        {
            // Получаем начальное и конечное время текущего диапазона
            var rangeStart = bookingDate.Date.Add(start);
            var rangeEnd = bookingDate.Date.Add(end);

            // Определяем фактическое начальное и конечное время для расчетов
            var effectiveStart = startTime < rangeStart ? rangeStart : startTime;
            var effectiveEnd = endTime < rangeEnd ? endTime : rangeEnd;

            // Пропускаем итерацию, если бронирование выходит за пределы текущего временного диапазона
            if (effectiveStart < effectiveEnd)
            {
                totalCost += (decimal)(effectiveEnd - effectiveStart).TotalHours * basePrice * multiplier;
            }
        }

        return totalCost;
    }
}
