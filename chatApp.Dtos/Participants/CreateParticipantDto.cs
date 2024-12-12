using System.ComponentModel.DataAnnotations;
using chatApp.Entities;

namespace chatApp.Dtos;

public class CreateParticipantDto
{
  [Required]
  public string Chat_id { get; set; }
  [Required]
  public string User_id { get; set; }
  public RoleType Role { get; set; }
}