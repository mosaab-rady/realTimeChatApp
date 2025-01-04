import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../../Models/Users/UserModel';
import { enviroment } from '../../../../enviroment/enviroment';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-all-users',
  imports: [CommonModule],
  templateUrl: './all-users.component.html',
  styleUrl: './all-users.component.css',
})
export class AllUsersComponent implements OnInit {
  users: UserModel[] = [];
  /**
   *
   */
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    // 1) get all users
    this.http
      .get<UserModel[]>(`${enviroment.apiUrl}/api/users`, {
        withCredentials: true,
      })
      .subscribe((e) => (this.users = e));
  }
}
