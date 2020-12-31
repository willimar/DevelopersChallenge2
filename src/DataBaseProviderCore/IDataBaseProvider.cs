using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseProviderCore
{
    public interface IDataBaseProvider : IDisposable
    {
        IDataSet<TEntity> GetDataSet<TEntity>() where TEntity : class;
    }
}
