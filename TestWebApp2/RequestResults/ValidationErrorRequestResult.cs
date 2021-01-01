namespace TestWebApp2.RequestResults
{
    /// <summary>
    /// Результат ответа http в случае ошибки предметной области
    /// </summary>
    public class ValidationErrorRequestResult : StatusCodeResultWithBody
    {
        /// <summary>
        /// Создание экземпляра класса <seealso cref="ValidationErrorRequestResult"/>
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <param name="message">Сообщение</param>
        public ValidationErrorRequestResult(string error, string message): base(409, message, error)
        {

        }
    }
}
