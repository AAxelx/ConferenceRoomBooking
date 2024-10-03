using ConferenceRoomBooking.Common.Models.DTOs.Room;
using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.SAL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : BaseController
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable([FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] int capacity)
    {
        var result = await _roomService.GetAvailableRoomsAsync(new GetAvailableRoomsDto
        {
            StartTime = startTime,
            EndTime = endTime,
            Capacity = capacity
        });
    
        if (result.ResponseType != ResponseType.Ok)
            return BadRequest(result.Message);

        return Ok(result.Value);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomDto roomDto)
    {
        var result = await _roomService.CreateRoomAsync(roomDto);
        if (result.ResponseType != ResponseType.Ok)
            return BadRequest(result.Message);

        return Ok(new { RoomId = result.Value });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RoomDto roomDto)
    {
        var result = await _roomService.UpdateRoomAsync(roomDto);
        if (result.ResponseType != ResponseType.Ok)
            return BadRequest(result.Message);

        return Ok();
    }

    [HttpDelete("{roomId}")]
    public async Task<IActionResult> Delete(Guid roomId)
    {
        var result = await _roomService.DeleteRoomAsync(roomId);
        if (result.ResponseType != ResponseType.Ok)
            return BadRequest(result.Message);

        return Ok();
    }
}
