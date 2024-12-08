namespace chatApp.Dtos;

public class CreateMessageDto
{
  public string Chat_id { get; set; }
  public string Sender_id { get; set; }
  public string Content { get; set; }
}