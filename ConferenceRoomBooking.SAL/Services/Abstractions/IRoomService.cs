using ConferenceRoomBooking.Common.Models.DTOs;

namespace ConferenceRoomBooking.SAL.Services.Abstractions;

public interface IRoomService
{
    Task<RoomDto> CreateRoomAsync(RoomDto roomDto);
    Task<RoomDto> UpdateRoomAsync(RoomDto roomDto);
    Task DeleteRoomAsync(Guid roomId);
    Task<List<RoomDto>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity);
}