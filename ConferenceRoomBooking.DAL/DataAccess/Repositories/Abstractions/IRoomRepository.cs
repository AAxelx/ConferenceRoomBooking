using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.DAL.Entities;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;

public interface IRoomRepository : IRepository<RoomModel>
{
    Task<List<RoomModel>> GetAvailableRoomsAsync(DateTime startDateTime, DateTime endDateTime, int capacity);
}