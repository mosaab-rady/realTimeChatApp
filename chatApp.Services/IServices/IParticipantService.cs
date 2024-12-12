using chatApp.Entities;

namespace chatApp.Services;

public interface IParticipantService
{
  // 1) get all participants
  Task<IEnumerable<Participant>> GetAllParticipantsAsync();
  // 2) get all participants in a chat given chat id
  Task<IEnumerable<Participant>> GetParticipantsInChatAsync(Guid chatId);
  // 3) get chat in a participant by id
  Task<ChatModel> GetChatInParticipantAsync(Guid id);
  // 4) get user in a participant by id
  Task<AppUser> GetUserInParticipantAsync(Guid id);
  // 5) create new paricipant
  Task CreateNewParticipantAsync(Participant participant);
  // 6) edit participant by id
  Task EditParticipantByIdAsync(Guid id, Participant participant);
  // 7) delete participant by id
  Task DeleteParticipantByIdAsync(Guid id);
  // 8) get Participant by id
  Task<Participant> GetParticipantByIdAsync(Guid id);
}