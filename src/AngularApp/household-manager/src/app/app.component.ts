import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'household-manager';
 /**
  *
  */
 constructor(private auth:AuthService, private router:Router) {
  
  
 }
  logout() {
    this.auth.logout();
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }
}
