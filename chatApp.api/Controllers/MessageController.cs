using AutoMapper;
using chatApp.Dtos;
using chatApp.Entities;
using chatApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chatApp.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MessageController(
  IMessageService messageService,
  IMapper mapper,
  UserManager<AppUser> userManager,
  IChatService chatService) : ControllerBase
{

  // 1) get all messages in database
  [HttpGet]
  public async Task<IEnumerable<MessageDto>> GetAllMessages()
  {
    return mapper.Map<IEnumerable<MessageDto>>(await messageService.GetAllMessagesAsync());
  }

  // 2) create new message
  [HttpPost]
  public async Task<IActionResult> CreateNewMessage(CreateMessageDto createMessageDto)
  {
    // 1) get user
    AppUser appUser = await userManager.FindByIdAsync(createMessageDto.Sender_id);
    // 2) get chat
    ChatModel chatModel = await chatService.GetChatByIdAsync(new Guid(createMessageDto.Chat_id));
    // 3) if no user or chat return not found error
    if (appUser is null | chatModel is null)
    {
      return Problem(
        detail: "No user or chat found",
        statusCode: StatusCodes.Status404NotFound);
    }

    // 4) Message model
    Message message = new()
    {
      Chat = chatModel,
      Chat_id = chatModel.Id,
      Content = createMessageDto.Content,
      Is_read = false,
      Sender = appUser,
      Sender_id = appUser.Id
    };
    // 5) create Message
    await messageService.CreateNewMessageAsync(message);
    // 6) return action
    return CreatedAtAction(
      "CreateNewMessage",
      mapper.Map<MessageDto>(message));
  }

  // 3) edit message
  [HttpPut("{id}")]
  public async Task<IActionResult> EditMessage(Guid id, EditMessageDto editMessageDto)
  {
    // 1) get message 
    Message message = await messageService.GetMessageByIdAsync(id);
    // 2) if no message
    if (message is null)
    {
      return Problem(
        $"No message found with id '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }
    // 3) edit message
    message.Content = editMessageDto.Content;
    await messageService.EditMessageByIdAsync(id, message);
    // 4) return
    return Ok();
  }


  // 4) delete message
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteMessage(Guid id)
  {
    Message message = await messageService.GetMessageByIdAsync(id);

    if (message is null)
    {
      return Problem(
         $"No message found with id '{id}'.",
         statusCode: StatusCodes.Status404NotFound);
    }

    await messageService.DeleteMessageByIdAsync(id);

    return NoContent();
  }


  // 5) get message by id
  [HttpGet("{id}")]
  public async Task<ActionResult<MessageDto>> GetMessage(Guid id)
  {
    Message message = await messageService.GetMessageByIdAsync(id);
    if (message is null)
    {
      return Problem(
          $"No message found with id '{id}'.",
          statusCode: StatusCodes.Status404NotFound);
    }

    return mapper.Map<MessageDto>(message);
  }

  // 6) edit a message is_read property
  [HttpPut("{id}/is_read")]
  public async Task<IActionResult> EditMessageRead(Guid id)
  {
    Message message = await messageService.GetMessageByIdAsync(id);

    if (message is null)
    {
      return Problem(
        $"No message found with id '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    message.Is_read = true;
    await messageService.EditMessageByIdAsync(id, message);

    return Ok();
  }


}