import {
  AfterViewChecked,
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ChatModel } from '../../../../Models/Chats/ChatModel';
import { ChatService } from '../../Services/chat.service';
import { MessageModel } from '../../../../Models/Messages/MessageModel';
import { MessageComponent } from '../message/message.component';
import { CommonModule } from '@angular/common';
import { CreateMessageComponent } from '../create-message/create-message.component';
import { SignalRService } from '../../Services/signal-r.service';

@Component({
  selector: 'app-chat-box',
  imports: [MessageComponent, CommonModule, CreateMessageComponent],
  templateUrl: './chat-box.component.html',
  styleUrl: './chat-box.component.css',
})
export class ChatBoxComponent implements OnInit, AfterViewChecked {
  @Input() chat!: ChatModel;
  @ViewChild('messagesSec') messagesSec!: ElementRef;
  messages: MessageModel[] = [];

  /**
   *
   */
  constructor(
    private chatService: ChatService,
    private signalR: SignalRService
  ) {}

  ngAfterViewChecked(): void {
    this.ScrollToBottom();
  }

  ngOnInit(): void {
    this.GetChatMessages();
    this.signalR.connectToChat(this.chat.id);
    // listen to new messages
    this.signalR.addReceiveMessageListener().subscribe((message) => {
      this.messages.push(message);
    });
  }
  // scroll to the last message
  ScrollToBottom() {
    const div = this.messagesSec.nativeElement;
    div.scrollTop = div.scrollHeight;
  }
  // get chat messages
  GetChatMessages() {
    this.chatService
      .GetChatMessages(this.chat.id)
      .subscribe(
        (messages) =>
          (this.messages = messages.sort((a, b) =>
            a.created_at > b.created_at ? 1 : -1
          ))
      );
  }
}
