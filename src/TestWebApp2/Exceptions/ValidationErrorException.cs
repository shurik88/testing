using System;

namespace TestWebApp2.Exceptions
{
    /// <summary>
    /// Ошибка предметной области
    /// </summary>
    public class ValidationErrorException : Exception
    {
        /// <summary>
        /// Создание экземпляра класс <seealso cref="ValidationErrorException"/>
        /// </summary>
        /// <param name="error">Сообщение по ошибке</param>
        public ValidationErrorException(string error) : this(error, null)
        {
        }

        /// <summary>
        /// Создание экземпляра класс <seealso cref="ValidationErrorException"/>
        /// </summary>
        /// <param name="error">Сообщение по ошибке</param>
        /// <param name="data">Данные по ошибке</param>
        public ValidationErrorException(string error, object data) : base("Domain model error")
        {
            Error = error;
            InvalidData = data;
        }

        /// <summary>
        /// Данные по ошибке
        /// </summary>
        public object InvalidData { get; private set; }

        /// <summary>
        /// Сообщение по ошибке
        /// </summary>
        public string Error { get; private set; }

    }
}
