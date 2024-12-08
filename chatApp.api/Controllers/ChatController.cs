using System.Reflection;
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
public class ChatController(
  IChatService chatService,
  IMapper mapper,
  UserManager<AppUser> userManager,
  IParticipantService participantService) : ControllerBase
{
  // private readonly IChatService chatService = chatService;
  // private readonly IMapper mapper = mapper;
  // private readonly UserManager<AppUser> userManager = userManager;

  // 1) get all chat in database
  [HttpGet]
  public async Task<IEnumerable<ChatDto>> GetAllChats() =>
  mapper.Map<IEnumerable<ChatDto>>(
    await chatService.GetAllChatsAsync()
  );

  // 2) create new Private chat
  [HttpPost("private")]
  public async Task<IActionResult> CreateNewPrivateChat(CreatePrivateChatDto createPrivateChatDto)
  {
    // first user
    var user1Email = User.Identity.Name;
    var user1 = await userManager.FindByEmailAsync(user1Email);
    // second user
    var user2 = await userManager.FindByIdAsync(createPrivateChatDto.UserID);

    // if no user return error
    if (user2 is null)
    {
      return Problem(
        detail: $"No user found with Id '{createPrivateChatDto.UserID}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    // 1) create chat
    ChatModel chatModel = new() { Type = ChatType.Private, Users = [user1, user2] };
    await chatService.CreateNewChatAsync(chatModel);
    // 2) return action result
    return CreatedAtAction(
      "CreateNewPrivateChat",
      new { detail = "created chat successfully" });
  }

  // 3) get chat by id
  [HttpGet("{id}")]
  public async Task<ActionResult<ChatDto>> GetChatByID(Guid id)
  {
    ChatModel chatModel = await chatService.GetChatByIdAsync(id);

    if (chatModel is null)
    {
      return Problem(
        detail: $"No chat found with Id '{id}'",
        statusCode: StatusCodes.Status404NotFound);
    }

    return mapper.Map<ChatDto>(chatModel);
  }


  // 4) get user chats
  [HttpGet("{userId}/chats")]
  public async Task<IActionResult> GetUserChatsByUserId(string userId)
  {
    // 1) find user
    AppUser appUser = await userManager.FindByIdAsync(userId);
    // 2) if no user return error
    if (appUser is null)
    {
      return Problem(
        detail: $"No user found with Id '{userId}'",
        statusCode: StatusCodes.Status404NotFound);
    }
    // 3) get chats
    IEnumerable<ChatModel> chatModels = await chatService.GetChatsByUserIdAsync(userId);
    // 4) return chats
    return Ok(mapper.Map<IEnumerable<ChatDto>>(chatModels));
  }


  // 5) get users in a chat
  [HttpGet("{chatId}/users")]
  public async Task<IActionResult> GetChatUsers(Guid chatId)
  {
    ChatModel chatModel = await chatService.GetChatByIdAsync(chatId);
    if (chatModel is null)
    {
      return Problem(
        detail: $"No chat found with Id '{chatId}'",
        statusCode: StatusCodes.Status404NotFound);
    }

    IEnumerable<AppUser> users = await chatService.GetChatUsersByChatIdAsync(chatId);

    return Ok(mapper.Map<IEnumerable<UserDto>>(users));
  }


  // 6) get participants in a chat
  [HttpGet("{chatId}/participants")]
  public async Task<IActionResult> GetChatParticipants(Guid chatId)
  {
    // 1) get chat
    ChatModel chatModel = await chatService.GetChatByIdAsync(chatId);
    // 2) if no chat return error
    if (chatModel is null)
    {
      return Problem(
        detail: $"No chat found with Id '{chatId}'",
        statusCode: StatusCodes.Status404NotFound);
    }
    // 3) return participants
    IEnumerable<Participant> participants = await chatService.GetParticipantsByChatIdAsync(chatId);
    return Ok(mapper.Map<IEnumerable<ParticipantDto>>(participants));
  }


  // 7) get messages in a chat
  [HttpGet("{chatId}/messages")]
  public async Task<IActionResult> GetChatMessages(Guid chatId)
  {
    // 1) get chat
    ChatModel chatModel = await chatService.GetChatByIdAsync(chatId);
    // 2) check if chat
    if (chatModel is null)
    {
      return Problem(
        detail: $"No chat found with Id '{chatId}'",
        statusCode: StatusCodes.Status404NotFound);
    }
    // 3) get and return messages
    IEnumerable<Message> messages = await chatService.GetMessagesByChatIdAsync(chatId);
    return Ok(mapper.Map<IEnumerable<MessageDto>>(messages));
  }


  // 8) edit chat by id 
  [HttpPut("{id}")]
  public async Task<IActionResult> EditChatById(Guid id, EditChatDto editChatDto)
  {
    // 1) find chat
    ChatModel chatModel = await chatService.GetChatByIdAsync(id);
    // 2) if no chat return error
    if (chatModel is null)
    {
      return Problem(
        detail: $"No chat found with Id '{id}'",
        statusCode: StatusCodes.Status404NotFound);
    }
    // 3) edit chat
    chatModel.Name = editChatDto.Name;
    await chatService.EditChatByIdAsync(id, chatModel);
    // 4) return 
    return Ok();
  }

  // 9) delete chat by id
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteChat(Guid id)
  {
    ChatModel chatModel = await chatService.GetChatByIdAsync(id);
    if (chatModel is null)
    {
      return Problem(
        detail: $"No chat found with Id '{id}'",
        statusCode: StatusCodes.Status404NotFound);
    }
    await chatService.DeleteChatByIdAsync(id);
    return NoContent();
  }

  // 10) add users to chat
  // probably there is no use case
  [HttpPut("{chatId}/addusers")]
  public async Task<IActionResult> AddUsersToChat(Guid chatId, AddUsersDto addUsersDto)
  {
    // 1) find chat
    ChatModel chatModel = await chatService.GetChatByIdAsync(chatId);
    // 2) if no chat return error
    if (chatModel is null)
    {
      return Problem(
              detail: $"No chat found with Id '{chatId}'",
              statusCode: StatusCodes.Status404NotFound);
    }
    // 3) get users
    var users = new List<AppUser>();
    foreach (var userId in addUsersDto.UserIds)
    {
      AppUser user = await userManager.FindByIdAsync(userId);

      if (user is null)
      {
        return Problem(
          detail: $"No user found with this Id '{userId}'.",
          statusCode: StatusCodes.Status404NotFound);
      }

      users.Add(user);
    }
    // 4) add users to chat
    await chatService.AddUserToChatByChatId(chatId, users);
    // 5) return 
    return Ok();
  }


  // 11) add participants to chat
  [HttpPut("{chatId}/addparticipants")]
  public async Task<IActionResult> AddParticipantsToChat(Guid chatId, AddUsersDto addUsersDto)
  {
    // 1) find chat
    ChatModel chatModel = await chatService.GetChatByIdAsync(chatId);
    // 2) if no chat return error
    if (chatModel is null)
    {
      return Problem(
              detail: $"No chat found with Id '{chatId}'",
              statusCode: StatusCodes.Status404NotFound);
    }
    // 3) get users and create a participant 
    var participants = new List<Participant>();

    foreach (var userId in addUsersDto.UserIds)
    {
      AppUser user = await userManager.FindByIdAsync(userId);

      if (user is null)
      {
        return Problem(
          detail: $"No user found with this Id '{userId}'.",
          statusCode: StatusCodes.Status404NotFound);
      }
      var participant = new Participant
      {
        Chat_id = chatId,
        Chat = chatModel,
        User_id = userId,
        User = user,
        Role = RoleType.Member
      };

      await participantService.CreateNewParticipantAsync(participant);
      participants.Add(participant);

    }
    // 4) add participants to chat
    await chatService.AddParticipantToChatByChatId(chatId, participants);
    // 5) return 
    return Ok();
  }


  // 12) create group chat
  [HttpPost("group")]
  public async Task<IActionResult> CreateNewGroupChat(CreateChatDto createChatDto)
  {
    // the user who created the chat
    var user1Email = User.Identity.Name;
    var user1 = await userManager.FindByEmailAsync(user1Email);


    // 1) create chat
    ChatModel chatModel = new()
    {
      Type = ChatType.Group,
      Name = createChatDto.Name,
      Created_by = user1.Id
    };
    await chatService.CreateNewChatAsync(chatModel);
    // 2) return action result
    return CreatedAtAction(
      "CreateNewGroupChat",
      new { detail = "created chat successfully" });
  }

  // 13) get my chats
  [HttpGet("mychats")]
  public async Task<IActionResult> GetMyChatsts()
  {
    // 1) find user
    var user1Email = User.Identity.Name;
    var user1 = await userManager.FindByEmailAsync(user1Email);
    // 2) get chats
    IEnumerable<ChatModel> chatModels = await chatService.GetChatsByUserIdAsync(user1.Id);
    // 3) return chats
    return Ok(mapper.Map<IEnumerable<ChatDto>>(chatModels));
  }
}