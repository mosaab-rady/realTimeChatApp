import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { enviroment } from '../../../enviroment/enviroment';
import { MessageModel } from '../../../Models/Messages/MessageModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection; // This is used to create connection
  constructor() {
    // This is used to create connection
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${enviroment.apiUrl}/chatHub`)
      .withAutomaticReconnect()
      .build();
  }

  // This method is used to start the connection
  public startConnection = () => {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  // This method returns an observable of message model
  // This observable is used to listen to the message from the server
  public addReceiveMessageListener(): Observable<MessageModel> {
    return new Observable<MessageModel>((observer) => {
      this.hubConnection.on('ReceiveMessage', (message: MessageModel) => {
        observer.next(message); // send the message to the client
      });
    });
  }

  // This method is used to send message to the server
  // public sendMessage = (user: string, message: string) => {
  //   this.hubConnection.invoke('SendMessage', user, message);
  // };

  // This method is used to connect to chat
  public connectToChat = (chatId: string) => {
    this.hubConnection.invoke('ConnectToChat', chatId);
  };

  // this method is used to send message to chat
  public sendMessageToChat = (chatId: string, message: MessageModel) => {
    this.hubConnection.invoke('SendMessageToChat', chatId, message);
  };
}
