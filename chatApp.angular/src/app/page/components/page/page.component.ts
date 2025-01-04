import { Component } from '@angular/core';
import { AllUsersComponent } from '../all-users/all-users.component';
import { PrivateChatsComponent } from '../private-chats/private-chats.component';
import { GroupChatsComponent } from '../group-chats/group-chats.component';
import { ChatBoxContainerComponent } from '../chat-box-container/chat-box-container.component';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrl: './page.component.css',
  imports: [
    PrivateChatsComponent,
    GroupChatsComponent,
    ChatBoxContainerComponent,
  ],
})
export class PageComponent {}
