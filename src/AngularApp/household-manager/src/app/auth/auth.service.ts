import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  base = environment.api.auth;
  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.base}/auth/login`, { username, password }).pipe(
      tap(res => {
        localStorage.setItem('jwt', res.token);
        const returnUrl = this.router.routerState.snapshot.root.queryParams['returnUrl'] || '/dashboard';
        this.router.navigateByUrl(returnUrl);
      })
    );
  }

  register(username: string, password: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>('/api/auth/register', { username, password }).pipe(
      tap(() => {
        this.router.navigate(['/login']); // Optional: auto-login instead
      })
    );
  }

  logout() {
    localStorage.removeItem('jwt');
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('jwt');
  }
}
