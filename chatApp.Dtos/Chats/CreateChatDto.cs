using System.ComponentModel.DataAnnotations;
using chatApp.Entities;

namespace chatApp.Dtos;

public class CreateChatDto
{
  public ChatType Type { get; set; }
  public string Name { get; set; }
  public string Created_by { get; set; }
}