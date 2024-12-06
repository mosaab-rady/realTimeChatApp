namespace chatApp.Dtos;

public class UserDto
{
  public string Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public bool IsOnline { get; set; }
  public DateTime Last_active { get; set; }
  public DateTime Created_at { get; set; }
}