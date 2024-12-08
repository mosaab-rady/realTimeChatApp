using chatApp.Entities;

namespace chatApp.Dtos;

public class CreateParticipantDto
{
  public string Chat_id { get; set; }
  public string User_id { get; set; }
  public RoleType Role { get; set; }
}