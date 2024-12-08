namespace chatApp.Dtos;

public class AddUsersDto
{
  // chat_id if adding users to chat
  public string Id { get; set; }
  public List<string> UserIds { get; set; }
}