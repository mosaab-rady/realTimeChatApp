import { Injectable } from '@angular/core';
import { UserModel } from '../../../Models/Users/UserModel';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../../../Models/Users/LoginModel';
import { firstValueFrom, Observable } from 'rxjs';
import { SignupModel } from '../../../Models/Users/SignupModel';
import { Router } from '@angular/router';
import { enviroment } from '../../../enviroment/enviroment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  USER: UserModel | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  // 1) log in
  Login(model: LoginModel): Observable<UserModel> {
    return this.http.post<UserModel>(
      `${enviroment.apiUrl}/api/account/login`,
      model,
      {
        withCredentials: true,
      }
    );
  }

  // 2) sign up
  Signup(model: SignupModel): Observable<object> {
    return this.http.post<object>(
      `${enviroment.apiUrl}/api/account/signup`,
      model,
      { withCredentials: true }
    );
  }

  // 3) is logged in
  async IsLoggedIn(): Promise<boolean> {
    if (this.USER !== null) {
      return true;
    }

    const res: UserModel | false = await firstValueFrom(
      this.http.get<UserModel>(
        `${enviroment.apiUrl}/api/account/isAuthenticted`,
        { withCredentials: true }
      )
    ).catch(() => false);

    if (res) {
      this.USER = res;
      return true;
    } else {
      this.USER = null;
      return false;
    }
  }
  // 4) log out
  Logout(): void {
    this.http
      .post(
        `${enviroment.apiUrl}/api/account/logout`,
        {},
        { withCredentials: true }
      )
      .subscribe({
        complete: () => {
          this.USER = null;
          this.router.navigate(['login']);
        },
      });
  }
}
