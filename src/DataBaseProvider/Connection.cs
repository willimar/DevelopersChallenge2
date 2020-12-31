using DataBaseProviderCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseProvider
{
    public class Connection : MongoClient
    {
        public Connection(ConnectionSetup connectionSetup) : base(connectionString: ConnectionFactory(connectionSetup))
        {

        }

        private static string ConnectionFactory(ConnectionSetup connectionSetup)
        {
            string connectionString;

            if (string.IsNullOrEmpty(connectionSetup.UserName))
            {
                connectionString = $"mongodb://{connectionSetup.HostInfo}:{connectionSetup.Port}/?readPreference=primary&appname=postal.code.api&ssl=false";
            }
            else
            {
                connectionString = $"mongodb+srv://{connectionSetup.UserName}:{connectionSetup.UserPws}@{connectionSetup.HostInfo}/{connectionSetup.DataBaseAuth}?retryWrites=true&w=majority";
            }

            return connectionString;
        }
    }
}
