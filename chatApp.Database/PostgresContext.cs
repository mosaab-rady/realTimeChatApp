using chatApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace chatApp.Database;

public class PostgresContext : IdentityDbContext<AppUser>
{
  public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
  {
  }


  public DbSet<ChatModel> Chats { get; set; }
  public DbSet<Message> Messages { get; set; }
  public DbSet<Notification> Notifications { get; set; }
  public DbSet<Participant> Participants { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    base.OnModelCreating(modelBuilder);

    modelBuilder.HasPostgresExtension("uuid-ossp");

    modelBuilder.Entity<ChatModel>(entity =>
    {
      entity.Property(e => e.Id)
      .HasDefaultValueSql("uuid_generate_v4()");
    });


    modelBuilder.Entity<Message>(entity =>
    {
      entity.Property(e => e.Id)
      .HasDefaultValueSql("uuid_generate_v4()");
    });

    modelBuilder.Entity<Notification>(entity =>
    {
      entity.Property(e => e.Id)
      .HasDefaultValueSql("uuid_generate_v4()");
    });

    modelBuilder.Entity<Participant>(entity =>
    {
      entity.Property(e => e.Id)
      .HasDefaultValueSql("uuid_generate_v4()");
    });
  }

}