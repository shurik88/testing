using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace TestWebApp2.RequestResults
{
    /// <summary>
    /// Возвращение ответа из контроллера вместе со статус кодом и телом
    /// </summary>
    public class StatusCodeResultWithBody : StatusCodeResult
    {
        private readonly object _body;
        private readonly string _reason;

        /// <summary>
        /// Список загловков
        /// </summary>
        public HeaderDictionary Headers { get; set; }
        
        /// <summary>
        /// Создание экземпляра класса <seealso cref="StatusCodeResultWithBody"/>
        /// </summary>
        /// <param name="statusCode">Код статуса</param>
        /// <param name="body">Тело</param>
        /// <param name="reason">Причина</param>
        public StatusCodeResultWithBody(int statusCode, object body, string reason = null) : base(statusCode)
        {
            _body = body;
            _reason = reason;
        }

        /// <inheritdoc/>
        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
        }

        /// <inheritdoc/>
        public async Task ExecuteResultAsyncOnHttpContextAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCode;
            context.Response.ContentType = "application/json";
            if (Headers != null)
            {
                foreach (var header in Headers.Keys)
                {
                    context.Response.Headers.Add(header, Headers[header]);
                }
            }
            if (!string.IsNullOrEmpty(_reason))
            {
                context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = _reason;
            }

            if(_body != null)
                await context.Response.WriteAsync(JsonConvert.SerializeObject(_body)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            await ExecuteResultAsyncOnHttpContextAsync(context.HttpContext);
        }
    }
}
