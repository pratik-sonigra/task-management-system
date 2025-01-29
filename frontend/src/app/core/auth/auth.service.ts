import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl; 
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/users/login`, { username, password })
      .pipe(
        tap((response) => {
          if (response && response.token) {
            localStorage.setItem('token', response.token); // Save JWT to localStorage
          }
        })
      );
  }

  register(userData: { username: string; email: string; password: string; roleId: number }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/users/register`, userData);
  }

  getRoles(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/roles`); 
  }

  logout() {
    localStorage.removeItem('token'); // Remove token on logout
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token'); // Check if token exists
  }

  getCurrentUser(): any {
    const token = localStorage.getItem('token');
    if (!token) {
      return null;
    }
  
    const decodedToken = this.jwtHelper.decodeToken(token);
    return {
      id: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
      username: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
    };
  }
}