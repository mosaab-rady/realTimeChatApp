namespace chatApp.Dtos;

public class EditMessageDto
{
  public string Id { get; set; }
  public string Content { get; set; }
  public bool Is_read { get; set; }
}