// src/app/services/auth.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  //private baseUrl = 'http://localhost:5101'; // Update with your actual Auth API URL
    private baseUrl = environment.api.auth; // 'http://localhost:5259'; // Update with your actual Auth API URL
  constructor(private http: HttpClient) {
    this.baseUrl = environment.api.auth;
  }

  login(username: string, password: string) {
    return this.http.post<{ token: string }>(`${this.baseUrl}/auth/login`, {
      username,
      password
    });
  }

  logout() {
    localStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }
}
