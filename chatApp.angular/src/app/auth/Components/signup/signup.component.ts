import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { Router } from '@angular/router';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { CheckEmailService } from '../../Services/check-email.service';
import { SignupModel } from '../../../../Models/Users/SignupModel';

interface ISignupForm {
  FirstName: FormControl<string>;
  LastName: FormControl<string>;
  Email: FormControl<string>;
  Password: FormControl<string>;
  ConfirmPassword: FormControl<string>;
}

@Component({
  selector: 'app-signup',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
})
export class SignupComponent {
  /**
   *
   */
  SignupForm: FormGroup;

  constructor(
    private checkEmail: CheckEmailService,
    private auth: AuthService,
    private router: Router
  ) {
    this.SignupForm = new FormGroup<ISignupForm>(
      {
        FirstName: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
        LastName: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required],
        }),
        Email: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required, Validators.email],
          asyncValidators: [this.checkEmail.emailAsyncValidator()],
          updateOn: 'blur',
        }),
        Password: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required, Validators.minLength(6)],
        }),
        ConfirmPassword: new FormControl('', {
          nonNullable: true,
          validators: [Validators.required],
          updateOn: 'blur',
        }),
      },
      [this.CheckPassword('Password', 'ConfirmPassword')]
    );
  }

  get passwordMatchError() {
    return (
      this.SignupForm.getError('mismatch') &&
      this.SignupForm.get('ConfirmPassword')?.touched
    );
  }

  CheckPassword(source: string, target: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const sourceCtrl = control.get(source);
      const targetCtrl = control.get(target);

      return sourceCtrl && targetCtrl && sourceCtrl.value !== targetCtrl.value
        ? { mismatch: true }
        : null;
    };
  }

  onSubmit() {
    // console.log(this.SignupForm);
    if (this.SignupForm.valid) {
      this.signupFn();
    }
  }

  signupFn() {
    const formValues = this.SignupForm.getRawValue();
    const signupModel: SignupModel = {
      firstName: formValues.FirstName,
      lastName: formValues.LastName,
      email: formValues.Email,
      confirmPassword: formValues.ConfirmPassword,
      password: formValues.Password,
    };

    this.auth.Signup(signupModel).subscribe({
      next: () => {},
      error: (err) => {
        console.log(err);

        this.SignupForm.setErrors({
          0: err.error.errors || 'somthing went wrong try again later',
        });
      },
      complete: () => {
        this.router.navigate(['login']);
      },
    });
  }
}
