using chatApp.Entities;

namespace chatApp.Services;

public interface INotificationService
{
  // 1) get all notification in database
  Task<IEnumerable<Notification>> GetAllNotificationsAsync();
  // 2) get all notification for user
  Task<IEnumerable<Notification>> GetUserNotificationsByUserIdAsync(string userId);
  // 3) get the notification message by id
  Task<Message> GetNotificationMessageAsync(Guid id);
  // 4) create a notification
  Task CreateNewNotificationAsync(Notification notification);
  // 5) edit notification by id
  Task EditNotificationByIdAsync(Guid id, Notification notification);
  // 6) delete notification by id
  Task DeleteNotificationByIdAsync(Guid id);
  // 7) get the notification user by id
  Task<AppUser> GetNotificationUserAsync(Guid id);
}