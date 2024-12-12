import { Component, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css'],
  standalone: true,
  imports: [FormsModule],
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    const loginPayload = {
      username: this.username,
      password: this.password,
    };

    interface LoginResponse {
      token: string;
      id: Number;
    }

    this.http
      .post<LoginResponse>(
        'http://localhost:5194/api/account/login',
        loginPayload,
        {
          observe: 'response',
        }
      )
      .subscribe({
        next: (response) => {
          if (response.status === 200 && response.body?.token) {
            window.localStorage.setItem('token', response.body['token']);

            this.router.navigate(['/tasks']);
          }
        },
        error: (error) => {
          console.error('Login failed:', error);
          this.errorMessage = 'Invalid username or password';
        },
      });
  }

  register() {
    this.router.navigate(['/register']);
  }
}
