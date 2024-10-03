using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Common.Models.DTOs.Room;

public class GetAvailableRoomsDto
{
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public int Capacity { get; set; }
}