namespace chatApp.Dtos;

public class NotificationDto
{
  public string Id { get; set; }

  public string User_id { get; set; }
  public string Message_id { get; set; }
  public bool Is_read { get; set; }
  public DateTime Created_at { get; set; }
}