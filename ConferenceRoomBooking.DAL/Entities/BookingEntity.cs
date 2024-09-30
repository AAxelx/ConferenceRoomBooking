namespace ConferenceRoomBooking.DAL.Entities;

public class BookingEntity
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid RoomId { get; set; }
    public RoomEntity Room { get; set; }
    public List<ServiceEntity> SelectedServices { get; set; }
}