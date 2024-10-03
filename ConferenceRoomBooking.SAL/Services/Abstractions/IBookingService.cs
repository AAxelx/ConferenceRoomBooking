using ConferenceRoomBooking.Common.Models.DTOs.Booking;
using ConferenceRoomBooking.Common.Models.ServiceResult;

namespace ConferenceRoomBooking.SAL.Services.Abstractions;

public interface IBookingService
{
    Task<ServiceValueResult<BookingDto>> CreateBookingAsync(CreateBookingDto bookingDto);
}