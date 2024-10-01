
namespace ConferenceRoomBooking.Common.Models.DTOs.Room;

public class CreateRoomDto
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }
    public List<Guid> AvailableServices { get; set; }
}