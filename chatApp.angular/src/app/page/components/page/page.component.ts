import { Component } from '@angular/core';
import { AllUsersComponent } from '../all-users/all-users.component';
import { PrivateChatsComponent } from '../private-chats/private-chats.component';
import { GroupChatsComponent } from '../group-chats/group-chats.component';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrl: './page.component.css',
  imports: [AllUsersComponent, PrivateChatsComponent, GroupChatsComponent],
})
export class PageComponent {}
