using ConferenceRoomBooking.Common.Models;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;

public interface IBookingRepository : IRepository<BookingModel>
{
    Task<List<BookingModel>> GetBookingsByUserIdAsync(Guid userId);
    Task<List<BookingModel>> GetBookingsByRoomIdAsync(Guid roomId);
}