using System.Collections.Generic;

namespace TestWebApp2.Common
{
    /// <summary>
    /// Данные http запроса
    /// </summary>
    public class HttpData
    {
        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Путь запроса
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Тип данных запроса
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Схема
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Статуст код
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Метод запроса
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Протокол
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// Список заголовков с значениями
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Строка запроса
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Список идентичностей
        /// </summary>
        public IEnumerable<HttpClaimIdentity> Identities { get; set; }

        /// <summary>
        /// Данные запроса post
        /// </summary>
        public string ViewModel { get; set; }

    }

    /// <summary>
    /// Идентичность
    /// </summary>
    public class HttpClaimIdentity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список утверждений
        /// </summary>
        public IEnumerable<HttpClaimItem> Claims { get; set; }
    }


    /// <summary>
    /// Claim
    /// </summary>
    public class HttpClaimItem
    {
        /// <summary>
        /// Тип клайма 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Значение клайма
        /// </summary>
        public string Value { get; set; }
    }
}
