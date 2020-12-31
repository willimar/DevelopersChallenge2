using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProviderCore
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<IHandleMessage>> UpdateData(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<IHandleMessage>> AppenData(TEntity entity);
        Task<IEnumerable<IHandleMessage>> DeleteData(Expression<Func<TEntity, bool>> func);
        Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> func, List<Expression<Func<TEntity, object>>> sortFields, int top = 0, int page = 0);
        void Dispose();
    }
}
