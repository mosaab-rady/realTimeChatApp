export interface ChatModel {
  id: string;
  type: string;
  name: string;
  created_by: string;
  created_at: Date;
  userId: string; // the id of the second user for private chats
}
