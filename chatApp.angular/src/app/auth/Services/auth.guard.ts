import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const isloggedIn = await authService.IsLoggedIn();
  if (isloggedIn) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
};
