using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Common.Models.DTOs.Booking;

public class CreateBookingDto
{
    [Required]
    public Guid RoomId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public DateTime BookingDate { get; set; }
    [Required]
    public TimeSpan Duration { get; set; }
    public List<Guid> SelectedServiceIds { get; set; }
}