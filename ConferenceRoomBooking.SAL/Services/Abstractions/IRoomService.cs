using ConferenceRoomBooking.Common.Models.DTOs.Room;
using ConferenceRoomBooking.Common.Models.ServiceResult;

namespace ConferenceRoomBooking.SAL.Services.Abstractions;

public interface IRoomService
{
    Task<ServiceValueResult<RoomDto>> CreateRoomAsync(CreateRoomDto roomDto);
    Task<ServiceValueResult<RoomDto>> UpdateRoomAsync(RoomDto roomDto);
    Task<ServiceResult> DeleteRoomAsync(Guid roomId);
    Task<ServiceValueResult<List<RoomDto>>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity);
}