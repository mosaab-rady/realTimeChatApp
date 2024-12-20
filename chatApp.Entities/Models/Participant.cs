using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace chatApp.Entities;

public class Participant
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  [ForeignKey("User")]
  public string User_id { get; set; }
  [Required]
  [JsonIgnore] // Prevents serialization of this property
  public AppUser User { get; set; }

  [Required]
  [ForeignKey("Chat")]
  public Guid Chat_id { get; set; }

  [Required]
  [JsonIgnore] // Prevents serialization of this property
  public ChatModel Chat { get; set; }

  public RoleType Role { get; set; }

  public DateTime Joined_at { get; set; } = DateTime.UtcNow;
}


public enum RoleType
{
  Admin,
  Member
}