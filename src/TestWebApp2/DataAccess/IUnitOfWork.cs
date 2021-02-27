using System.Threading;
using System.Threading.Tasks;

namespace TestWebApp2.DataAccess
{
    /// <summary>
    ///     Единица работы.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Начать транзакцию.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        ///     Зафиксировать транзакцию.
        /// </summary>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task CommitAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        ///     Откатить транзакцию.
        /// </summary>
        /// <param name="token">Токен отмены задачи</param>
        /// <returns></returns>
        Task RollbackAsync(CancellationToken token = default(CancellationToken));
    }
}
