import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout-button',
  standalone: true,
  imports: [],
  templateUrl: './logout-button.component.html',
  styleUrl: './logout-button.component.css',
})
export class LogoutButtonComponent {
  constructor(private http: HttpClient, private router: Router) {}

  logout() {
    if (typeof window !== 'undefined' && window.localStorage.getItem('token')) {
      window.localStorage.removeItem('token');
    }
    this.router.navigate(['/login']);
  }
}
