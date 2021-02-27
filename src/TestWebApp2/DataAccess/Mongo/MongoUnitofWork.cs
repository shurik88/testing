using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApp2.DataAccess.Mongo
{
    /// <summary>
    ///     Единица работы, реализованная на mongo.
    /// </summary>
    internal class MongoUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MongoUrl _mongoUrl;
        private readonly MongoClient _client;

        /// <summary>
        ///     Создание экземпляра класса <seealso cref="MongoUnitOfWork"/>.
        /// </summary>
        /// <param name="connectionString">Строка подключения к бд</param>
        /// <param name="sessionOptions">Параметры сессии</param>
        public MongoUnitOfWork(string connectionString, ClientSessionOptions sessionOptions = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _mongoUrl = new MongoUrl(connectionString);
            _client = new MongoClient(_mongoUrl);
            Session = _client.StartSession();
        }

        /// <summary>
        /// Сессия
        /// </summary>
        public IClientSessionHandle Session { get; }


        IMongoClient Client { get; }

        #region IDisposable

        /// <inheritdoc/>
        public void Dispose()
        {
            if (Session.IsInTransaction)
                Session.AbortTransaction();
        }

        #endregion

        #region IUnitOfWork

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            Session.StartTransaction();
        }

        /// <inheritdoc/>
        public async Task CommitAsync(CancellationToken token = default(CancellationToken))
        {
            await Session.CommitTransactionAsync(token);
        }

        /// <inheritdoc/>
        public async Task RollbackAsync(CancellationToken token = default(CancellationToken))
        {
            await Session.AbortTransactionAsync(token);
        }

        #endregion
    }
}
