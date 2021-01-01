using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TestWebApp2.Model
{
    /// <summary>
    ///     Лог событий(действий) по заданию.
    /// </summary>
    public class ToDoActionLog
    {
        /// <summary>
        ///     Дата действия.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Действие
        /// </summary>
        public ToDoAction Action { get; set; }

        /// <summary>
        ///     Детали.
        /// </summary>
        [BsonIgnoreIfNull]
        public object Details { get; set; }
    }
}
