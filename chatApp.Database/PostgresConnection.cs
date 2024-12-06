namespace chatApp.Database;

public class PostgresConnection
{
  public required string Host { get; set; }
  public required string Database { get; set; }
  public required string Username { get; set; }
  public required string Password { get; set; }
  public string ConnectionString
  {
    get
    {
      return $"Host={Host};Database={Database};Username={Username};Password={Password}";
    }
  }
}