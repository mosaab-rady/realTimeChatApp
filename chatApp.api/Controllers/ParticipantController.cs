using AutoMapper;
using chatApp.Dtos;
using chatApp.Entities;
using chatApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantController(
  IParticipantService participantService,
  IMapper mapper,
  IChatService chatService,
  UserManager<AppUser> userManager) : ControllerBase
{
  // 1) get all participants
  [HttpGet]
  public async Task<IEnumerable<ParticipantDto>> GetAllParticipants() =>
   mapper.Map<IEnumerable<ParticipantDto>>(await participantService.GetAllParticipantsAsync());


  // 2) get all participants in a chat
  [HttpGet("{chatId}/participants")]
  public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetAllParticipantsInChat(Guid chatId)
  {
    ChatModel chatModel = await chatService.GetChatByIdAsync(chatId);

    if (chatModel is null)
    {
      return Problem(
        detail: $"No chat found with id '{chatId}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    IEnumerable<Participant> participants = await participantService.GetParticipantsInChatAsync(chatId);

    return Ok(mapper.Map<IEnumerable<ParticipantDto>>(participants));
  }


  // 3) get participant by id
  [HttpGet("{id}")]
  public async Task<ActionResult<ParticipantDto>> GetParticipant(Guid id)
  {
    Participant participant = await participantService.GetParticipantByIdAsync(id);

    if (participant is null)
    {
      return Problem(
        detail: $"No participant found with id '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    return mapper.Map<ParticipantDto>(participant);
  }

  // 4) crete new participant
  [HttpPost]
  public async Task<IActionResult> CreateNewParticipant(CreateParticipantDto createParticipantDto)
  {
    ChatModel chatModel = await chatService.GetChatByIdAsync(new Guid(createParticipantDto.Chat_id));
    AppUser appUser = await userManager.FindByIdAsync(createParticipantDto.User_id);


    if (appUser is null | chatModel is null)
    {
      return Problem(
        detail: "No user or chat found.",
        statusCode: StatusCodes.Status404NotFound
        );
    }

    Participant participant = new()
    {
      Chat = chatModel,
      Chat_id = chatModel.Id,
      Role = RoleType.Member,
      User = appUser,
      User_id = appUser.Id
    };


    await participantService.CreateNewParticipantAsync(participant);

    return CreatedAtAction("CreateNewParticipant", new { detail = "Created Participant." });
  }
  // 5) delete participant
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteParticipant(Guid id)
  {
    Participant participant = await participantService.GetParticipantByIdAsync(id);

    if (participant is null)
    {
      return Problem(
        detail: $"No participant found with id '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    await participantService.DeleteParticipantByIdAsync(id);
    return NoContent();
  }
}