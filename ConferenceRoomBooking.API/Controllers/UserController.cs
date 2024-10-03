using ConferenceRoomBooking.Common.Models.DTOs.User;
using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.SAL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
    {
        var result = await _userService.CreateUserAsync(userDto);
        if (result.ResponseType != ResponseType.Ok)
            return BadRequest(result.Message);

        return Ok(new { UserId = result.Value });
    }
}
