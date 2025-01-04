import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ChatModel } from '../../../Models/Chats/ChatModel';

@Injectable({
  providedIn: 'root',
})
export class ChatBoxService {
  constructor() {}

  private dataSubject = new Subject<ChatModel>(); // Subject for emitting data
  data$ = this.dataSubject.asObservable(); // Observable for subscribers

  sendData(data: ChatModel) {
    this.dataSubject.next(data); // Emit new data
  }
}
