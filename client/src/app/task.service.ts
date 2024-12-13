import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

export interface Task {
  id: number;
  name: string;
  categoryId: number;
  status: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private taskApiURL = 'http://localhost:5194/api/UserTask';
  private categoryApiURL = 'http://localhost:5194/api/Category';

  constructor(private http: HttpClient) {}

  getAllTasks(): Observable<any> {
    return this.http.get(`${this.taskApiURL}`);
  }

  updateTaskStatus(task: Task): Observable<any> {
    return this.http.put(`${this.taskApiURL}/${task.id}`, task);
  }

  deleteTask(id: any) {
    return this.http.delete(`${this.taskApiURL}/${id}`);
  }

  createTask(taskName: string, categoryName: string) {
    return this.http.post(`${this.taskApiURL}`, {
      name: taskName,
      categoryName: categoryName,
    });
  }

  getAllCategories(): Observable<any> {
    return this.http.get(`${this.categoryApiURL}`);
  }
}
