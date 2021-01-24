using System;
using System.Linq;
using TestWebApp2.Model;

namespace TestWebApp2.Domain.Actions
{
    /// <summary>
    ///     Завершение задания.
    /// </summary>
    public class FinishActionOnToDo : ActionOnToDo
    {
        /// <inheritdoc/>
        public override ToDoAction Action => ToDoAction.Finish;

        /// <inheritdoc/>
        public override void PostAction(ToDo todo)
        {
            var start = todo.Log.First().Date;
            var currentDate = DateTime.UtcNow;
            var totalDurationInMinutes = (currentDate-start).TotalMinutes;
            if(todo.Log.Any(x => x.Action == ToDoAction.Pause))
            {
                var pauseActions = todo.Log
                    .Where(x => x.Action == ToDoAction.Pause || x.Action == ToDoAction.Resume).ToList();

                for(var i = 0; i < pauseActions.Count - 1; i+=2)
                    totalDurationInMinutes -= (pauseActions[i + 1].Date - pauseActions[i].Date).TotalMinutes;
            }

            todo.FactDuration = (int)totalDurationInMinutes;
        }
    }
}
