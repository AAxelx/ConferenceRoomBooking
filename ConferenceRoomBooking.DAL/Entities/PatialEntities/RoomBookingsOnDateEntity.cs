namespace ConferenceRoomBooking.DAL.Entities.PatialEntities;

public class RoomBookingsOnDateEntity
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string RoomName { get; set; }
}