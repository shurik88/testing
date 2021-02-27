using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApp2.DataAccess.Mongo.Extensions
{
    public static class MongoQuerableExtensions
    {
        public static async Task<List<TDocument>> ToListAsync<TDocument>(this IQueryable<TDocument> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.ToList();

            var asyncSource = CastAndGet(source);
            return await IAsyncCursorSourceExtensions.ToListAsync(asyncSource, cancellationToken);
        }

        private static IMongoQueryable<TDocument> CastAndGet<TDocument>(this IQueryable<TDocument> source)
        {
            var asyncSource = source as IMongoQueryable<TDocument>;
            if (asyncSource == null)
                throw new InvalidOperationException("IQueryableProviderNotForMongo");

            return asyncSource;
        }

        public static async Task<bool> AnyAsync<TDocument>(this IQueryable<TDocument> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.Any();

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.AnyAsync(asyncSource, cancellationToken);
        }

        public static async Task<bool> AnyAsync<TDocument>(this IQueryable<TDocument> source, Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.Any(filter);

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.AnyAsync(asyncSource.Where(filter), cancellationToken);
        }

        public static async Task<TDocument> FirstAsync<TDocument>(this IQueryable<TDocument> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.First();

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.FirstAsync(asyncSource, cancellationToken);
        }

        public static async Task<TDocument> FirstAsync<TDocument>(this IQueryable<TDocument> source, Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.First(filter);

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.FirstAsync(asyncSource.Where(filter), cancellationToken);
        }

        public static async Task<TDocument> FirstOrDefaultAsync<TDocument>(this IQueryable<TDocument> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.FirstOrDefault();

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.FirstOrDefaultAsync(asyncSource, cancellationToken);
        }

        public static async Task<TDocument> FirstOrDefaultAsync<TDocument>(this IQueryable<TDocument> source, Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.FirstOrDefault(filter);

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.FirstOrDefaultAsync(asyncSource.Where(filter), cancellationToken);
        }

        public static async Task<TDocument> SingleAsync<TDocument>(this IQueryable<TDocument> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.Single();

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.SingleAsync(asyncSource, cancellationToken);
        }

        public static async Task<TDocument> SingleAsync<TDocument>(this IQueryable<TDocument> source, Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.Single(filter);

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.SingleAsync(asyncSource.Where(filter), cancellationToken);
        }

        public static async Task<TDocument> SingleOrDefaultAsync<TDocument>(this IQueryable<TDocument> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.SingleOrDefault();

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.SingleOrDefaultAsync(asyncSource, cancellationToken);
        }

        public static async Task<TDocument> SingleOrDefaultAsync<TDocument>(this IQueryable<TDocument> source, Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.SingleOrDefault(filter);

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.SingleOrDefaultAsync(asyncSource.Where(filter), cancellationToken);
        }

        public static async Task<int> CountAsync<TDocument>(this IQueryable<TDocument> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.Count();

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.CountAsync(asyncSource, cancellationToken);
        }

        public static async Task<int> CountAsync<TDocument>(this IQueryable<TDocument> source, Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(EnumerableQuery<>))
                return source.Count(filter);

            var asyncSource = CastAndGet(source);
            return await MongoQueryable.CountAsync(asyncSource.Where(filter), cancellationToken);
        }
    }
}
