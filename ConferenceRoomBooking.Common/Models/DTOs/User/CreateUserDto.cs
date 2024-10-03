using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.Common.Models.DTOs.User;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
}