using System.ComponentModel.DataAnnotations;

namespace chatApp.Dtos;

public class CreateMessageDto
{
  [Required]
  public string Chat_id { get; set; }
  [Required]
  public string Sender_id { get; set; }
  [Required]
  public string Content { get; set; }
}