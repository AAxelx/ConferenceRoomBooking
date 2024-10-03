namespace ConferenceRoomBooking.Common.Models;

public class ServiceModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<RoomModel> Rooms { get; set; } = new ();
}