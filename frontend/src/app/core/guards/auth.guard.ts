import { Injectable } from '@angular/core';
import {
  CanActivate,
  CanActivateChild,
  CanLoad,
  Router,
  Route,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private authService: AuthService, private router: Router) {}

  // Check if the user can activate the route
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkAccess(route);
  }

  // Check if the user can activate child routes
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.checkAccess(childRoute);
  }

  // Check if the user can load a module
  canLoad(
    route: Route
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const roles = route.data?.['roles'] as Array<string>;
    return this.checkRoleAccess(roles);
  }

  // Check access based on roles
  private checkAccess(route: ActivatedRouteSnapshot): boolean | UrlTree {
    const roles = route.data?.['roles'] as Array<string>;
    return this.checkRoleAccess(roles);
  }

  private checkRoleAccess(allowedRoles: Array<string>): boolean | UrlTree {
    if (!this.authService.isAuthenticated()) {
      // Redirect to login if not authenticated
      return this.router.createUrlTree(['/auth/login']);
    }

    const currentUser = this.authService.getCurrentUser();
    if (allowedRoles && !allowedRoles.includes(currentUser.role)) {
      // Redirect based on roles if access is restricted
      return currentUser.role === 'Admin' 
        ? this.router.createUrlTree(['/dashboard']) 
        : this.router.createUrlTree(['/tasks']);
    }
  
    return true; // Allow access if authenticated and role matches
  }
}