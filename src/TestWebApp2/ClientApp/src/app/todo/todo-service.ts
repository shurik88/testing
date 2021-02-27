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

  getToDo(id: string) {
    return this.http.get(this.url + '/' + id);
  }

  createToDo(todo: EditToDo) {
    if (todo.tags != null && todo.tags.length === 0) {
      todo.tags = null;
    }
    return this.http.post(`${this.url}`, todo);
  }

  updateToDo(id: string, todo: EditToDo) {
    if (todo.tags != null && todo.tags.length === 0) {
      todo.tags = null;
    }
    return this.http.put(`${this.url}/${id}`, todo);
  }

  deleteToDo(id: string) {
    return this.http.delete(`${this.url}/${id}`);
  }
}
