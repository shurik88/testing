import { Component } from '@angular/core';
import { ToDoService } from './todo-service';
import * as moment from 'moment';
import { NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Injectable } from '@angular/core';
//import { DatePickerComponent } from 'ng2-date-picker';


@Injectable()
export class NgbDateMomentAdapter extends NgbDateAdapter<moment.Moment> {

  fromModel(date: moment.Moment): NgbDateStruct {
    if (!date) {
      return null;
    }
    return { year: date.year(), month: date.month(), day: date.day() };
  }

  toModel(date: NgbDateStruct): moment.Moment {
    if (!date) {
      return null;
    }
    return moment(date.year + '-' + date.month + '-' + date.day, 'YYYY-MM-DD');
  }
}

@Injectable()
export class CustomAdapter extends NgbDateAdapter<string> {

  readonly DELIMITER = '-';

  fromModel(value: string | null): NgbDateStruct | null {
    if (value) {
      let tIndex = value.indexOf('T');

      let date = (tIndex === -1 ? value : value.substring(0, tIndex)).split(this.DELIMITER);
      return {
        day: parseInt(date[2], 10),
        month: parseInt(date[1], 10),
        year: parseInt(date[0], 10)
      };
    }
    return null;
  }

  toModel(date: NgbDateStruct | null): string | null {
    return date ? date.year + this.DELIMITER + date.month + this.DELIMITER + date.day : null;
  }
}

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  providers: [ToDoService, { provide: NgbDateAdapter, useClass: CustomAdapter }]
})
export class ToDoComponent {
  todo: ToDo = { id: null, text: "", priority: 1, deadline: null, assignedTo: null, tags: [] };
  todos: ToDo[];              
  tableMode = true;          // табличный режим
  moment: any = moment;

  public tags: Array<any> = [];
  public deadline: moment.Moment = moment(); //NgbDateStruct = { year: (moment()).year(), month: moment().month(), day: moment().day() };

  constructor(private service: ToDoService,
    private dateAdapter: NgbDateAdapter<string>) {
  }

  public onDateSelect(date: NgbDateStruct) {

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
    //this.todo
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
    this.deadline = moment();
    this.tableMode = false;
  }
}
