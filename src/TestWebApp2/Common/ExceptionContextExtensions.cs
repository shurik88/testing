using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace TestWebApp2.Common
{
    /// <summary>
    /// Методы расширения для обработки исключений <seealso cref="ExceptionContext"/>
    /// </summary>
    public static class ExceptionContextExtensions
    {
        /// <summary>
        /// Получение данных http запроса
        /// </summary>
        /// <param name="context">Контекст исключения</param>
        /// <returns></returns>
        public static HttpData GetHttpData(this ExceptionContext context)
        {
            return context.HttpContext.GetHttpData();
        }

        /// <summary>
        /// Получение данных http запроса
        /// </summary>
        /// <param name="context">Контекст http</param>
        /// <returns></returns>
        public static HttpData GetHttpData(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            return new HttpData
            {
                ContentType = context.Request.ContentType,
                Host = context.Request.Host.HasValue ? context.Request.Host.Value : "",
                Method = context.Request.Method,
                Path = context.Request.Path,
                Protocol = context.Request.Protocol,
                Query = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : "",
                Scheme = context.Request.Scheme,
                StatusCode = context.Response.StatusCode,
                Headers = context.Request.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value)),
                Identities = context.User.Identities.Select(
                        x => new HttpClaimIdentity
                        {
                            Name = x.Name,
                            Claims = x.Claims.Select(y => new HttpClaimItem { Type = y.Type, Value = y.Value })
                        })
            };
        }
    }
}
