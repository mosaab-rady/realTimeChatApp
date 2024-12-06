using chatApp.Database;
using chatApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chatApp.Services;

public class NotificationService(
  PostgresContext dbContext,
  UserManager<AppUser> userManager) : INotificationService
{
  private readonly PostgresContext dbContext = dbContext;
  private readonly UserManager<AppUser> userManager = userManager;


  // create new notification
  public async Task CreateNewNotificationAsync(Notification notification)
  {
    await dbContext.Notifications.AddAsync(notification);
    await dbContext.SaveChangesAsync();
  }

  // delete notification
  public async Task DeleteNotificationByIdAsync(Guid id)
  {
    var notification = await dbContext.Notifications.FindAsync(id);
    dbContext.Notifications.Remove(notification);
    await dbContext.SaveChangesAsync();
  }

  // edit notification
  public async Task EditNotificationByIdAsync(Guid id, Notification notification)
  {
    var notification1 = await dbContext.Notifications.FindAsync(id);
    notification1 = notification;
    await dbContext.SaveChangesAsync();
  }

  // get all notifiactions
  public async Task<IEnumerable<Notification>> GetAllNotificationsAsync() =>
   (await dbContext.Notifications.ToListAsync());

  // get the message that the notification came to him
  public async Task<Message> GetNotificationMessageAsync(Guid id) =>
   (await dbContext.Notifications.Include(n => n.Message)
    .FirstOrDefaultAsync(n => n.Id == id)).Message;

  // get the user the notification came to him
  public async Task<AppUser> GetNotificationUserAsync(Guid id) =>
   (await dbContext.Notifications.Include(n => n.User)
    .FirstOrDefaultAsync(n => n.Id == id)).User;


  // get all notifications for a user
  public async Task<IEnumerable<Notification>> GetUserNotificationsByUserIdAsync(string userId) =>
   (await userManager.Users.Include(u => u.Notifications)
    .FirstOrDefaultAsync(u => u.Id == userId)).Notifications;

}