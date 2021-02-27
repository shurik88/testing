namespace TestWebApp2.RequestResults
{
    /// <summary>
    /// Ответ http в случае невозможности выполить действие, 
    /// например пользователь прошел авторизацию, но нет доступа к данному объекту
    /// </summary>
    public class ForbiddenResult : StatusCodeResultWithBody
    {
        /// <summary>
        /// Создание экземпляра класса <seealso cref="ForbiddenResult"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public ForbiddenResult(string message): base(403, message, message) {  }
    }
}
