using DemoDay1.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoDay1.Infra
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        DbContext Context { get; }

        Task<int> Commit(CancellationToken cancellationToken = default(CancellationToken));
    }
}
