using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatApp.Entities;

public class Participant
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  [ForeignKey("User")]
  public string User_id { get; set; }
  public AppUser User { get; set; }

  [Required]
  [ForeignKey("Chat")]
  public Guid Chat_id { get; set; }

  [Required]
  public ChatModel Chat { get; set; }

  public RoleType Role { get; set; }

  public DateTime Joined_at { get; set; } = DateTime.UtcNow;
}


public enum RoleType
{
  Admin,
  Member
}