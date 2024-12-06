using chatApp.Entities;

namespace chatApp.Services;

public interface IMessageService
{
  // 1) get all messages in the database
  Task<IEnumerable<Message>> GetAllMessagesAsync();
  // 2) create new message
  Task CreateNewMessageAsync(Message message);
  // 3) edit message by id 
  Task EditMessageByIdAsync(Guid id, Message message);
  // 4) delete message by id
  Task DeleteMessageByIdAsync(Guid id);
  // 5) get all messages in a chat by chat id
  Task<IEnumerable<Message>> GetAllMessagesInChatAsync(Guid chatId);
  // 6) get message by id
  Task<Message> GetMessageByIdAsync(Guid id);
}