
namespace TestWebApp2.RequestResults
{
    /// <summary>
    /// Результат ответа http в случае непрохождения авторизации
    /// </summary>
    public class UnauthorizedResultWithMessage: StatusCodeResultWithBody
    {
        /// <summary>
        /// Создание экземпляра класса <seealso cref="UnauthorizedResultWithMessage"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public UnauthorizedResultWithMessage(string message) : base(401, message)
        {
        }
    }
}
