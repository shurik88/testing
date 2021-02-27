namespace TestWebApp2.DataAccess
{
    /// <summary>
    ///     Базовый интрфейс сущности
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        ///     Идентификактор сущности
        /// </summary>
        T Id { get; set; }
    }
}
