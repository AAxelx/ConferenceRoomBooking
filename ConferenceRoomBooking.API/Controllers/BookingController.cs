using ConferenceRoomBooking.Common.Models.DTOs.Booking;
using ConferenceRoomBooking.Common.Models.Enums;
using ConferenceRoomBooking.SAL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : BaseController
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto createBookingDto)
    {
        var result = await _bookingService.CreateBookingAsync(createBookingDto);
        if (result.ResponseType != ResponseType.Ok)
            return BadRequest(result.Message);

        return Ok(new { BookingId = result.Value });
    }
}
