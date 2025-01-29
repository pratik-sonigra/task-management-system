import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/auth/auth.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    CommonModule
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registrationForm: FormGroup;
  roles: any[] = [];
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registrationForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      roleId: ['', Validators.required], // Use roleId instead of role
    });
  }

  ngOnInit(): void {
    this.fetchRoles();
  }

  toggleForm() {
    this.router.navigate(['/auth/login']);
  }

  fetchRoles(): void {
    this.authService.getRoles().subscribe(
      (roles) => {
        this.roles = roles;
      },
      (error) => {
        console.error('Error fetching roles:', error);
      }
    );
  }

  onSubmit() {
    if (this.registrationForm.invalid) return;

    const { username, email, password, roleId } = this.registrationForm.value;

    this.authService.register({ username, email, password, roleId }).subscribe(
      () => {
        this.successMessage = 'Registration successful! You can now log in.';
        this.errorMessage = '';
        this.registrationForm.reset();
        this.router.navigate(['/auth/login']);
      },
      (error) => {
        console.error('Registration error:', error);
        this.errorMessage = 'Registration failed. Please try again.';
        this.successMessage = '';
      }
    );
  }
}