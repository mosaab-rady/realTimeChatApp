using chatApp.Database;
using chatApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace chatApp.Services;

public class UserService(PostgresContext dbContext) : IUserService
{
  public async Task AddChatToUserByUserIdAsync(string userId, ChatModel chatModel)
  {
    var user = await dbContext.Users.Include(u => u.Chats).FirstOrDefaultAsync(u => u.Id == userId);
    user.Chats.Add(chatModel);
    await dbContext.SaveChangesAsync();
  }

  public async Task<IEnumerable<ChatModel>> GetGroupChatsAsync(string userId)
  {
    var user = await dbContext.Users.Include(u => u.Participants)
      .ThenInclude(p => p.Chat)
      .FirstOrDefaultAsync(u => u.Id == userId);

    List<ChatModel> chatModels = [];
    foreach (var paricipant in user.Participants)
    {
      chatModels.Add(paricipant.Chat);
    }

    return chatModels;
  }
}