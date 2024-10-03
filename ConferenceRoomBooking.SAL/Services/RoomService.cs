using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.DTOs.Room;
using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.Common.Models.ServiceResult;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.SAL.Services.Abstractions;

namespace ConferenceRoomBooking.SAL.Services;

public class RoomService(IRoomRepository roomRepository, IServiceRepository serviceRepository, IMapper mapper) : IRoomService
{
    private const int START_WORK_HOUR = 6;
    private const int END_WORK_HOUR = 23; //TODO: add class with constants or config
    
    public async Task<ServiceValueResult<List<RoomDto>>> GetAvailableRoomsAsync(GetAvailableRoomsDto getAvailableRoomsDto)//TODO: add pagination
    {
        if (getAvailableRoomsDto.StartTime < DateTime.Now)
            return new ServiceValueResult<List<RoomDto>>(ResponseType.BadRequest, "The start time cannot be in the past.");
        
        
        if (getAvailableRoomsDto.StartTime.TimeOfDay < TimeSpan.FromHours(START_WORK_HOUR) 
            || getAvailableRoomsDto.EndTime.TimeOfDay > TimeSpan.FromHours(END_WORK_HOUR))
            return new ServiceValueResult<List<RoomDto>>(ResponseType.BadRequest, $"Rooms are available for booking from {TimeSpan.FromHours(START_WORK_HOUR)} to {TimeSpan.FromHours(END_WORK_HOUR)}");

        var rooms = await roomRepository.GetAvailableRoomsAsync(getAvailableRoomsDto.StartTime, 
            getAvailableRoomsDto.EndTime, getAvailableRoomsDto.Capacity);
    
        if (rooms.Count == 0)
            return new ServiceValueResult<List<RoomDto>>(new List<RoomDto>());


        return new ServiceValueResult<List<RoomDto>>(mapper.Map<List<RoomDto>>(rooms));
    }
    
    public async Task<ServiceValueResult<RoomDto>> CreateRoomAsync(CreateRoomDto createRoomDto)
    {
        var roomModel = mapper.Map<RoomModel>(createRoomDto);
        
        var availableServices = await serviceRepository.GetServicesByIdsAsync(createRoomDto.AvailableServicesIds);
        if (availableServices.Count != createRoomDto.AvailableServicesIds.Count)
            return new ServiceValueResult<RoomDto>(ResponseType.BadRequest, $"Invalid service ID.");

        roomModel.Id = new Guid();
        roomModel.AvailableServices = availableServices;
        var createdRoom = await roomRepository.AddAsync(roomModel);
        
        return new ServiceValueResult<RoomDto>(mapper.Map<RoomDto>(createdRoom));
    }

    public async Task<ServiceValueResult<RoomDto>> UpdateRoomAsync(RoomDto roomDto)
    {
        var roomModel = mapper.Map<RoomModel>(roomDto);
        
        var existingRoom = await roomRepository.GetByIdAsync(roomModel.Id);
        if(existingRoom == null)
            return new ServiceValueResult<RoomDto>(ResponseType.BadRequest, $"Invalid room ID.");

        var currentServiceIds = existingRoom.AvailableServices.Select(s => s.Id).ToHashSet();
        var newServicesIds = roomModel.AvailableServices
            .Where(s => !currentServiceIds.Contains(s.Id))
            .Select(s => s.Id)
            .ToList();
        
        if (newServicesIds.Any())
        {
            var availableServices = await serviceRepository.GetServicesByIdsAsync(newServicesIds);
            if (availableServices.Count != newServicesIds.Count)
                return new ServiceValueResult<RoomDto>(ResponseType.BadRequest, $"Invalid service ID.");
        }
        
        await roomRepository.UpdateAsync(roomModel);
        return new ServiceValueResult<RoomDto>(roomDto);
    }

    public async Task<ServiceResult> DeleteRoomAsync(Guid roomId)
    {
        await roomRepository.DeleteAsync(roomId);
        return new ServiceResult(ResponseType.Ok);
    }
}
