using DataBaseProviderCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class DataSet<TEntity> : IDataSet<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _mongoCollection;

        public DataSet(IMongoDatabase database)
        {
            this._mongoCollection = database.GetCollection<TEntity>(typeof(TEntity).Name.Replace("`1", string.Empty)); ;
        }

        public async Task Append(IEnumerable<TEntity> entity)
        {
            await this._mongoCollection.InsertManyAsync(entity);
        }

        public async Task<long> DeleteRecords(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await this._mongoCollection.DeleteManyAsync(predicate);

            return result.DeletedCount;
        }

        public void Dispose()
        {
            
        }

        public async Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> sortFields, int limit = 0, int page = 0)
        {
            var find = this._mongoCollection.Find(predicate);

            await Task.Run(() => {
                foreach (var item in sortFields)
                {
                    find = find.SortBy(item);
                }
            });

            await Task.Run(() => {
                if (page > 0 && limit > 0)
                {
                    find = find.Skip((page - 1) * limit).Limit(limit);
                }
            });

            await Task.Run(() => {
                if (limit > 0)
                {
                    find = find.Limit(limit);
                }
            });

            return await find.ToListAsync();
        }

        public async Task<long> UpdateRecords(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            var result = await this._mongoCollection.ReplaceOneAsync(predicate, entity);

            return result.MatchedCount;
        }
    }
}
