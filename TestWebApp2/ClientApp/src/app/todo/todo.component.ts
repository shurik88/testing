import { Component } from '@angular/core';
import { ToDoService } from './todo-service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  providers: [ToDoService]
})
export class ToDoComponent {
  todo: ToDo = { id: null, text: "", priority: 1 };
  todos: ToDo[];              
  tableMode = true;          // табличный режим


  constructor(private service: ToDoService) {
  }

  ngOnInit() {
    this.loadToDos();    // загрузка данных при старте компонента  
  }
  // получаем данные через сервис
  loadToDos() {
    this.service.getToDos()
      .subscribe((data: ToDo[]) => this.todos = data);
  }
  // сохранение данных
  save() {
    if (this.todo.id === null) {
      this.service.createToDo(this.todo)
        .subscribe((data: ToDo) => this.todos.push(data));
    } else {
      this.service.updateToDo(this.todo)
        .subscribe(() => this.loadToDos());
    }
    this.cancel();
  }

  editToDo(p: ToDo) {
    this.todo = p;
  }

  cancel() {
    this.todo = { id: null, text: "", priority: 1 };
    this.tableMode = true;
  }

  delete(p: ToDo) {
    this.service.deleteToDo(p.id)
      .subscribe(() => this.loadToDos());
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }
}
