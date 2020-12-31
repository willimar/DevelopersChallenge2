using DataBaseProviderCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class BaseRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        protected readonly IDataSet<TEntity> _dataset = null;

        public BaseRepository(IDataBaseProvider provider)
        {
            this._dataset = provider.GetDataSet<TEntity>();
        }

        public async Task<IEnumerable<IHandleMessage>> AppenData(TEntity entity)
        {
            await this._dataset.Append(new List<TEntity>() { entity });

            return new List<IHandleMessage>() { new HandleMessage("AppendData", "Inserted record in provider.", HandlesCode.Accepted) };
        }

        public async Task<IEnumerable<IHandleMessage>> DeleteData(Expression<Func<TEntity, bool>> func)
        {
            var check = await this._dataset.DeleteRecords(func);

            if (check <= 0)
            {
                return new List<IHandleMessage>() { new HandleMessage("RecordNotFoundException", "Provider not found record.", HandlesCode.ValueNotFound) };
            }

            return new List<IHandleMessage>() { new HandleMessage("DeltedRecord", "Record was removed from data provider.", HandlesCode.Accepted) }; ;
        }

        public void Dispose()
        {
            this._dataset.Dispose();
        }

        public async Task<IEnumerable<IHandleMessage>> UpdateData(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var count = await this._dataset.UpdateRecords(predicate, entity);

            if (count <= 0)
            {
                return new List<IHandleMessage>() { new HandleMessage("RecordNotFoundException", "Record not found in data provider to change.", HandlesCode.ValueNotFound) };
            }

            return new List<IHandleMessage>() { new HandleMessage("RecordChanged", "The record was changed.", HandlesCode.Accepted) };
        }

        public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> func, List<Expression<Func<TEntity, object>>> sortFields, int top = 0, int page = 0)
        {
            var result = await this._dataset.GetEntities(func, sortFields, top, page);

            if (result is null)
            {
                return new List<TEntity>();
            }
            else
            {
                return result.ToList();
            }
        }
    }
}
