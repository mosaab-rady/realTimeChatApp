using System.ComponentModel.DataAnnotations;

namespace chatApp.Dtos;

public class CreatePrivateChatDto
{
  // the id of the other user
  [Required]
  public string UserID { get; set; }
}