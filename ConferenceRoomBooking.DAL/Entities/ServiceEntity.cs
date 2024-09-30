namespace ConferenceRoomBooking.DAL.Entities;

public class ServiceEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<BookingEntity> Bookings { get; set; }
    public List<RoomEntity> Rooms { get; set; }
}