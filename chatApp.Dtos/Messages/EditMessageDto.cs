using System.ComponentModel.DataAnnotations;

namespace chatApp.Dtos;

public class EditMessageDto
{
  [Required]
  public string Id { get; set; }
  [Required]
  public string Content { get; set; }
}