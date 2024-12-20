using chatApp.Entities;

namespace chatApp.Services;

public interface IUserService
{
  Task AddChatToUserByUserIdAsync(string userId, ChatModel chatModel);

  Task<IEnumerable<ChatModel>> GetGroupChatsAsync(string userId);
}