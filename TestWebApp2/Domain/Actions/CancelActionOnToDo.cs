using TestWebApp2.Model;

namespace TestWebApp2.Domain.Actions
{
    /// <summary>
    ///     Отмена исполнения задания.
    /// </summary>
    public class CancelActionOnToDo : ActionOnToDo
    {
        private readonly string _reason;

        /// <summary>
        ///     Создание экземпляра класса <see cref="CancelActionOnToDo"/>
        /// </summary>
        /// <param name="reason">Причина отклонения</param>
        public CancelActionOnToDo(string reason)
        {
            _reason = reason;
        }

        /// <inheritdoc/>
        public override ToDoAction Action => ToDoAction.Cancel;

        /// <inheritdoc/>
        public override void DoAction(ToDo todo)
        {
            todo.CancelReason = _reason;
        }
    }
}
