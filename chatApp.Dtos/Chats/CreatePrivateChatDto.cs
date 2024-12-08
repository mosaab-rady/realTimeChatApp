using System.ComponentModel.DataAnnotations;

namespace chatApp.Dtos;

public class CreatePrivateChatDto
{
  [Required]
  public string UserID { get; set; }
}