import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ToDoService {

  private url = "/api/todos";

  constructor(private http: HttpClient) {
  }

  getToDos() {
    return this.http.get(this.url);
  }

  getToDo(id: number) {
    return this.http.get(this.url + '/' + id);
  }

  createToDo(todo: ToDo) {
    if (todo.tags != null && todo.tags.length === 0) {
      todo.tags = null;
    }
    return this.http.post(`${this.url}`, todo);
  }

  updateToDo(todo: ToDo) {
    if (todo.tags != null && todo.tags.length === 0) {
      todo.tags = null;
    }
    return this.http.put(`${this.url}/${todo.id}`, todo);
  }

  deleteToDo(id: string) {
    return this.http.delete(`${this.url}/${id}`);
  }
}
