namespace ConferenceRoomBooking.Common.Models;

public class RoomModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }
    public List<BookingModel> Bookings { get; set; }
    public List<ServiceModel> AvailableServices { get; set; } = new ();
}