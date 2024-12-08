using chatApp.Entities;

namespace chatApp.Dtos;

public class ChatDto
{
  public string Id { get; set; }
  public string Type { get; set; }
  public string Name { get; set; }
  public string Created_by { get; set; }
  public DateTime Created_at { get; set; }
  public List<UserDto> Users { get; set; }
  public List<MessageDto> Messages { get; set; }
  public List<ParticipantDto> Participants { get; set; }
}

