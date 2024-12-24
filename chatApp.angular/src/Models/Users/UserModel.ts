export interface UserModel {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  isOnline: boolean;
  lastActive: Date;
  createdAt: Date;
}
