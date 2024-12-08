using chatApp.Database;
using chatApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chatApp.Services;


public class ChatService(PostgresContext dbContext, UserManager<AppUser> userManager) : IChatService
{
  private readonly PostgresContext dbContext = dbContext;
  private readonly UserManager<AppUser> userManager = userManager;


  // create new chat
  public async Task CreateNewChatAsync(ChatModel chatModel)
  {
    await dbContext.Chats.AddAsync(chatModel);
    await dbContext.SaveChangesAsync();
  }

  // delete chat by id
  public async Task DeleteChatByIdAsync(Guid id)
  {
    var chat = await dbContext.Chats.FindAsync(id);
    dbContext.Chats.Remove(chat);
    await dbContext.SaveChangesAsync();
  }


  // edit chat
  public async Task EditChatByIdAsync(Guid id, ChatModel chatModel)
  {
    // 1) find chat
    ChatModel chatModel1 = await dbContext.Chats.FindAsync(id);
    // 2) update chat
    chatModel1 = chatModel;
    // 3) save changes
    await dbContext.SaveChangesAsync();

  }


  // get all chats
  public async Task<IEnumerable<ChatModel>> GetAllChatsAsync() =>
   await dbContext.Chats.ToListAsync();


  // get chat by id
  public async Task<ChatModel> GetChatByIdAsync(Guid id) =>
   await dbContext.Chats.FindAsync(id);


  // get chats that a user is participating in it
  public async Task<IEnumerable<ChatModel>> GetChatsByUserIdAsync(string userId) =>
   (await userManager.Users.Include(user => user.Chats)
    .SingleOrDefaultAsync(user => user.Id == userId)).Chats;


  // get all messages in a chat
  public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId) =>
    (await dbContext.Chats.Include(chat => chat.Messages)
    .FirstOrDefaultAsync(chat => chat.Id == chatId)).Messages;


  // get all participants in a chat
  public async Task<IEnumerable<Participant>> GetParticipantsByChatIdAsync(Guid chatId) =>
   (await dbContext.Chats.Include(chat => chat.Participants)
    .FirstOrDefaultAsync(chat => chat.Id == chatId)).Participants;


  // get users that participate in a chat "private chat"
  public async Task<IEnumerable<AppUser>> GetChatUsersByChatIdAsync(Guid chatId) =>
   (await dbContext.Chats.Include(chat => chat.Users)
    .FirstOrDefaultAsync(chat => chat.Id == chatId)).Users;


  // add users to chat
  public async Task AddUserToChatByChatId(Guid chatId, List<AppUser> users)
  {
    var chatUsers = (await dbContext.Chats.Include(c => c.Users)
      .FirstOrDefaultAsync(c => c.Id == chatId)).Users;

    chatUsers.AddRange(users);

    await dbContext.SaveChangesAsync();
  }


  // add participant to chat
  public async Task AddParticipantToChatByChatId(Guid chatId, List<Participant> participants)
  {
    var chatParticipants = (await dbContext.Chats.Include(c => c.Participants)
      .FirstOrDefaultAsync(c => c.Id == chatId)).Participants;

    chatParticipants.AddRange(participants);

    await dbContext.SaveChangesAsync();
  }
}