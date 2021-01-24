using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TestWebApp2.Exceptions;

namespace TestWebApp2.Interceptors
{
    /// <summary>
    ///     Глобальный перехватчик запросов.
    /// </summary>
    /// <remarks>
    ///     Для выполнения глобальной обработки исключений.
    /// </remarks>
    internal class ExceptionInterceptor: Interceptor
    {
        private readonly ILogger<ExceptionInterceptor> _logger;
        private readonly IHostEnvironment _environment;

        /// <summary>
        ///     Создание экземпляра класса <see cref="ExceptionInterceptor"/>.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="environment">Окружение</param>
        public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger, IHostEnvironment environment)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        /// <inheritdoc/>
        public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
           TRequest request,
           ServerCallContext context,
           UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (ValidationErrorException e)
            {
                _logger.LogWarning(409, "Validation error: {@message} with data: {@data}", e.Error, e.InvalidData ?? "");
                throw new RpcException(new Status(StatusCode.FailedPrecondition, e.Error));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occured when calling {context.Method}");
                throw new RpcException(new Status(StatusCode.Internal, _environment.IsDevelopment() ? e.Message : "Error occurred, see logs"));
            }

        }
    }
}
