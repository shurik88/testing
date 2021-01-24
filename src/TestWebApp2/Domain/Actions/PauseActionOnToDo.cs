using TestWebApp2.Model;

namespace TestWebApp2.Domain.Actions
{
    /// <summary>
    ///     Остановка выполнения задания.
    /// </summary>
    public class PauseActionOnToDo : ActionOnToDo
    {
        /// <inheritdoc/>
        public override ToDoAction Action => ToDoAction.Pause;
    }
}
