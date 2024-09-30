namespace ConferenceRoomBooking.Common.Models;

public class BookingModel
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime BookingDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public List<ServiceModel> SelectedServices { get; set; } = new List<ServiceModel>();
    public decimal TotalPrice { get; set; }
}