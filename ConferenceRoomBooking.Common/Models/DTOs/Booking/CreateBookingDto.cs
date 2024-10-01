namespace ConferenceRoomBooking.Common.Models.DTOs.Booking;

public class CreateBookingDto
{
    public Guid RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public TimeSpan Duration { get; set; }
    public List<Guid> SelectedServiceIds { get; set; }
}