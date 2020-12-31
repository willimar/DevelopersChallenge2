using DataBaseProviderCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseProvider
{
    public class DataBaseProvider : IDataBaseProvider
    {
        private IMongoDatabase _dbConnection;

        public DataBaseProvider(IMongoClient context, string dataBase)
        {
            this._dbConnection = context.GetDatabase(dataBase);
        }

        public void Dispose()
        {
            
        }

        public IDataSet<TEntity> GetDataSet<TEntity>() where TEntity : class
        {
            return new DataSet<TEntity>(this._dbConnection);
        }
    }
}
