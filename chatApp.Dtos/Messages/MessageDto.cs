namespace chatApp.Dtos;

public class MessageDto
{
  public string Id { get; set; }
  public string Chat_id { get; set; }
  public string Sender_id { get; set; }
  public string Content { get; set; }
  public DateTime Created_at { get; set; }
  public bool Is_read { get; set; }

  public ChatDto Chat { get; set; }
  public UserDto Sender { get; set; }
}