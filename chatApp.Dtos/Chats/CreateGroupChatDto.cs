using System.ComponentModel.DataAnnotations;
using chatApp.Entities;

namespace chatApp.Dtos;

public class CreateGroupChatDto
{
  // group name
  [Required]
  public string Name { get; set; }
}