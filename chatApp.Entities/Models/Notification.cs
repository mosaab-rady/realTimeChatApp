using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatApp.Entities;

public class Notification
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  [ForeignKey("Message")]
  public Guid Message_id { get; set; }
  public Message Message { get; set; }

  [Required]
  [ForeignKey("User")]
  public string User_id { get; set; }
  public AppUser User { get; set; }

  public bool Is_read { get; set; }

  public DateTime Created_at { get; set; }

}