import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TaskService } from '../task.service';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-task-button',
  standalone: true,
  imports: [NgIf, FormsModule, NgFor],
  templateUrl: './create-task-button.component.html',
  styleUrl: './create-task-button.component.css',
})
export class CreateTaskButtonComponent {
  @Input() categories: any[] = [];
  filteredCategories: string[] = [];
  categoryNames: string[] = [];

  @Output() sendTasksToParent: EventEmitter<any[]> = new EventEmitter();

  showPanel: boolean = false;
  newTaskName: string = '';
  categoryPickedName: string = '';

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.updateCategoryNames();
  }

  ngOnChanges(): void {
    this.updateCategoryNames();
  }

  updateCategoryNames(): void {
    this.categoryNames = this.categories.map((category) => category.name);
    this.filteredCategories = this.categoryNames;
  }

  toggleCreateTaskPanel(): void {
    this.showPanel = !this.showPanel;
  }

  createTask(): void {
    if (this.newTaskName.trim()) {
      this.taskService.createTask(this.newTaskName, this.categoryPickedName).subscribe({
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

  onSearch(query: string): void {
    this.filteredCategories = this.categoryNames.filter((category) =>
      category.toLowerCase().includes(query.toLowerCase())
    );
  }
}
