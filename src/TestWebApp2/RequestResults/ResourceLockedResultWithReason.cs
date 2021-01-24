namespace TestWebApp2.RequestResults
{
    /// <summary>
    /// Ответ http в случае обращения к ранее доступному ресурсу, который заблокирован
    /// </summary>
    public class ResourceLockedResultWithReason: StatusCodeResultWithBody
    {
        /// <summary>
        /// Создание экземпляра класса <seealso cref="ResourceLockedResultWithReason"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public ResourceLockedResultWithReason(string message) : base(423, message, message)
        {

        }
    }
}
