import { Component, Input, OnInit } from '@angular/core';
import { MessageModel } from '../../../../Models/Messages/MessageModel';
import moment from 'moment';
import { AuthService } from '../../../auth/Services/auth.service';

@Component({
  selector: 'app-message',
  imports: [],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css',
})
export class MessageComponent implements OnInit {
  @Input() message!: MessageModel;
  myMessage: Boolean = false;
  /**
   *
   */
  constructor(private auth: AuthService) {}

  ngOnInit(): void {
    this.message.created_at = moment(this.message.created_at).fromNow();
    if (this.auth.USER!.id === this.message.sender_id) {
      this.myMessage = true;
    }
  }
}
