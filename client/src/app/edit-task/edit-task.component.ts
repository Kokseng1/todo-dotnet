import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-edit-task',
  standalone: true,
  imports: [],
  templateUrl: './edit-task.component.html',
  styleUrl: './edit-task.component.css',
})
export class EditTaskComponent {
  @Input() task = '';

  ngOnInit(): void {
    console.log(this.task);
  }
}
