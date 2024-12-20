using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace chatApp.Entities;

public class AppUser : IdentityUser
{

  [Required]
  public required string FirstName { get; set; }

  [Required]
  public required string LastName { get; set; }

  public Boolean IsOnline { get; set; }

  public DateTime Last_active { get; set; }

  public DateTime Created_at { get; set; } = DateTime.UtcNow;

  public List<ChatModel> Chats { get; set; }

  public List<Notification> Notifications { get; set; }

  public List<Participant> Participants { get; set; }
}