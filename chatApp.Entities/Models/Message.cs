using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatApp.Entities;


public class Message
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  [ForeignKey("Chat")]
  public Guid Chat_id { get; set; }

  [Required]
  public ChatModel Chat { get; set; }

  [Required]
  [ForeignKey("Sender")]
  public string Sender_id { get; set; }

  [Required]
  public AppUser Sender { get; set; }

  [Required]
  public string Content { get; set; }

  public DateTime Created_at { get; set; } = DateTime.UtcNow;

  public bool Is_read { get; set; }
}