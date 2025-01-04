import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AuthService } from '../../../auth/Services/auth.service';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MessageService } from '../../Services/message.service';
import { CreateMessageModel } from '../../../../Models/Messages/CreateMessageModel';
import { MessageModel } from '../../../../Models/Messages/MessageModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-message',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-message.component.html',
  styleUrl: './create-message.component.css',
})
export class CreateMessageComponent {
  @Input() chatId: string = '';
  @Output() sendNewMessage = new EventEmitter<MessageModel>();

  /**
   *
   */
  constructor(
    private auth: AuthService,
    private messageService: MessageService
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
    //------------
    this.messageService
      .CreateMessage(createMessageModel)
      .subscribe((createdMessage) => {
        this.sendNewMessage.emit(createdMessage); // add the new created message to chat messages
        this.CreateMessageForm.reset();
      });
  }
}
