namespace ConferenceRoomBooking.DAL.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }
    public List<BookingEntity> Bookings { get; set; }
    public List<ServiceEntity> AvaliableServices { get; set; }
}