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
  constructor(private chatService: ChatService) {}

  ngAfterViewChecked(): void {
    this.ScrollToBottom();
  }

  ngOnInit(): void {
    this.GetChatMessages();
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
      .subscribe((messages) => (this.messages = messages));
  }

  // receive new message from create message component
  ReceiveNewMessage(newMessage: MessageModel) {
    this.messages.push(newMessage);
  }
}
