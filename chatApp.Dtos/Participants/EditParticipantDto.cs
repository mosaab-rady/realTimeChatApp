using chatApp.Entities;

namespace chatApp.Dtos;

public class EditParticipantDto
{
  public string Id { get; set; }
  public RoleType Role { get; set; }
}