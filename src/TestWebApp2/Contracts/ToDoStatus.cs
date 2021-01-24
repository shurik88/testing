namespace TestWebApp2.Contracts
{
    /// <summary>
    ///     Статуст задания.
    /// </summary>
    public enum ToDoStatus
    {
        /// <summary>
        ///     Создано.
        /// </summary>
        Created = 0,

        /// <summary>
        ///    Начато 
        /// </summary>
        Started = 1,

        /// <summary>
        ///     Остановленно
        /// </summary>
        Paused = 2,

        /// <summary>
        ///     Отмененно
        /// </summary>
        Cancelled = 3,

        /// <summary>
        ///     Завершено
        /// </summary>
        Finished = 4
    }
}
