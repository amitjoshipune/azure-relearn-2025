import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-home',
  template: `
  <h1>Welcome!</h1>
  <button (click)="logout()">Logout</button>
  <nav>
    <a routerLink="products">Products</a> |
    <a routerLink="todos">Todos</a> |
    <a routerLink="stock">Stock</a> |
    <a routerLink="requisition">Requisition</a>
  </nav>
  <router-outlet></router-outlet>`
})
export class HomeComponent {
  constructor(private auth: AuthService) {}
  logout() { this.auth.logout(); }
}
