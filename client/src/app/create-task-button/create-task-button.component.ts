import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TaskService } from '../task.service';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-task-button',
  standalone: true,
  imports: [NgIf, FormsModule],
  templateUrl: './create-task-button.component.html',
  styleUrl: './create-task-button.component.css',
})
export class CreateTaskButtonComponent {
  @Output() sendTasksToParent: EventEmitter<any[]> = new EventEmitter();

  showPanel: boolean = false;
  newTaskName: string = '';

  constructor(private taskService: TaskService) {}

  toggleCreateTaskPanel(): void {
    this.showPanel = !this.showPanel;
  }

  createTask(): void {
    if (this.newTaskName.trim()) {
      this.taskService.createTask(this.newTaskName).subscribe({
        next: (response) => {
          this.newTaskName = '';
          this.showPanel = false;
          this.taskService.getAllTasks().subscribe((data) => {
            this.sendTasksToParent.emit(data);
          });
        },
        error: (err) => console.error('Error creating task:', err),
      });
    } else {
      alert('Task name cannot be empty');
    }
  }
}
