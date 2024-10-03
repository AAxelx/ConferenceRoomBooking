
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Common.Models.DTOs.Room;

public class CreateRoomDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public decimal BasePricePerHour { get; set; }
    public List<Guid> AvailableServicesIds { get; set; }
}