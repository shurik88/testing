namespace TestWebApp2.RequestResults
{
    /// <summary>
    /// Ответ http в случае невозможности найти запрашиваемый объект
    /// </summary>
    public class NotFoundResultWithReason: StatusCodeResultWithBody
    {
        /// <summary>
        /// Создание экземпляра класса <seealso cref="NotFoundResultWithReason"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public NotFoundResultWithReason(string message): base(404, message, message)
        {

        }
    }
}
