import { Component, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css'],
  standalone: true,
  imports: [FormsModule],
})
export class RegisterFormComponent {
  email: string = '';
  username: string = '';
  password: string = '';

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    const registerPayload = {
      email: this.email,
      username: this.username,
      password: this.password,
    };

    this.http
      .post('http://localhost:5194/api/account/register', registerPayload, {
        observe: 'response',
      })
      .subscribe({
        next: (response) => {
          if (response.status === 201) {
            console.log('Register successful');
            this.router.navigate(['/login']);
          }
        },
        error: (error) => {
          console.error('Register failed:', error);
        },
      });
  }
}
