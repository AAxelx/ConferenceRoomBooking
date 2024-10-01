using ConferenceRoomBooking.Common.Models.DTOs.Service;

namespace ConferenceRoomBooking.Common.Models.DTOs.Booking;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal TotalPrice { get; set; }
    public List<ServiceDto> SelectedServices { get; set; }
}