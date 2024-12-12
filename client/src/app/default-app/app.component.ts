import { Component, NgModule } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TaskListComponent } from '../task-list/task-list.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    TaskListComponent,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  // greeting = {};
  // constructor(private http: HttpClient) {
  //   http.get('resource').subscribe((data) => (this.greeting = data));
  // }

  title = 'client';
}
