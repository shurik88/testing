using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestWebApp2.Contracts;
using TestWebApp2.Domain.Actions;
using TestWebApp2.Exceptions;
using TestWebApp2.Model;

namespace TestWebApp2.Domain
{
    /// <summary>
    ///     Исполнение задания (машина состояний).
    /// </summary>
    internal class ToDoExecutionWorkflow
    {
        private ToDo _todo;
        private readonly IDictionary<ToDoStatus, Dictionary<ToDoAction, ToDoTransition>> _workflow;

        private class Transition<TEntity, TAction, TStatus>
        {
            public Func<TEntity, bool> Guard { get; set; }
            public Func<TEntity, TStatus> Action { get; set; }
        }

        private class ToDoTransition : Transition<ToDo, ToDoAction, ToDoStatus>
        {
        }

        /// <summary>
        ///     Создание экземпляра класса <see cref="ToDoExecutionWorkflow"/>.
        /// </summary>
        public ToDoExecutionWorkflow()
        {
            _workflow = new Dictionary<ToDoStatus, Dictionary<ToDoAction, ToDoTransition>>
            {
                {
                    ToDoStatus.Created,
                    new Dictionary<ToDoAction, ToDoTransition>
                    {
                        { ToDoAction.Start, new ToDoTransition { Action = (_) => ToDoStatus.Started } }
                    }
                },
                {
                    ToDoStatus.Started,
                    new Dictionary<ToDoAction, ToDoTransition>
                    {
                        { ToDoAction.Pause, new ToDoTransition { Action = (_) => ToDoStatus.Paused } },
                        { ToDoAction.Cancel, new ToDoTransition { Action = (_) => ToDoStatus.Cancelled } },
                        { ToDoAction.Finish, new ToDoTransition { Action = (_) => ToDoStatus.Finished } }
                    }
                },
                {
                    ToDoStatus.Paused,
                    new Dictionary<ToDoAction, ToDoTransition>
                    {
                        { ToDoAction.Resume, new ToDoTransition { Action = (_) => ToDoStatus.Started } }
                    }
                }
            };
        }

        /// <summary>
        ///     Инициализация.
        /// </summary>
        /// <param name="todo">Задание</param>
        public void Init(ToDo todo)
        {
            _todo = todo;
        }

        /// <summary>
        ///     Выполнить действие над заданием.
        /// </summary>
        /// <param name="action">действие</param>
        /// <exception cref="ValidationErrorException">При нарушении правил конечного автомата</exception>
        public void DoAction(ActionOnToDo action)
        {
            action.DoAction(_todo);

            Fire(action.Action);

            AddLogEntry(action.Action, action.LogDetails);

            action.PostAction(_todo);
        }

        private void AddLogEntry(ToDoAction action, object logDetails)
        {
            if (_todo.Log == null)
                _todo.Log = new Collection<ToDoActionLog>();

            _todo.Log.Add(new ToDoActionLog { Action = action, Date = DateTime.UtcNow, Details = logDetails });
        }

        /// <summary>
        ///     Переход состояния
        /// </summary>
        /// <param name="action">Действие</param>
        private void Fire(ToDoAction action)
        {
            ValidateMachineState();

            if (!_workflow.ContainsKey(_todo.Status))
                throw new ValidationErrorException("Final call state", new { CallStatus = _todo.Status, Action = action });

            if (!_workflow[_todo.Status].ContainsKey(action))
                throw new ValidationErrorException("Invalid action for todo", new { CallStatus = _todo.Status, Action = action });

            if (_workflow[_todo.Status][action].Guard != null && !_workflow[_todo.Status][action].Guard(_todo))
                throw new ValidationErrorException("Invalid action for todo", new { Call = _todo, Action = action });

            var todoAction = _workflow[_todo.Status][action];

            _todo.Status = todoAction.Action(_todo);
        }

        private void ValidateMachineState()
        {
            if (_todo == null)
                throw new InvalidOperationException("Init todo before");
        }
    }
}
