import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiURL = 'http://localhost:5194/api/UserTask';

  constructor(private http: HttpClient) {}

  getAllTasks(): Observable<any> {
    return this.http.get(`${this.apiURL}`);
  }

  updateTaskStatus(taskId: number, status: boolean): Observable<any> {
    return this.http.put(`${this.apiURL}/${taskId}`, { status });
  }

  deleteTask(id: any) {
    return this.http.delete(`${this.apiURL}/${id}`);
  }

  createTask(name: string) {
    return this.http.post(`${this.apiURL}/add`, { name: name, status: false });
  }
}
