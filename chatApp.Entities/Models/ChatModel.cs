using System.ComponentModel.DataAnnotations;

namespace chatApp.Entities;

public class ChatModel
{
  [Key]
  public Guid Id { get; set; }

  public ChatType Type { get; set; }

  public string Name { get; set; }

  public Guid Created_by { get; set; }

  public DateTime Created_at { get; set; }

  public List<AppUser> Users { get; set; }

  public List<Participant> Participants { get; set; }

  public List<Message> Messages { get; set; }
}


public enum ChatType
{
  PrivateChat,
  GroupChat

}