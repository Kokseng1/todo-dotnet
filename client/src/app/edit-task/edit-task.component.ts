import { Component, Input } from '@angular/core';
import { TaskService } from '../task.service';
import { FormsModule } from '@angular/forms';
import { response } from 'express';

@Component({
  selector: 'app-edit-task',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-task.component.html',
  styleUrl: './edit-task.component.css',
})
export class EditTaskComponent {
  constructor(private taskService: TaskService) {}
  @Input() task: any = null;
  @Input() categories: any[] = [];
  categoryNames: any[] = [];
  filteredCategories: any[] = [];
  categoryPickedName: string = '';

  ngOnInit(): void {
    this.updateCategoryNames();
    this.categoryPickedName = this.task.categoryName;
  }

  ngOnChanges(): void {
    this.updateCategoryNames();
  }

  updateCategoryNames(): void {
    this.categoryNames = this.categories.map((category) => category.name);
    this.filteredCategories = this.categoryNames;
  }

  onSearch(query: string): void {
    this.filteredCategories = this.categoryNames.filter((category) =>
      category.toLowerCase().includes(query.toLowerCase())
    );
  }

  submitEdit(): void {
    console.log(this.categoryPickedName);
    this.taskService
      .updateTaskStatus({
        id: this.task.id,
        name: this.task.name,
        status: this.task.status,
        categoryName: this.categoryPickedName,
      })
      .subscribe({ next: (response) => {} });
  }
}
