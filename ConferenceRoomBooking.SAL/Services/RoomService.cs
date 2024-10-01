using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.DTOs.Room;
using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.Common.Models.ServiceResult;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.SAL.Services.Abstractions;

namespace ConferenceRoomBooking.SAL.Services;

public class RoomService(IRoomRepository roomRepository, IMapper mapper) : IRoomService
{
    public async Task<ServiceValueResult<RoomDto>> CreateRoomAsync(CreateRoomDto createRoomDto)
    {
        var roomModel = mapper.Map<RoomModel>(createRoomDto);
        roomModel.Id = new Guid();
        var createdRoom = await roomRepository.AddAsync(roomModel);
        return new ServiceValueResult<RoomDto>(mapper.Map<RoomDto>(createdRoom));
    }

    public async Task<ServiceValueResult<RoomDto>> UpdateRoomAsync(RoomDto roomDto)
    {
        var roomModel = mapper.Map<RoomModel>(roomDto);
        await roomRepository.UpdateAsync(roomModel);
        return new ServiceValueResult<RoomDto>(roomDto);
    }

    public async Task<ServiceResult> DeleteRoomAsync(Guid roomId)
    {
        await roomRepository.DeleteAsync(roomId);
        return new ServiceResult(ResponseType.Ok);
    }

    public async Task<ServiceValueResult<List<RoomDto>>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity)
    {
        var rooms = await roomRepository.GetAvailableRoomsAsync(startTime, endTime, capacity);
        return new ServiceValueResult<List<RoomDto>>(mapper.Map<List<RoomDto>>(rooms));
    }
}
