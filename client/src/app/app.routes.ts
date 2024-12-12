import { Routes } from '@angular/router';
import { LoginComponent } from './login-form/login-form.component';
import { TaskListComponent } from './task-list/task-list.component';
import { RegisterFormComponent } from './register-form/register-form.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'tasks', component: TaskListComponent },
  { path: 'register', component: RegisterFormComponent },
  { path: 'logout', component: LoginComponent },
  { path: '', component: TaskListComponent },
];
