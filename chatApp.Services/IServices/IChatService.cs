using chatApp.Entities;

namespace chatApp.Services;


public interface IChatService
{
  // 1) get all chats in database
  Task<IEnumerable<ChatModel>> GetAllChatsAsync();
  // 2) get chat by id
  Task<ChatModel> GetChatByIdAsync(Guid id);
  // 3) create new chat
  Task CreateNewChatAsync(ChatModel chatModel);
  // 4) edit chat by chat_id
  Task EditChatByIdAsync(Guid id, ChatModel chatModel);
  // 5) delete chat
  Task DeleteChatByIdAsync(Guid id);


  // 6) get chats to a user by user id
  Task<IEnumerable<ChatModel>> GetChatsByUserIdAsync(string userId);
  // 7) get all participants in a chat 
  Task<IEnumerable<Participant>> GetParticipantsByChatIdAsync(Guid chatId);
  // 8) get messages in a chat
  Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId);

  // 9) get users in chat 
  Task<IEnumerable<AppUser>> getUsersByChatIdAsync(Guid chatId);

}
