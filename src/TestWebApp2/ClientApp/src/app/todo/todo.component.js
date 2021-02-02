"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ToDoComponent = exports.CustomAdapter = exports.NgbDateMomentAdapter = void 0;
var core_1 = require("@angular/core");
var todo_service_1 = require("./todo-service");
var moment = require("moment");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var core_2 = require("@angular/core");
var todoStatus_1 = require("./todoStatus");
var editToDoVaidator_1 = require("./editToDoVaidator");
var todoExecution_pb_service_1 = require("../generated/todoExecution_pb_service");
var todoExecution_pb_1 = require("../generated/todoExecution_pb");
var grpc_web_1 = require("@improbable-eng/grpc-web");
//import { } from './todoStatus';
//import { DatePickerComponent } from 'ng2-date-picker';
var NgbDateMomentAdapter = /** @class */ (function (_super) {
    __extends(NgbDateMomentAdapter, _super);
    function NgbDateMomentAdapter() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    NgbDateMomentAdapter.prototype.fromModel = function (date) {
        if (!date) {
            return null;
        }
        return { year: date.year(), month: date.month(), day: date.day() };
    };
    NgbDateMomentAdapter.prototype.toModel = function (date) {
        if (!date) {
            return null;
        }
        return moment(date.year + '-' + date.month + '-' + date.day, 'YYYY-MM-DD');
    };
    NgbDateMomentAdapter = __decorate([
        core_2.Injectable()
    ], NgbDateMomentAdapter);
    return NgbDateMomentAdapter;
}(ng_bootstrap_1.NgbDateAdapter));
exports.NgbDateMomentAdapter = NgbDateMomentAdapter;
var CustomAdapter = /** @class */ (function (_super) {
    __extends(CustomAdapter, _super);
    function CustomAdapter() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.DELIMITER = '-';
        return _this;
    }
    CustomAdapter.prototype.fromModel = function (value) {
        if (value) {
            var tIndex = value.indexOf('T');
            var date = (tIndex === -1 ? value : value.substring(0, tIndex)).split(this.DELIMITER);
            return {
                day: parseInt(date[2], 10),
                month: parseInt(date[1], 10),
                year: parseInt(date[0], 10)
            };
        }
        return null;
    };
    CustomAdapter.prototype.toModel = function (date) {
        return date ? date.year + this.DELIMITER + date.month + this.DELIMITER + date.day : null;
    };
    CustomAdapter = __decorate([
        core_2.Injectable()
    ], CustomAdapter);
    return CustomAdapter;
}(ng_bootstrap_1.NgbDateAdapter));
exports.CustomAdapter = CustomAdapter;
var ToDoComponent = /** @class */ (function () {
    function ToDoComponent(service, dateAdapter) {
        this.service = service;
        this.dateAdapter = dateAdapter;
        this.validator = new editToDoVaidator_1.EditToDoValidator();
        this.todo = { id: null, text: "", priority: 1, deadline: null, assignedTo: null, tags: [], status: todoStatus_1.ToDoStatus.created };
        this.tableMode = true; // табличный режим
        this.moment = moment;
        this.tags = [];
        this.deadline = moment(); //NgbDateStruct = { year: (moment()).year(), month: moment().month(), day: moment().day() };
    }
    ToDoComponent.prototype.onDateSelect = function (date) {
    };
    ToDoComponent.prototype.ngOnInit = function () {
        this.loadToDos(); // загрузка данных при старте компонента  
    };
    // получаем данные через сервис
    ToDoComponent.prototype.loadToDos = function () {
        var _this = this;
        this.service.getToDos()
            .subscribe(function (data) { return _this.todos = data; });
    };
    // сохранение данных
    ToDoComponent.prototype.save = function () {
        var _this = this;
        this.todo.tags = this.tags.length > 0 ? this.tags.map(function (x) { return x.displayValue; }) : null;
        this.validationResult = this.validator.validate(this.todo);
        if (this.validationResult.isInvalid()) {
            console.log(this.validationResult.getFailureMessages());
            return;
        }
        //this.todo
        if (this.todo.id === null) {
            this.service.createToDo(this.todo)
                .subscribe(function (data) { return _this.todos.push(data); });
        }
        else {
            this.service.updateToDo(this.todo.id, this.todo)
                .subscribe(function () { return _this.loadToDos(); });
        }
        this.cancel();
    };
    ToDoComponent.prototype.getStatusName = function (p) {
        if (!p)
            return '';
        switch (p.status) {
            case todoStatus_1.ToDoStatus.created:
                return "Создана";
            case todoStatus_1.ToDoStatus.started:
                return "Начата";
            case todoStatus_1.ToDoStatus.paused:
                return "Остановлена";
            case todoStatus_1.ToDoStatus.cancelled:
                return "Отменена";
            case todoStatus_1.ToDoStatus.finished:
                return "Завершена";
            default:
                throw new Error("unknown todo status: " + p.status);
        }
    };
    ToDoComponent.prototype.getStatusClassName = function (p) {
        if (!p)
            return '';
        switch (p.status) {
            case todoStatus_1.ToDoStatus.created:
                return "text-info";
            case todoStatus_1.ToDoStatus.started:
                return "text-primary";
            case todoStatus_1.ToDoStatus.paused:
                return "text-secondary";
            case todoStatus_1.ToDoStatus.cancelled:
                return "text-warning";
            case todoStatus_1.ToDoStatus.finished:
                return "text-success";
            default:
                throw new Error("unknown todo status: " + p.status);
        }
    };
    ToDoComponent.prototype.editToDo = function (p) {
        this.todo = p;
        if (p.tags == null) {
            this.tags = [];
        }
        else {
            this.tags = p.tags.map(function (x) { return { displayValue: x }; });
        }
    };
    ToDoComponent.prototype.cancel = function () {
        this.todo = { id: null, text: "", priority: 1, deadline: null, assignedTo: null, tags: [], status: todoStatus_1.ToDoStatus.created };
        this.tableMode = true;
        this.tags = [];
    };
    ToDoComponent.prototype.delete = function (p) {
        var _this = this;
        this.service.deleteToDo(p.id)
            .subscribe(function () { return _this.loadToDos(); });
    };
    ToDoComponent.prototype.startToDo = function (p) {
        var input = new todoExecution_pb_1.StartToDoRequest();
        input.setId(p.id);
        grpc_web_1.grpc.unary(todoExecution_pb_service_1.ToDoExecution.Start, {
            request: input,
            host: "https://localhost:5001",
            onEnd: function (res) {
                var status = res.status, message = res.message;
                if (status === grpc_web_1.grpc.Code.OK && message) {
                    var result = message.toObject();
                    console.log("success grpc!!!");
                    //this.countries = result.countriesList.map(country =>
                    //  <CountryModel>({
                    //    name: country.name,
                    //    description: country.description
                    //  }));
                }
            }
        });
    };
    ToDoComponent.prototype.add = function () {
        this.cancel();
        this.deadline = moment();
        this.tableMode = false;
    };
    ToDoComponent = __decorate([
        core_1.Component({
            selector: 'app-todo',
            templateUrl: './todo.component.html',
            providers: [todo_service_1.ToDoService, { provide: ng_bootstrap_1.NgbDateAdapter, useClass: CustomAdapter }]
        })
    ], ToDoComponent);
    return ToDoComponent;
}());
exports.ToDoComponent = ToDoComponent;
//# sourceMappingURL=todo.component.js.map