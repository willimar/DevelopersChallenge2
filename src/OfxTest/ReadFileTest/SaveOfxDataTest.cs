using DataBaseProviderCore;
using FinancialCore.Entities;
using FinancialCore.Enums;
using FinancialMediator;
using FinancialService.Abstract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinancialStatementTest.ReadFileTest
{
    public class SaveOfxDataTest
    {
        private readonly Mock<IRepository<FinancialStatement>> _repositoryMock;
        private readonly Mock<IFinancialStatementService> _financialStatementServiceMock;
        private readonly SaveOfxData _saveOfxData;

        public SaveOfxDataTest()
        {
            this._repositoryMock = new Mock<IRepository<FinancialStatement>>();
            this._financialStatementServiceMock = new Mock<IFinancialStatementService>();
            this._saveOfxData = new SaveOfxData(this._financialStatementServiceMock.Object, this._repositoryMock.Object);
        }

        private Task<FinancialStatement> GetFinancialStatement()
        {
            return Task<FinancialStatement>.Run(() => {
                return new FinancialStatement(123, 123456789, CurrencyType.BRL)
                {
                    Transactions = new List<Transaction>() { new Transaction(TransactionType.Credit, DateTime.UtcNow, 1200, "Unity Test Value") }
                };
            });
        }

        [Fact]
        public async Task SaveOfxDataTest_AppendExecute()
        {
            var file = Properties.Resources.extrato1;
            var entity = this.GetFinancialStatement();
            this._financialStatementServiceMock.Setup(x => x.FileToFinancialStatement(file)).Returns(entity);
            var result = await this._saveOfxData.Save(new string[] { file });

            this._financialStatementServiceMock.Verify(x => x.FileToFinancialStatement(file), Times.Once);
            this._repositoryMock.Verify(x => x.AppenData(entity.Result), Times.Once);
        }

        [Fact]
        public async Task SaveOfxDataTest_UpdateExecute()
        {
            var file = Properties.Resources.extrato1;
            var entityTask = this.GetFinancialStatement();
            var financialStatement = await this.GetFinancialStatement();

            financialStatement.Transactions.Add(new Transaction(TransactionType.Debit, DateTime.UtcNow, 12.5M, "Credit Valut Test"));

            var order = new List<Expression<Func<FinancialStatement, object>>>();
            Task<IEnumerable<FinancialStatement>> getResult = Task.Run(() => { return new FinancialStatement[] { financialStatement }.AsEnumerable(); });
            this._financialStatementServiceMock.Setup(x => x.FileToFinancialStatement(file)).Returns(entityTask);
            this._repositoryMock.Setup(repository => repository.GetData(entity => 
                entity.BankId == financialStatement.BankId && entity.Account == financialStatement.Account && entity.CurrencyType == financialStatement.CurrencyType,
                order, 0, 0)).Returns(getResult);

            var result = await this._saveOfxData.Save(new string[] { file });

            this._financialStatementServiceMock.Verify(x => x.FileToFinancialStatement(file), Times.Once);
            this._repositoryMock.Verify(x => x.UpdateData(financialStatement, entity => entity.Equals(financialStatement)), Times.Once);
        }
    }
}
