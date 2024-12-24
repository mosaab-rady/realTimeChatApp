import { Routes } from '@angular/router';
import { LoginComponent } from './auth/Components/login/login.component';
import { SignupComponent } from './auth/Components/signup/signup.component';
import { authGuard } from './auth/Services/auth.guard';

export const routes: Routes = [
  { path: 'login', title: 'Login | Chat App', component: LoginComponent },
  { path: 'signup', title: 'Sign up | Chat App', component: SignupComponent },
  {
    path: '',
    canActivate: [authGuard],
    loadChildren: () => import('./page/page.module').then((m) => m.PageModule),
  },
];
