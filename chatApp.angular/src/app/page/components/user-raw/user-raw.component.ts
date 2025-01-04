import { Component, Input } from '@angular/core';
import { ChatBoxComponent } from '../chat-box/chat-box.component';
import { CommonModule } from '@angular/common';
import { ChatBoxService } from '../../Services/chat-box.service';
import { ChatModel } from '../../../../Models/Chats/ChatModel';

@Component({
  selector: 'app-user-raw',
  imports: [CommonModule],
  templateUrl: './user-raw.component.html',
  styleUrl: './user-raw.component.css',
})
export class UserRawComponent {
  @Input() chat!: ChatModel;
  /**
   *
   */
  constructor(private chatBoxService: ChatBoxService) {}

  activateChatBox() {
    this.chatBoxService.sendData(this.chat);
  }
}
