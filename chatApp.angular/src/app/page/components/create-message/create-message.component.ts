import { Component, Input } from '@angular/core';
import { AuthService } from '../../../auth/Services/auth.service';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MessageService } from '../../Services/message.service';
import { CreateMessageModel } from '../../../../Models/Messages/CreateMessageModel';
import { CommonModule } from '@angular/common';
import { SignalRService } from '../../Services/signal-r.service';

@Component({
  selector: 'app-create-message',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-message.component.html',
  styleUrl: './create-message.component.css',
})
export class CreateMessageComponent {
  @Input() chatId: string = ''; // the id of the chat
  /**
   *
   */
  constructor(
    private auth: AuthService,
    private messageService: MessageService,
    private signalR: SignalRService
  ) {}

  CreateMessageForm: FormGroup = new FormGroup({
    Content: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required],
    }),
  });

  onSubmit() {
    if (this.CreateMessageForm.valid) {
      this.CreateMessageFn();
    }
  }

  CreateMessageFn() {
    // create message model
    const createMessageModel: CreateMessageModel = {
      chat_id: this.chatId, // the id of current chat
      sender_id: this.auth.USER!.id, // current user id
      content: this.CreateMessageForm.value.Content,
    };
    // create the message and send it to chat using signalR
    this.messageService
      .CreateMessage(createMessageModel)
      .subscribe((createdMessage) => {
        // send the message to chat
        this.signalR.sendMessageToChat(this.chatId, createdMessage);

        this.CreateMessageForm.reset(); // reset the form
      });
  }
}
