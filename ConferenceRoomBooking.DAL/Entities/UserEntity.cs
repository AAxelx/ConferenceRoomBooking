namespace ConferenceRoomBooking.DAL.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<BookingEntity> Bookings { get; set; }
}