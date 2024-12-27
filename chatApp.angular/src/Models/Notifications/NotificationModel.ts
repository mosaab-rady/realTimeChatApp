export interface NotificationModel {
  id: string;
  user_id: string;
  message_id: string;
  is_read: boolean;
  created_at: Date;
}
