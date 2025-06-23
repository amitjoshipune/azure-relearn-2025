import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  login(username: string, password: string) {
    return this.http.post<{ token: string }>(
      `${environment.api.auth}/login`, { username, password }
    ).pipe(tap(r => localStorage.setItem(environment.tokenKey, r.token)));
  }

  register(user: any) {
    return this.http.post(`${environment.api.auth}/register`, user);
  }

  logout() {
    localStorage.removeItem(environment.tokenKey);
  }

  get token() {
    return localStorage.getItem(environment.tokenKey);
  }

  get isLoggedIn(): boolean {
    return !!this.token;
  }
}
