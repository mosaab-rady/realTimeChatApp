import { Injectable } from '@angular/core';
import { enviroment } from '../../../enviroment/enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NotificationModel } from '../../../Models/Notifications/NotificationModel';
import { CreateNotificationModel } from '../../../Models/Notifications/CreateNotificationModel';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  apiUrl = enviroment.apiUrl + '/api/notification';
  constructor(private http: HttpClient) {}

  // -------------------
  // 1) get user notifications
  GetUserNotifications(userId: string): Observable<NotificationModel[]> {
    return this.http.get<NotificationModel[]>(
      `${this.apiUrl}/${userId}/notifications`,
      { withCredentials: true }
    );
  }
  // ---------------
  // 2) get user unread notifications
  GetUserUnReadNotifications(userId: string): Observable<NotificationModel[]> {
    return this.http.get<NotificationModel[]>(
      `${this.apiUrl}/${userId}/notifications/unread`,
      { withCredentials: true }
    );
  }
  // 3) create new notification
  CreateNotification(
    createNotificationModel: CreateNotificationModel
  ): Observable<object> {
    return this.http.post<object>(`${this.apiUrl}`, createNotificationModel, {
      withCredentials: true,
    });
  }
  // 4) see notification
  ReadNotification(notificationId: string): Observable<object> {
    return this.http.put<object>(
      `${this.apiUrl}/${notificationId}/is_read`,
      {},
      { withCredentials: true }
    );
  }

  // 5) delete notification
  DeleteNotification(notificationId: string): Observable<object> {
    return this.http.delete<object>(`${this.apiUrl}/${notificationId}`, {
      withCredentials: true,
    });
  }
}
