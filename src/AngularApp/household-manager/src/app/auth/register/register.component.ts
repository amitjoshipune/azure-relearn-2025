import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  template: `
  <form (ngSubmit)="register()">
    <input [(ngModel)]="model.username" name="username" placeholder="Username" required>
    <input [(ngModel)]="model.password" name="password" type="password" placeholder="Password" required>
    <button type="submit">Register</button>
  </form>`
})
export class RegisterComponent {
  model: any = {};
  constructor(private auth: AuthService, private router: Router) {}
  register() {
    this.auth.register(this.model).subscribe(() => this.router.navigate(['/login']));
  }
}
