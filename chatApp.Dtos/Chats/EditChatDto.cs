using System.ComponentModel.DataAnnotations;

namespace chatApp.Dtos;

public class EditChatDto
{
  [Required]
  public string Id { get; set; }
  [Required]
  public string Name { get; set; }
}