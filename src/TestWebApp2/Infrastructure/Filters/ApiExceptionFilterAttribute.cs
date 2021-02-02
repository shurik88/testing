using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using TestWebApp2.Common;
using TestWebApp2.Exceptions;
using TestWebApp2.RequestResults;

namespace TestWebApp2.Filters
{
    /// <summary>
    ///     Глобальный фильтр обработки исключений.
    /// </summary>
    internal class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<HttpData> _logger;

        /// <summary>
        /// Создание экземпляра класса <seealso cref="ApiExceptionFilterAttribute"/>
        /// </summary>
        /// <param name="httpLogger">Логгер</param>
        public ApiExceptionFilterAttribute(ILogger<HttpData> httpLogger)
        {
            _logger = httpLogger ?? throw new ArgumentNullException(nameof(httpLogger));
        }

        /// <inheritdoc/>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationErrorException validationExeption)
            {
                context.Result = new ValidationErrorRequestResult(validationExeption.Error, context.Exception.Message);
                var httpData = context.GetHttpData();
                _logger.LogWarning(409, "Validation error: {@message} with data: {@data}, http: {@Http}", validationExeption.Error, validationExeption.InvalidData ?? "", httpData);
                return;
            }

            _logger.LogError(500, context.Exception, "exception: {@Http}", context.GetHttpData());

            context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
