namespace ConferenceRoomBooking.Common.Models.PartialModels;

public class RoomBookingsOnDateModel
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string RoomName { get; set; }
}