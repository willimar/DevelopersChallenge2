using DataBaseProviderCore;
using FinancialCore.Entities;
using FinancialCore.Enums;
using FinancialService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialMediator
{
    public class SaveOfxData
    {
        private readonly IFinancialStatementService _financialStatementService;
        private readonly IRepository<FinancialStatement> _repository;

        public SaveOfxData(IFinancialStatementService financialStatementService, IRepository<FinancialStatement> repository)
        {
            this._financialStatementService = financialStatementService;
            this._repository = repository;
        }

        public async Task<IEnumerable<IHandleMessage>> Save(string[] filesContent)
        {
            var result = new List<IHandleMessage>();
            foreach (var file in filesContent)
            {
                var financialStatement = await this._financialStatementService.FileToFinancialStatement(file);
                var order = new List<Expression<Func<FinancialStatement, object>>>();
                var data = await this._repository.GetData(entity => 
                    entity.BankId == financialStatement.BankId && entity.Account == financialStatement.Account && entity.CurrencyType == financialStatement.CurrencyType, 
                    order, 0, 0);

                if (!data.Any())
                {
                    result.AddRange(await this._repository.AppenData(financialStatement));
                }
                else
                {
                    var saveData = data.FirstOrDefault();

                    foreach (var transaction in financialStatement.Transactions)
                    {
                        if (!saveData.Transactions.Any(entity => entity.HashId == transaction.HashId))
                        {
                            saveData.Transactions.Add(transaction);
                        }
                    }

                    result.AddRange(await this._repository.UpdateData(saveData, entity => entity.BankId == financialStatement.BankId && entity.Account == financialStatement.Account && entity.CurrencyType == financialStatement.CurrencyType));
                }
            }

            if (result.Any())
            {
                return result;
            }

            return new List<IHandleMessage>() { new HandleMessage("FileContentNotFound.", "No data saved.", HandlesCode.ValueNotFound) };
        }

        public async Task<List<int>> GetBankList()
        {
            var data = await this._repository.GetData(x => true, new List<Expression<Func<FinancialStatement, object>>>());

            return data.Select(x => x.BankId).Distinct().ToList();
        }

        public async Task<List<long>> GetAccountList(int bankId)
        {
            var data = await this._repository.GetData(x => x.BankId == bankId, new List<Expression<Func<FinancialStatement, object>>>());

            return data.Select(x => x.Account).Distinct().ToList();
        }

        public async Task<List<FinancialStatement>> GetTransactions(int bankId, long accountId, CurrencyType currencyType)
        {
            var data = await this._repository.GetData(x => x.BankId == bankId && x.Account == accountId && x.CurrencyType == currencyType, new List<Expression<Func<FinancialStatement, object>>>());

            return data.ToList();
        }
    }
}
