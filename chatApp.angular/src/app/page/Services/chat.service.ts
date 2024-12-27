import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { enviroment } from '../../../enviroment/enviroment';
import { CreatePrivateChatModel } from '../../../Models/Chats/CreatePrivateChatModel';
import { Observable } from 'rxjs';
import { CreateGroupChatModel } from '../../../Models/Chats/CreateGroupChatModel';
import { ChatModel } from '../../../Models/Chats/ChatModel';
import { ParticipantModel } from '../../../Models/Participants/ParticipantModel';
import { UserModel } from '../../../Models/Users/UserModel';
import { MessageModel } from '../../../Models/Messages/MessageModel';
import { AddUsersToChatModel } from '../../../Models/Users/AddUsersToChatModel';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  apiUrl = `${enviroment.apiUrl}/api/chat`;
  constructor(private http: HttpClient) {}

  // 1) get user private chats
  GetPrivateChats(userId: string): Observable<ChatModel[]> {
    return this.http.get<ChatModel[]>(
      `${this.apiUrl}/${userId}/private/chats`,
      { withCredentials: true }
    );
  }
  // 2) get user group chats
  GetGroupChats(userId: string): Observable<ChatModel[]> {
    return this.http.get<ChatModel[]>(`${this.apiUrl}/${userId}/group/chats`, {
      withCredentials: true,
    });
  }
  // 4) create private chat
  createPrivateChat(
    createPrivateChatModel: CreatePrivateChatModel
  ): Observable<object> {
    return this.http.post(
      `${enviroment.apiUrl}/private`,
      createPrivateChatModel,
      { withCredentials: true }
    );
  }
  // 5) create group chat
  createGroupChat(
    createGroupChatModel: CreateGroupChatModel
  ): Observable<object> {
    return this.http.post(`${enviroment.apiUrl}/group`, createGroupChatModel, {
      withCredentials: true,
    });
  }

  // 6) get participants in a chat
  GetParticipantsInChat(chatId: string): Observable<ParticipantModel[]> {
    return this.http.get<ParticipantModel[]>(
      `${this.apiUrl}/${chatId}/participants`,
      { withCredentials: true }
    );
  }

  // 7) get users in chat
  GetUsersInChat(chatId: string): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`${this.apiUrl}/${chatId}/users`, {
      withCredentials: true,
    });
  }

  // 8) get chat messages
  GetChatMessages(chatId: string): Observable<MessageModel[]> {
    return this.http.get<MessageModel[]>(`${this.apiUrl}/${chatId}/messages`, {
      withCredentials: true,
    });
  }
  //9) Delete Chat --------------------------------
  DeleteChat(chatId: string): Observable<object> {
    return this.http.delete<object>(`${this.apiUrl}/${chatId}`, {
      withCredentials: true,
    });
  }
  // 10) Add users to group chat -------------------------
  AddUsersToGroupChat(
    chatId: string,
    addUsersToChatModel: AddUsersToChatModel
  ): Observable<object> {
    return this.http.put<object>(
      `${this.apiUrl}/${chatId}/addparticipants`,
      addUsersToChatModel,
      {
        withCredentials: true,
      }
    );
  }
  //------------------------

  // ) leave group
}
