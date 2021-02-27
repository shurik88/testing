using MongoDB.Bson.Serialization.Attributes;
 
namespace TestWebApp2.DataAccess.Mongo
{
    /// <summary>
    ///     Базовый тип сущности, с которой может использоваться <seealso cref="MongoRepository{TEntity, TKey}"/>
    /// </summary>
    [BsonIgnoreExtraElements(Inherited = true)]
    public class MongoEntityKey<T> : IEntity<T>
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        [BsonId]
        public T Id { get; set; }
    }
}
