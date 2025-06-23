
import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-login',
  template: `
  <form (ngSubmit)="login()">
    <input [(ngModel)]="username" name="username" placeholder="Username" required>
    <input [(ngModel)]="password" name="password" type="password" placeholder="Password" required>
    <button type="submit">Login</button>
  </form>`
})
export class LoginComponent {
  username = '';
  password = '';
  constructor(private auth: AuthService, private router: Router) {}
  login() {
    this.auth.login(this.username, this.password)
      .subscribe(() => this.router.navigate(['/']));
  }
}
