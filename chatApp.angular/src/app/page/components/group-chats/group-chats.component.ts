import { Component, OnInit } from '@angular/core';
import { ChatModel } from '../../../../Models/Chats/ChatModel';
import { ChatService } from '../../Services/chat.service';
import { AuthService } from '../../../auth/Services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-group-chats',
  imports: [CommonModule],
  templateUrl: './group-chats.component.html',
  styleUrl: './group-chats.component.css',
})
export class GroupChatsComponent implements OnInit {
  chats: ChatModel[] = [];
  /**
   *
   */
  constructor(private chatService: ChatService, private auth: AuthService) {}
  ngOnInit(): void {
    this.chatService
      .GetGroupChats(this.auth.USER?.id!)
      .subscribe((chats) => (this.chats = chats));
  }
}
