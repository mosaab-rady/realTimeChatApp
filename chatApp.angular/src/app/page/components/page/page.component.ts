import { Component, OnInit } from '@angular/core';
import { PrivateChatsComponent } from '../private-chats/private-chats.component';
import { ChatBoxContainerComponent } from '../chat-box-container/chat-box-container.component';
import { SignalRService } from '../../Services/signal-r.service';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrl: './page.component.css',
  imports: [PrivateChatsComponent, ChatBoxContainerComponent],
})
export class PageComponent implements OnInit {
  constructor(private signalR: SignalRService) {}

  ngOnInit(): void {
    this.signalR.startConnection(); // Start the connection
  }
}
