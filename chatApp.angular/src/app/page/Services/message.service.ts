import { Injectable } from '@angular/core';
import { enviroment } from '../../../enviroment/enviroment';
import { HttpClient } from '@angular/common/http';
import { CreateMessageModel } from '../../../Models/Messages/CreateMessageModel';
import { Observable } from 'rxjs';
import { MessageModel } from '../../../Models/Messages/MessageModel';
import { EditMessageModel } from '../../../Models/Messages/EditMessageModel';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  apiUrl = enviroment.apiUrl + '/api/message';

  constructor(private http: HttpClient) {}

  // 1) create message
  CreateMessage(
    createMessageModel: CreateMessageModel
  ): Observable<MessageModel> {
    return this.http.post<MessageModel>(`${this.apiUrl}`, createMessageModel, {
      withCredentials: true,
    });
  }
  // 2) delete message
  DeleteMessage(messageId: string): Observable<object> {
    return this.http.delete<object>(`${this.apiUrl}/${messageId}`, {
      withCredentials: true,
    });
  }
  // 3) edit message
  EditMessage(
    messageId: string,
    editMessageModel: EditMessageModel
  ): Observable<object> {
    return this.http.put<object>(
      `${this.apiUrl}/${messageId}`,
      editMessageModel,
      { withCredentials: true }
    );
  }
  // -----------------
  // 4) read message
  ReadMessage(messageId: string): Observable<object> {
    return this.http.put<object>(
      `${this.apiUrl}/${messageId}/is_read`,
      {},
      { withCredentials: true }
    );
  }
}
