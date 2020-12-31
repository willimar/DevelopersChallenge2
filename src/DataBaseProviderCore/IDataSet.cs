using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProviderCore
{
    public interface IDataSet<TEntity> : IDisposable where TEntity : class
    {
        Task Append(IEnumerable<TEntity> entity);
        Task<long> DeleteRecords(Expression<Func<TEntity, bool>> predicate);
        Task<long> UpdateRecords(Expression<Func<TEntity, bool>> predicate, TEntity entity);
        Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> sortFields, int limit = 0, int page = 0);
    }
}
