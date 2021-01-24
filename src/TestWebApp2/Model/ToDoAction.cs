namespace TestWebApp2.Model
{
    /// <summary>
    ///     Событие задания.
    /// </summary>
    public enum ToDoAction
    {
        /// <summary>
        ///     Начать выполнять.
        /// </summary>
        Start = 0,

        /// <summary>
        ///     Временно остановить.
        /// </summary>
        Pause = 1,

        /// <summary>
        ///     Возобновить.
        /// </summary>
        Resume = 2,

        /// <summary>
        ///     Отклонить.
        /// </summary>
        Cancel = 3,

        /// <summary>
        ///     Завершить.
        /// </summary>
        Finish = 4
    }
}
