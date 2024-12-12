using System.ComponentModel.DataAnnotations;

namespace chatApp.Dtos;

public class CreateNotificationDto
{
  [Required]
  public string User_id { get; set; }
  [Required]
  public string Message_id { get; set; }
}