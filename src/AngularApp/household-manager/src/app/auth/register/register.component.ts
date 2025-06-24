import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../auth.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  //standalone: true,
 // imports: [CommonModule, ReactiveFormsModule, RouterModule]
})
export class RegisterComponent {
  form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private auth: AuthService) {}

  register() {
    const { username, password } = this.form.value;
    this.auth.register(username!, password!).subscribe();
  }
}
