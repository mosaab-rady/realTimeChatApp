import { Component, OnInit } from '@angular/core';
import { ChatService } from '../../Services/chat.service';
import { AuthService } from '../../../auth/Services/auth.service';
import { ChatModel } from '../../../../Models/Chats/ChatModel';
import { UserRawComponent } from '../user-raw/user-raw.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-private-chats',
  imports: [UserRawComponent, CommonModule],
  templateUrl: './private-chats.component.html',
  styleUrl: './private-chats.component.css',
})
export class PrivateChatsComponent implements OnInit {
  chats: ChatModel[] = [];
  /**
   *
   */
  constructor(private chatService: ChatService, private auth: AuthService) {}
  ngOnInit(): void {
    this.chatService
      .GetPrivateChats(this.auth.USER?.id!)
      .subscribe((chats) => (this.chats = chats));
  }
}
