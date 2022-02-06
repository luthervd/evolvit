using System.Collections.Generic;

namespace Cms.Shared
{
    public interface IUnitOfWork<T,TId> where T : IEntity<TId>
    {
        Task<T> DoWork(T entity);
    }
}
