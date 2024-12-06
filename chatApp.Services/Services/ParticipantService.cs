using System.Dynamic;
using chatApp.Database;
using chatApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace chatApp.Services;

public class ParticipantService(PostgresContext dbContext) : IParticipantService
{
  private readonly PostgresContext dbContext = dbContext;


  // create new Participant
  public async Task CreateNewParticipantAsync(Participant participant)
  {
    await dbContext.Participants.AddAsync(participant);
    await dbContext.SaveChangesAsync();
  }


  // delete participant by id
  public async Task DeleteParticipantByIdAsync(Guid id)
  {
    var participant = await dbContext.Participants.FindAsync(id);
    dbContext.Participants.Remove(participant);
    await dbContext.SaveChangesAsync();
  }


  // edit participant
  public async Task EditParticipantByIdAsync(Guid id, Participant participant)
  {
    var participant1 = await dbContext.Participants.FindAsync(id);
    participant1 = participant;
    await dbContext.SaveChangesAsync();
  }


  // get all participant
  public async Task<IEnumerable<Participant>> GetAllParticipantsAsync() =>
   (await dbContext.Participants.ToListAsync());


  // get chat
  public async Task<ChatModel> getChatInParticipantAsync(Guid id) =>
   (await dbContext.Participants.Include(participant => participant.Chat)
    .FirstOrDefaultAsync(p => p.Id == id)).Chat;


  // get participants in chat
  public async Task<IEnumerable<Participant>> GetParticipantsInChatAsync(Guid chatId) =>
   (await dbContext.Chats.Include(c => c.Participants)
    .FirstOrDefaultAsync(c => c.Id == chatId)).Participants;


  // get user info in a participant
  public async Task<AppUser> getUserInParticipantAsync(Guid id) =>
   (await dbContext.Participants.Include(p => p.User)
    .FirstOrDefaultAsync(p => p.Id == id)).User;

}