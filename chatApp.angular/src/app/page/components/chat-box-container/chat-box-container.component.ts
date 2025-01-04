import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ChatBoxComponent } from '../chat-box/chat-box.component';
import { ChatBoxService } from '../../Services/chat-box.service';
import { ChatModel } from '../../../../Models/Chats/ChatModel';

@Component({
  selector: 'app-chat-box-container',
  imports: [CommonModule, ChatBoxComponent],
  templateUrl: './chat-box-container.component.html',
  styleUrl: './chat-box-container.component.css',
})
export class ChatBoxContainerComponent implements OnInit {
  openedChatBoxs: ChatModel[] = [];

  /**
   *
   */
  constructor(private chatBoxService: ChatBoxService) {}
  ngOnInit(): void {
    this.chatBoxService.data$.subscribe((data) => {
      this.openedChatBoxs.unshift(data);
      if (this.openedChatBoxs.length > 2) {
        this.openedChatBoxs.pop();
      }
    });
  }
}
