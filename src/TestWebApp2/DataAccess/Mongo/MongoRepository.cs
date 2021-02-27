using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApp2.DataAccess.Mongo
{
    /// <summary>
    /// Репозитарий работы с сущностями, реализованный на mongo
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    internal class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly IMongoRepositoryStrategy _repositoryStrategy;

        /// <inheritdoc/>
        public IQueryable<TEntity> Entities => Collection.AsQueryable();

        /// <summary>
        /// Создание экземпляра класса <see cref="MongoRepository{TEntity, TKey}"/>
        /// </summary>
        /// <param name="connectionString">Строка подключения к бд</param>
        /// <param name="collectionName">Имя коллекции</param>
        public MongoRepository(string connectionString, string collectionName) :
            this(new MongoUrl(connectionString).DatabaseName, collectionName, new MongoClient(new MongoUrl(connectionString)))
        {
        }

        public MongoRepository(IMongoDatabase database, string collectionName, IClientSessionHandle session = null)
        {
            DataBase = database;
            Collection = DataBase.GetCollection<TEntity>(collectionName);
            if (session == null)
                _repositoryStrategy = new MongoRepositoryStrategyWithoutTransaction(Collection);
            else
                _repositoryStrategy = new MongoRepositoryStrategyWithTransaction(session, Collection);
        }

        private MongoRepository(string dbName, string collectionName, IMongoClient client, IClientSessionHandle session = null)
            : this(client.GetDatabase(dbName), collectionName, session)
        {
        }

        /// <summary>
        ///     Создание экземпляра класса <seealso cref="MongoRepository{T}"/>.
        /// </summary>
        /// <param name="session">Сессия</param>
        /// <param name="dbName">Имя базы данных</param>
        /// <param name="collectionName">Название коллекции</param>
        public MongoRepository(IClientSessionHandle session, string dbName, string collectionName) : 
            this(dbName, collectionName, session.Client, session)
        {
        }

        protected IMongoDatabase DataBase { get; }

        protected IMongoCollection<TEntity> Collection { get; }


        #region CRUD

        private interface IMongoRepositoryStrategy : IRepository<TEntity, TKey>
        {
        }

        private class MongoRepositoryStrategyWithoutTransaction : IMongoRepositoryStrategy
        {
            private readonly IMongoCollection<TEntity> _collection;

            /// <inheritdoc/>
            public IQueryable<TEntity> Entities => _collection.AsQueryable();

            /// <inheritdoc/>
            public MongoRepositoryStrategyWithoutTransaction(IMongoCollection<TEntity> collection)
            {
                _collection = collection ?? throw new ArgumentNullException(nameof(collection));
            }

            /// <inheritdoc/>
            public async Task AddAsync(TEntity entity, CancellationToken token = default)
            {
                await _collection.InsertOneAsync(entity, new InsertOneOptions { BypassDocumentValidation = false }, token);
            }

            /// <inheritdoc/>
            public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
            {
                if (entities.Any())
                    await _collection.InsertManyAsync(entities, null, token);
            }

            /// <inheritdoc/>
            public async Task ClearAsync(CancellationToken token = default)
            {
                await _collection.DeleteManyAsync(x => true, token);
            }

            /// <inheritdoc/>
            public async Task DeleteAsync(TEntity entity, CancellationToken token = default)
            {
                await _collection.DeleteOneAsync(x => x.Id.Equals(entity.Id), new DeleteOptions { }, token);
            }

            /// <inheritdoc/>
            public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
            {
                var listIds = entities.Select(x => x.Id).ToList();
                await DeleteManyAsync(x => listIds.Contains(x.Id), token);
            }

            /// <inheritdoc/>
            public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
            {
                await _collection.DeleteManyAsync(criteria, token);
            }

            /// <inheritdoc/>
            public async Task<bool> SaveAsync(TEntity entity, bool isUpsert = false, CancellationToken token = default)
            {
                var res = await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new ReplaceOptions { IsUpsert = isUpsert }, token);
                return isUpsert || res.ModifiedCount == 1;
            }

            /// <inheritdoc/>
            public async Task<IEnumerable<TProperty>> DistinctAsync<TProperty>(Expression<Func<TEntity, TProperty>> keySelector, Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
            {
                var res = await _collection.DistinctAsync(keySelector, filter, null, token);
                return res.ToList();
            }

            /// <inheritdoc/>
            public async Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
            {
                var res = await _collection.DeleteOneAsync(criteria, token);
                return res.DeletedCount == 1;
            }

            /// <inheritdoc/>
            public async Task<bool> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> search, TEntity entity, CancellationToken token = default)
            {
                var res = await _collection.FindOneAndReplaceAsync(search, entity, null, token);
                return res != null;
            }

            //#region IQueryable<T>

            ///// <inheritdoc/>
            //public Type ElementType => _collection.AsQueryable().ElementType;

            ///// <inheritdoc/>
            //public Expression Expression => _collection.AsQueryable().Expression;

            ///// <inheritdoc/>
            //public IQueryProvider Provider => _collection.AsQueryable().Provider;

            ///// <inheritdoc/>
            //public IEnumerator<TEntity> GetEnumerator()
            //{
            //    return _collection.AsQueryable().GetEnumerator();
            //}

            ///// <inheritdoc/>
            //IEnumerator IEnumerable.GetEnumerator()
            //{
            //    return GetEnumerator();
            //}

            //#endregion
        }

        private class MongoRepositoryStrategyWithTransaction : IMongoRepositoryStrategy
        {
            private readonly IClientSessionHandle _session;
            private readonly IMongoCollection<TEntity> _collection;

            /// <inheritdoc/>
            public MongoRepositoryStrategyWithTransaction(IClientSessionHandle session, IMongoCollection<TEntity> collection)
            {
                _session = session ?? throw new ArgumentNullException(nameof(session));
                _collection = collection ?? throw new ArgumentNullException(nameof(collection));
            }

            /// <inheritdoc/>
            public IQueryable<TEntity> Entities => _collection.AsQueryable();

            /// <inheritdoc/>
            public async Task AddAsync(TEntity entity, CancellationToken token = default)
            {
                await _collection.InsertOneAsync(_session, entity, new InsertOneOptions { BypassDocumentValidation = false }, token);
            }

            /// <inheritdoc/>
            public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
            {
                if(entities.Any())
                    await _collection.InsertManyAsync(_session, entities, null, token);
            }

            /// <inheritdoc/>
            public async Task ClearAsync(CancellationToken token = default)
            {
                await DeleteManyAsync(x => true, token);
                //await _collection.Database.DropCollectionAsync(_session, _collection.CollectionNamespace.CollectionName, token);
            }

            /// <inheritdoc/>
            public async Task DeleteAsync(TEntity entity, CancellationToken token = default)
            {
                await _collection.DeleteOneAsync(_session, x => x.Id.Equals(entity.Id), new DeleteOptions { }, token);
            }

            /// <inheritdoc/>
            public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
            {
                var listIds = entities.Select(x => x.Id).ToList();
                await DeleteManyAsync(x => listIds.Contains(x.Id), token);
            }

            /// <inheritdoc/>
            public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
            {
                await _collection.DeleteManyAsync(_session, criteria, new DeleteOptions(), token);
            }

            /// <inheritdoc/>
            public async Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
            {
                var res = await _collection.DeleteOneAsync(_session, criteria, null, token);
                return res.DeletedCount == 1;
            }

            /// <inheritdoc/>
            public async Task<IEnumerable<TProperty>> DistinctAsync<TProperty>(Expression<Func<TEntity, TProperty>> keySelector, Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
            {
                var res = await _collection.DistinctAsync(keySelector, filter, null, token);
                return res.ToList();
            }

            public async Task<bool> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> search, TEntity entity, CancellationToken token = default)
            {
                var data = await _collection.FindOneAndReplaceAsync(search, entity, null, token);
                return data != null;
            }

            /// <inheritdoc/>
            public async Task<bool> SaveAsync(TEntity entity, bool isUpsert = false, CancellationToken token = default)
            {
                var res = await _collection.ReplaceOneAsync(_session, x => x.Id.Equals(entity.Id), entity, new ReplaceOptions { IsUpsert = isUpsert }, token);
                return isUpsert || res.ModifiedCount == 1;
            }
        }

        /// <summary>
        /// Сохранение сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="isUpsert">Если истина, то если сущность отсутствует, то она будет добавлена</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        public async Task<bool> SaveAsync(TEntity entity, bool isUpsert = false, CancellationToken token = default)
        {
            return await _repositoryStrategy.SaveAsync(entity, isUpsert, token);
        }

        ///// <summary>
        ///// Частичное обновление сущности
        ///// </summary>
        ///// <param name="entity">Сущность</param>
        ///// <param name="token">Токен отмены задачи</param>
        ///// <param name="updates">Частичные изменения</param>
        ///// <returns></returns>
        //public Task SavePartialAsync(T entity, CancellationToken token = default, params Expression<Func<T, object>>[] updates)
        //{
        //    var updateDefinition = Builders<BsonDocument>.Update.Combine(ConvertUpdateDefinition(entity, updates));
        //    return DataBase.GetCollection<BsonDocument>(_collectionName).UpdateOneAsync(x => x["_id"] == entity.Id, updateDefinition, null, token);
        //}

        //private static IEnumerable<UpdateDefinition<BsonDocument>> ConvertUpdateDefinition(T entity, params Expression<Func<T, object>>[] updates)
        //{
        //    var map = (BsonClassMap<T>)BsonClassMap.LookupClassMap(typeof(T));
        //    foreach (var update in updates)
        //    {
        //        var fieldMap = map.GetMemberMap(update);
        //        var exp = update.Compile();
        //        var value = exp(entity);
        //        var sz = Newtonsoft.Json.JsonConvert.SerializeObject(value, new JsonSerializerSettings
        //        {
        //            ContractResolver = new BsonDataContractResolver(),
        //            Converters = new List<JsonConverter> { new LongJsonConverter(), new DateTimeJsonConverter() }
        //        });

        //        var updateStr = string.Format("{{$set : {{{0} : {1}}}}}", fieldMap.ElementName, sz);
        //        yield return updateStr;
        //    }
        //}

        /// <summary>
        /// Добавление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity, CancellationToken token = default)
        {
            await _repositoryStrategy.AddAsync(entity, token);
        }

        /// <summary>
        /// Добавление нескольких сущностей
        /// </summary>
        /// <param name="entities">Сущности</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        public async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            await _repositoryStrategy.AddManyAsync(entities, token);
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity, CancellationToken token = default)
        {
            await _repositoryStrategy.DeleteAsync(entity, token);
        }

        /// <summary>
        /// Удаление нескольких сущностей
        /// </summary>
        /// <param name="entities">Сущности</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            await _repositoryStrategy.DeleteManyAsync(entities, token);
        }

        /// <summary>
        /// Удаление нескольких сущностей по условию
        /// </summary>
        /// <param name="criteri">Условие</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
        {
            await _repositoryStrategy.DeleteManyAsync(criteria, token);
        }

        /// <inheritdoc/>
        public async Task ClearAsync(CancellationToken token = default)
        {
            await _repositoryStrategy.ClearAsync(token);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TProperty>> DistinctAsync<TProperty>(Expression<Func<TEntity, TProperty>> keySelector, Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
        {
            return await _repositoryStrategy.DistinctAsync(keySelector, filter, token);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
        {
            return await _repositoryStrategy.DeleteOneAsync(criteria, token);
        }

        /// <inheritdoc/>
        public async Task<bool> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> search, TEntity entity, CancellationToken token = default)
        {
            return await _repositoryStrategy.FindOneAndReplaceAsync(search, entity, token);
        }

        #endregion

        //#region IQueryable<T>

        ///// <inheritdoc/>
        //public Type ElementType => Collection.AsQueryable().ElementType;

        ///// <inheritdoc/>
        //public Expression Expression => Collection.AsQueryable().Expression;

        ///// <inheritdoc/>
        //public IQueryProvider Provider => Collection.AsQueryable().Provider;

        ///// <inheritdoc/>
        //public IEnumerator<TEntity> GetEnumerator()
        //{
        //    return Collection.AsQueryable().GetEnumerator();
        //}

        ///// <inheritdoc/>
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        //#endregion
    }
}
