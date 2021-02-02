using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TestWebApp2.Infrastructure.Features
{
    /// <summary>
    ///    workaround see https://docs.microsoft.com/ru-ru/aspnet/core/grpc/browser?view=aspnetcore-5.0
    /// </summary>
    public class HttpSysWorkaroundHttpResponseFeature : IHttpResponseFeature
    {
        private readonly IHttpResponseFeature _inner;

        public HttpSysWorkaroundHttpResponseFeature(IHttpResponseFeature inner)
        {
            _inner = inner;
            _inner.OnStarting(o =>
            {
                HasStarted = true;
                return Task.CompletedTask;
            }, null);
        }

        [Obsolete]
        public Stream Body
        {
            get => _inner.Body;
            set { _inner.Body = value; }
        }
        public bool HasStarted { get; private set; }
        public IHeaderDictionary Headers
        {
            get => _inner.Headers;
            set { _inner.Headers = value; }
        }
        public string ReasonPhrase
        {
            get => _inner.ReasonPhrase;
            set { _inner.ReasonPhrase = value; }
        }
        public int StatusCode
        {
            get => _inner.StatusCode;
            set { _inner.StatusCode = value; }
        }

        public void OnCompleted(Func<object, Task> callback, object state)
        {
            _inner.OnCompleted(callback, state);
        }

        public void OnStarting(Func<object, Task> callback, object state)
        {
            _inner.OnStarting(callback, state);
        }
    }
}
