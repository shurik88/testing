using System;

namespace TestWebApp2.Contracts
{
    /// <summary>
    /// Задание
    /// </summary>
    public class ToDoDto: EditToDoDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Статус задания.
        /// </summary>
        public ToDoStatus Status { get; set; }
    }
}
