using AutoMapper;
using chatApp.Dtos;
using chatApp.Entities;
using chatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chatApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationController(
  INotificationService notificationService,
  UserManager<AppUser> userManager,
  IMessageService messageService,
  IMapper mapper) : ControllerBase
{
  // 1) get all notification
  [HttpGet]
  public async Task<IEnumerable<NotificationDto>> GetAllNotifications()
  {
    var notifications = await notificationService.GetAllNotificationsAsync();
    return mapper.Map<IEnumerable<NotificationDto>>(notifications);
  }

  // 2) get notification by id
  [HttpGet("{id}")]
  public async Task<ActionResult<NotificationDto>> GetNotificationById(Guid id)
  {
    Notification notification = await notificationService.GetNotificationByIdAsync(id);

    if (notification is null)
    {
      return Problem(
        detail: $"No notification found with id '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    return mapper.Map<NotificationDto>(notification);
  }
  // 3) get user notifications by user id
  [HttpGet("{userId}/notifications")]
  public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUserNotifications(string userId)
  {
    AppUser appUser = await userManager.FindByIdAsync(userId);

    if (appUser is null)
    {
      return Problem(
        detail: $"No user found with id '{userId}'.",
        statusCode: StatusCodes.Status404NotFound
      );
    }

    IEnumerable<Notification> notifications = await notificationService.GetUserNotificationsByUserIdAsync(userId);

    return Ok(mapper.Map<IEnumerable<NotificationDto>>(notifications));
  }


  // 4) get my notifications

  // [HttpGet("mynotifications")]
  // public async Task<ActionResult<IEnumerable<NotificationDto>>> GetMyNotifications()
  // {
  //   string userEmail = User.Identity.Name;

  //   AppUser appUser = await userManager.FindByEmailAsync(userEmail);

  //   if (appUser is null)
  //   {
  //     return Problem(
  //       detail: $"No user found with email '{userEmail}'.",
  //       statusCode: StatusCodes.Status404NotFound
  //     );
  //   }

  //   IEnumerable<Notification> notifications = await notificationService.GetUserNotificationsByUserIdAsync(appUser.Id);

  //   return Ok(mapper.Map<IEnumerable<NotificationDto>>(notifications));
  // }


  // 5) get un read notification
  [HttpGet("{userId}/notifications/unread")]
  public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUserUnReadNotifications(string userId)
  {
    AppUser appUser = await userManager.FindByIdAsync(userId);

    if (appUser is null)
    {
      return Problem(
        detail: $"No user found with id '{userId}'.",
        statusCode: StatusCodes.Status404NotFound
      );
    }

    IEnumerable<Notification> notifications =
    await notificationService.GetMyUnReadNotificationsAsync(appUser.Id);

    return Ok(mapper.Map<IEnumerable<NotificationDto>>(notifications));
  }

  // 6) create new notification
  [HttpPost]
  public async Task<IActionResult> CreateNotification(CreateNotificationDto createNotificationDto)
  {
    AppUser appUser = await userManager.FindByIdAsync(createNotificationDto.User_id);
    Message message = await messageService.GetMessageByIdAsync(createNotificationDto.Message_id);

    if (appUser is null | message is null)
    {
      return Problem(
        detail: "No user or message found.",
        statusCode: StatusCodes.Status404NotFound
        );
    }

    Notification notification = new()
    {
      Message_id = message.Id,
      Message = message,
      User = appUser,
      User_id = appUser.Id
    };

    await notificationService.CreateNewNotificationAsync(notification);

    return CreatedAtAction("CreateNotification", new { detail = "Created New Notification" });
  }


  // 7) delete notification
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteNotification(Guid id)
  {
    Notification notification = await notificationService.GetNotificationByIdAsync(id);

    if (notification is null)
    {
      return Problem(
        detail: $"No Notification found with id '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    await notificationService.DeleteNotificationByIdAsync(id);

    return NoContent();
  }

  // edit is_read property
  [HttpPut("{notificationId}/is_read")]
  public async Task<IActionResult> EditIsRead(Guid notificationId)
  {
    Notification notification = await notificationService.GetNotificationByIdAsync(notificationId);

    if (notification is null)
    {
      return Problem(
        detail: $"No Notification found with id '{notificationId}'.",
        statusCode: StatusCodes.Status404NotFound);
    }
    notification.Is_read = true;

    await notificationService.EditNotificationByIdAsync(notification.Id, notification);

    return Ok();

  }
}