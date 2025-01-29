import { Component } from '@angular/core';
import { AuthService } from '../../../core/auth/auth.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss'],
  imports: [MatToolbarModule, MatSidenavModule, MatListModule, RouterModule, CommonModule],
})
export class SideNavComponent {
  currentUser: any;

  constructor(private authService: AuthService, private router: Router) {}

  isAdmin(): boolean {
    console.log('Checking isAdmin');
    this.currentUser = this.authService.getCurrentUser();
    return this.currentUser.role === 'Admin';
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login']);
  }
}
