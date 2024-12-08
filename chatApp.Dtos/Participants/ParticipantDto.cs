using chatApp.Entities;

namespace chatApp.Dtos;

public class ParticipantDto
{
  public string Id { get; set; }
  public string Chat_id { get; set; }
  public string User_id { get; set; }
  public RoleType Role { get; set; }
  public DateTime Joined_at { get; set; }
  public ChatDto Chat { get; set; }
  public UserDto User { get; set; }
}