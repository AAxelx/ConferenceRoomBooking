using ConferenceRoomBooking.Common.Models.DTOs.Service;

namespace ConferenceRoomBooking.Common.Models.DTOs.Room;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }
    public List<ServiceDto> AvailableServices { get; set; }
}