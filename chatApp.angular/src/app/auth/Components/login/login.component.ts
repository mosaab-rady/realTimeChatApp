import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { LoginModel } from '../../../../Models/Users/LoginModel';
import { AuthService } from '../../Services/auth.service';
import { UserModel } from '../../../../Models/Users/UserModel';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

interface ILoginForm {
  Email: FormControl<string>;
  Password: FormControl<string>;
}

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  /**
   *
   */
  constructor(private auth: AuthService, private router: Router) {}

  LoginForm = new FormGroup<ILoginForm>({
    Email: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.email],
    }),
    Password: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required],
    }),
  });

  onSubmit() {
    if (this.LoginForm.valid) {
      this.LoginFn();
    }
  }

  LoginFn() {
    const loginModel: LoginModel = {
      email: this.LoginForm.value.Email!,
      password: this.LoginForm.value.Password!,
    };

    this.auth.Login(loginModel).subscribe({
      next: (userModel: UserModel) => {
        // console.log('data', userModel);
        this.auth.USER = userModel;
      },
      error: (err) => {
        // console.log('error', err);
        this.LoginForm.setErrors({
          0: err.error?.detail || 'somthing went wrong try again later',
        });
      },
      complete: () => {
        // console.log('complete');
        this.router.navigate(['']);
      },
    });
  }
}
