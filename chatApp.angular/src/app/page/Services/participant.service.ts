import { Injectable } from '@angular/core';
import { enviroment } from '../../../enviroment/enviroment';
import { HttpClient } from '@angular/common/http';
import { CreateParticipantModel } from '../../../Models/Participants/CreateParticipantModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ParticipantService {
  apiUrl = enviroment.apiUrl + '/api/participant';
  constructor(private http: HttpClient) {}
  // 1) create participant
  CreateParticipant(
    createParticipantModel: CreateParticipantModel
  ): Observable<object> {
    return this.http.post<object>(`${this.apiUrl}`, createParticipantModel, {
      withCredentials: true,
    });
  }
  // 2) delete participant
  DeleteParticipant(participantId: string): Observable<object> {
    return this.http.delete<object>(`${this.apiUrl}/${participantId}`, {
      withCredentials: true,
    });
  }
}
