using ConferenceRoomBooking.Common.Models.DTOs.User;
using ConferenceRoomBooking.Common.Models.ServiceResult;

namespace ConferenceRoomBooking.SAL.Services.Abstractions;

public interface IUserService
{
    Task<ServiceValueResult<UserDto>> CreateUserAsync(CreateUserDto userDto);
}