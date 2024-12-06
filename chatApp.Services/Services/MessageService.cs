using chatApp.Database;
using chatApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace chatApp.Services;

public class MessageService(PostgresContext dbContext) : IMessageService
{
  private readonly PostgresContext dbContext = dbContext;


  // create new message
  public async Task CreateNewMessageAsync(Message message)
  {
    await dbContext.Messages.AddAsync(message);
    await dbContext.SaveChangesAsync();
  }


  // delete message
  public async Task DeleteMessageByIdAsync(Guid id)
  {
    // 1) find message 
    // 2) delete it
    dbContext.Messages.Remove(await dbContext.Messages.FindAsync(id));
    // 3) save changes
    await dbContext.SaveChangesAsync();
  }


  // Edit message by id
  public async Task EditMessageByIdAsync(Guid id, Message message)
  {
    // 1) find message
    Message message1 = await dbContext.Messages.FindAsync(id);
    // 2) update
    message1 = message;
    // 3) save changes
    await dbContext.SaveChangesAsync();
  }


  // get all messages in database
  // no use case
  public async Task<IEnumerable<Message>> GetAllMessagesAsync() =>
    await dbContext.Messages.ToListAsync();


  // get all messages in a chat
  // similar to that in chat service
  public async Task<IEnumerable<Message>> GetAllMessagesInChatAsync(Guid chatId) =>
   (await dbContext.Chats.Include(chat => chat.Messages)
    .FirstOrDefaultAsync(chat => chat.Id == chatId)).Messages;


  // get single message by id
  public async Task<Message> GetMessageByIdAsync(Guid id) =>
    await dbContext.Messages.FindAsync(id);

}