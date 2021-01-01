using TestWebApp2.Exceptions;
using TestWebApp2.Model;

namespace TestWebApp2.Domain.Actions
{
    /// <summary>
    /// Действие над заявкой
    /// </summary>
    public abstract class ActionOnToDo
    {
        /// <summary>
        /// Действие
        /// </summary>
        public abstract ToDoAction Action { get; }

        /// <summary>
        /// Метод, вызывающийся до совершения события, но после проверки возможности перехода
        /// </summary>
        /// <param name="todo">Задание</param>
        /// <exception cref="ValidationErrorException">При нарушении правил предметной области</exception>
        public virtual void DoAction(ToDo todo) { }

        /// <summary>
        /// Метод, вызывающийся после совершения события (статус задания может быть уже обновлен)
        /// </summary>
        /// <param name="todo">Задание</param>
        public virtual void PostAction(ToDo todo) { }

        /// <summary>
        /// Дополнительные детали для журнала событий
        /// </summary>
        public virtual object LogDetails => null;
    }
}
