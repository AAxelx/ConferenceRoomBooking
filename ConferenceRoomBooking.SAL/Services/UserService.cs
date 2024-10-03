using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.DTOs.User;
using ConferenceRoomBooking.Common.Models.ServiceResult;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.SAL.Services.Abstractions;

namespace ConferenceRoomBooking.SAL.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<ServiceValueResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        var userModel = mapper.Map<UserModel>(createUserDto);
        userModel.Id = new Guid();
        var createdUserModel = await userRepository.AddAsync(userModel);
        
        return new ServiceValueResult<UserDto>(mapper.Map<UserDto>(createdUserModel));
    }
}
