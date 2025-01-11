using chatApp.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace chatApp.ChatHub
{
  public class ChatHub : Hub
  {
    // public async Task SendMessage(string user, string message)
    // {
    //   await Clients.All.SendAsync("ReceiveMessage", user, message);
    // }
    // public async Task SendMessageToUser(string connectionId, string user, string message)
    // {
    //   await Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message);
    // }

    // connect user to chat
    public async Task ConnectToChat(string chatId)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    // send message to chat
    public async Task SendMessageToChat(string chatId, MessageDto message)
    {
      await Clients.Group(chatId).SendAsync("ReceiveMessage", message);
    }
  }
}