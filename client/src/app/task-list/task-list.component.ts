import { Component } from '@angular/core';
import { TaskService } from '../task.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { LogoutButtonComponent } from '../logout-button/logout-button.component';
import { FormsModule } from '@angular/forms';
import { CreateTaskButtonComponent } from '../create-task-button/create-task-button.component';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [
    HttpClientModule,
    CommonModule,
    LogoutButtonComponent,
    FormsModule,
    CreateTaskButtonComponent,
  ],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css',
})
export class TaskListComponent {
  tasks: any[] = [];
  isLoading: boolean = true;
  confirmDelete: Number = -1;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.getTasks();
  }

  getTasks(): void {
    this.taskService.getAllTasks().subscribe((data) => {
      this.tasks = [...data];
      this.isLoading = false;
    });
  }

  toggleTaskStatus(task: any): void {
    this.taskService
      .updateTaskStatus(task.id, task.status)
      .subscribe((data) => {
        this.getTasks();
      });
  }

  delete(task: any): void {
    if (this.confirmDelete == task.id) {
      this.taskService.deleteTask(task.id).subscribe((data) => {
        this.getTasks();
      });
      this.confirmDelete = -1;
    } else {
      this.confirmDelete = task.id;
    }
  }

  cancelDelete(): void {
    this.confirmDelete = -1;
  }

  updateTasksFromChild(data: any[]): void {
    this.tasks = data;
  }
}
