using TestWebApp2.Model;

namespace TestWebApp2.Domain.Actions
{
    /// <summary>
    ///     Возобновление выполнения задания.
    /// </summary>
    public class ResumeActionOnToDo : ActionOnToDo
    {
        /// <inheritdoc/>
        public override ToDoAction Action => ToDoAction.Resume;
    }
}
