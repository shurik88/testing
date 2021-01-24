using TestWebApp2.Model;

namespace TestWebApp2.Domain.Actions
{
    /// <summary>
    ///     Начать выполнять задание.
    /// </summary>
    public class StartActionOnToDo : ActionOnToDo
    {
        /// <inheritdoc/>
        public override ToDoAction Action => ToDoAction.Start;
    }
}
