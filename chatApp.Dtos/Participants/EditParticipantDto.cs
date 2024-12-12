using System.ComponentModel.DataAnnotations;
using chatApp.Entities;

namespace chatApp.Dtos;

public class EditParticipantDto
{
  [Required]
  public string Id { get; set; }

  [Required]
  public RoleType Role { get; set; }
}