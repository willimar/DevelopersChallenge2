using DataBaseProvider;
using DataBaseProviderCore;
using FinancialCore.Entities;
using FinancialMediator;
using FinancialService.Abstract;
using FinancialService.Concret;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfxApi.Setups
{
    public static class ControllersSetup
    {
        private static ConnectionSetup GetConnection()
        {
            ConnectionSetup connection = new ConnectionSetup() {
                DataBaseAuth = Environment.GetEnvironmentVariable("API_DATABASEAUTH", EnvironmentVariableTarget.Machine).Trim(),
                HostInfo = Environment.GetEnvironmentVariable("API_HOSTINFO", EnvironmentVariableTarget.Machine).Trim(),
                Port = Convert.ToInt32(Environment.GetEnvironmentVariable("API_PORT", EnvironmentVariableTarget.Machine).Trim()),
                UserName = Environment.GetEnvironmentVariable("API_USERNAME", EnvironmentVariableTarget.Machine).Trim(),
                UserPws = Environment.GetEnvironmentVariable("API_USERPWS", EnvironmentVariableTarget.Machine).Trim(),
                DataBaseName = Environment.GetEnvironmentVariable("API_DATABASENAME", EnvironmentVariableTarget.Machine).Trim()
            };

            return connection;
        }

        public static void ControllersDependences(this IServiceCollection services)
        {
            services.AddSingleton<ConnectionSetup>(r => ControllersSetup.GetConnection());
            services.AddScoped<IMongoClient, Connection>(r => new Connection(r.GetService<ConnectionSetup>()));
            services.AddScoped<IDataBaseProvider, DataBaseProvider.DataBaseProvider>(r => new DataBaseProvider.DataBaseProvider(r.GetService<IMongoClient>(), r.GetService<ConnectionSetup>().DataBaseName));
            services.AddScoped<IRepository<FinancialStatement>, BaseRepository<FinancialStatement>>();
            services.AddScoped<IFinancialStatementService, FinancialStatementService>();
            services.AddScoped<SaveOfxData>();
        }
    }
}
