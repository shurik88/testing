using System;
using TestWebApp2.Model;

namespace TestWebApp2.Contracts
{
    /// <summary>
    ///     Действие при выполнении дела.
    /// </summary>
    public class ToDoActionDto
    {
        /// <summary>
        ///     Дата действия.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Действие.
        /// </summary>
        public ToDoAction Action { get; set; }
    }
}
