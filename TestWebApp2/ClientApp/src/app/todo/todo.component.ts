import { Component } from '@angular/core';
import { ToDoService } from './todo-service';
import * as moment from 'moment';
//import { DatePickerComponent } from 'ng2-date-picker';  

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  providers: [ToDoService]
})
export class ToDoComponent {
  todo: ToDo = { id: null, text: "", priority: 1, deadline: null, assignedTo: null, tags: [] };
  todos: ToDo[];              
  tableMode = true;          // табличный режим
  moment: any = moment;

  public tags: Array<any> = [];

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
    if (this.tags.length > 0) {
      this.todo.tags = this.tags.map(x => x.displayValue);
    }
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
    
    if (p.tags == null) {
      this.tags = [];
    } else {
      this.tags = p.tags.map(x => { return { displayValue: x }; });
    }
  }

  cancel() {
    this.todo = { id: null, text: "", priority: 1, deadline: null, assignedTo: null, tags: [] };
    this.tableMode = true;
    this.tags = [];
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
