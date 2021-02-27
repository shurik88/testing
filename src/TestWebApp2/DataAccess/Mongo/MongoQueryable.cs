using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestWebApp2.DataAccess.Mongo
{
    /// <summary>
    ///     Реализация <see cref="IQueryable{T}"/> для mongo.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    internal class MongoQueryable<TEntity> : IQueryable<TEntity>
        where TEntity: class
    {
        ///// <summary>
        /////     Создание экземпляра класса <see cref="MongoQueryabley{TEntity}"/>.
        ///// </summary>
        ///// <param name="connectionString">Строка подключения к бд</param>
        ///// <param name="collectionName">Имя коллекции</param>
        //public MongoQueryable(string connectionString, string collectionName)      
        //{
        //    var mongoUrl = new MongoUrl(connectionString);
        //    var mongoClient = new MongoClient(new MongoUrl(connectionString));
        //    var dataBase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        //    Collection = dataBase.GetCollection<TEntity>(collectionName);
        //}

        /// <summary>
        ///     Создание экземпляра класса <see cref="MongoQueryabley{TEntity}"/>.
        /// </summary>
        /// <param name="dataBase">БД mongo</param>
        /// <param name="collectionName">Имя коллекции</param>
        public MongoQueryable(IMongoDatabase dataBase, string collectionName)
        {
            Collection = dataBase.GetCollection<TEntity>(collectionName);
        }

        protected IMongoCollection<TEntity> Collection { get; }


        #region IQueryable<T>

        /// <inheritdoc/>
        public Type ElementType => Collection.AsQueryable().ElementType;

        /// <inheritdoc/>
        public Expression Expression => Collection.AsQueryable().Expression;

        /// <inheritdoc/>
        public IQueryProvider Provider => Collection.AsQueryable().Provider;

        /// <inheritdoc/>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
