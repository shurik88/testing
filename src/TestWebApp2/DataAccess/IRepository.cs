using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApp2.DataAccess
{
    /// <summary>
    ///     Интерфейс выполнения модификации сущности
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    public interface IRepository<TEntity, TKey>
        where TEntity: class, IEntity<TKey>
    {
        /// <summary>
        ///     Сущности
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        /// <summary>
        ///     Добавление сущности.
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task AddAsync(TEntity entity, CancellationToken token = default);

        /// <summary>
        ///     Добавление нескольких сущностей.
        /// </summary>
        /// <param name="entities">Сущности</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default);

        /// <summary>
        ///     Удаление сущности.
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity, CancellationToken token = default);

        /// <summary>
        ///     Удаление нескольких сущностей.
        /// </summary>
        /// <param name="entities">Сущности</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default);

        /// <summary>
        ///     Удаление нескольких сущностей по условию.
        /// </summary>
        /// <param name="criteria">Условие</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task DeleteManyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default);

        /// <summary>
        ///     Удаление одной сущности по условию.
        /// </summary>
        /// <param name="criteria">Условие</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns>Найден ли элемент</returns>
        Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default);

        /// <summary>
        ///     Получение уникальных значений
        /// </summary>
        /// <typeparam name="TProperty">Свойство, по которому проверяется уникальность</typeparam>
        /// <param name="keySelector">Выражение уникальности</param>
        /// <param name="filter">Фильтр данных</param>
        /// /// <param name="token">Токен отмены задачи</param>
        /// <returns>Список уникальных значений свойства</returns>
        Task<IEnumerable<TProperty>> DistinctAsync<TProperty>(Expression<Func<TEntity, TProperty>> keySelector, Expression<Func<TEntity, bool>> filter, CancellationToken token = default);

        /// <summary>
        ///     Сохранение сущности.
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="isUpsert">Если истина, то если сущность отсутствует, то она будет добавлена</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task<bool> SaveAsync(TEntity entity, bool isUpsert = false, CancellationToken token = default);

        /// <summary>
        ///     Поиск из замена сущности по критерию.
        /// </summary>
        /// <remarks>
        ///     Метод рекомендуется использовать для оптимистических блокировок.
        /// </remarks>
        /// <param name="search">Критерий поиска сущности</param>
        /// <param name="entity">Сущность</param>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns>Была ли найдена сущность и изменена</returns>
        Task<bool> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> search, TEntity entity, CancellationToken token = default);

        /// <summary>
        ///     Очистка коллекции.
        /// </summary>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task ClearAsync(CancellationToken token = default);
    }
}
