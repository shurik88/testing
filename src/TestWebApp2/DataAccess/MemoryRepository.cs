using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApp2.DataAccess
{
    /// <summary>
    ///     Реализация <seealso cref="IRepository{TEntity, TKey}"/> в памяти для тетсирования.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    public class MemoryRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity: class, IEntity<TKey>
    {
        /// <inheritdoc/>
        public IQueryable<TEntity> Entities => _data.AsQueryable();
        
        // /// <inheritdoc/>
        //public Type ElementType => _data.AsQueryable().ElementType;

        ///// <inheritdoc/>
        //public Expression Expression => _data.AsQueryable().Expression;

        ///// <inheritdoc/>
        //public IQueryProvider Provider => _data.AsQueryable().Provider;

        private readonly List<TEntity> _data;

        /// <summary>
        ///     Создание экземпляра класса <see cref="MemoryRepository{TEntity, TKey}"/>
        /// </summary>
        /// <param name="initData">Начальные данные</param>
        public MemoryRepository(List<TEntity> initData = null)
        {
            _data = initData ?? new List<TEntity>();
        }

        /// <inheritdoc/>
        public Task AddAsync(TEntity entity, CancellationToken token = default)
        {
            _data.Add(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            _data.AddRange(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task ClearAsync(CancellationToken token = default)
        {
            _data.Clear();
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(TEntity entity, CancellationToken token = default)
        {
            _data.Remove(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            foreach (var entity in entities)
                _data.Remove(entity);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
        {
            var func = criteria.Compile();
            _data.RemoveAll(t => func(t));
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        //public IEnumerator<TEntity> GetEnumerator() => _data.GetEnumerator();

        /// <inheritdoc/>
        public Task<bool> SaveAsync(TEntity entity, bool isUpsert = false, CancellationToken token = default)
        {
            if (!_data.Contains(entity) && isUpsert)
                _data.Add(entity);
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TProperty>> DistinctAsync<TProperty>(Expression<Func<TEntity, TProperty>> keySelector, Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
        {
            var seenKeys = new HashSet<TProperty>();
            var compiledFilter = filter.Compile();
            var compiledSelector = keySelector.Compile();
            foreach (var element in _data.Where(compiledFilter))
                seenKeys.Add(compiledSelector(element));

            return Task.FromResult(seenKeys.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken token = default)
        {
            var compiledFilter = criteria.Compile();
            var firstItem = _data.FirstOrDefault(compiledFilter);
            if (firstItem == null)
                return Task.FromResult(false);

            _data.Remove(firstItem);
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> search, TEntity entity, CancellationToken token = default)
        {
            var compiledFilter = search.Compile();
            var index = _data.FindIndex(x => compiledFilter(x));
            if(index == -1)
                return Task.FromResult(false);

            _data[index] = entity;
            return Task.FromResult(true);
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}
