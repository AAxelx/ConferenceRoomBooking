using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.PartialModels;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;

public interface IBookingRepository : IRepository<BookingModel>
{
    Task<List<RoomBookingsOnDateModel>> GetRoomBookingsOnDateAsync(Guid roomId, DateTime date);
}