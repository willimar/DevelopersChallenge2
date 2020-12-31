using FinancialCore.Entities;
using FinancialCore.Enums;
using FinancialService.Abstract;
using FinancialService.Concret;
using FinancialService.Exceptions;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FinancialStatementTest.ReadFileTest
{
    public class FinancialStatementServiceTest
    {
        readonly IFinancialStatementService _financialStatementService;

        public FinancialStatementServiceTest()
        {
            this._financialStatementService = new FinancialStatementService();
        }

        [Fact]
        public async Task FileToFinancialStatementTest_Extrato1()
        {
            var fileContent = Properties.Resources.extrato1;

            FinancialStatement financialStatement = await this._financialStatementService.FileToFinancialStatement(fileContent);

            financialStatement.Should().NotBeNull();
            financialStatement.BankId.Should().Be(341);
            financialStatement.Account.Should().Be(7037300576);
            financialStatement.CurrencyType.Should().Be(CurrencyType.BRL);
            financialStatement.Transactions.Should().HaveCount(31);
        }
        [Fact]
        public async Task FileToFinancialStatementTest_Extrato2()
        {
            var fileContent = Properties.Resources.extrato7;

            FinancialStatement financialStatement = await this._financialStatementService.FileToFinancialStatement(fileContent);

            financialStatement.Should().NotBeNull();
            financialStatement.BankId.Should().Be(341);
            financialStatement.Account.Should().Be(7037300576);
            financialStatement.CurrencyType.Should().Be(CurrencyType.BRL);
            financialStatement.Transactions.Should().HaveCount(22);
        }

        [Fact]
        public async Task FileToFinancialStatementTest_OrderChanged()
        {
            var fileContent = Properties.Resources.extrato2;

            FinancialStatement financialStatement = await this._financialStatementService.FileToFinancialStatement(fileContent);

            financialStatement.Should().NotBeNull();
            financialStatement.BankId.Should().Be(341);
            financialStatement.Account.Should().Be(7037300576);
            financialStatement.CurrencyType.Should().Be(CurrencyType.BRL);
            financialStatement.Transactions.Should().HaveCount(4);
        }

        [Fact]
        public void FileToFinancialStatementTest_EmpytValues_TransactionType()
        {
            var fileContent = Properties.Resources.extrato3;

            Action action = () => { FinancialStatement financialStatement = this._financialStatementService.FileToFinancialStatement(fileContent).Result; };

            action.Should().Throw<ConvertValueException>().WithMessage("Impossible convert value '' to the type TransactionType.");
        }

        [Fact]
        public void FileToFinancialStatementTest_EmpytValues_TransactionDate()
        {
            var fileContent = Properties.Resources.extrato4;

            Action action = () => { FinancialStatement financialStatement = this._financialStatementService.FileToFinancialStatement(fileContent).Result; };

            action.Should().Throw<ConvertValueException>().WithMessage("Impossible convert value '' to the type DateTime.");
        }

        [Fact]
        public void FileToFinancialStatementTest_EmpytValues_TransactionValue()
        {
            var fileContent = Properties.Resources.extrato5;

            Action action = () => { FinancialStatement financialStatement = this._financialStatementService.FileToFinancialStatement(fileContent).Result; };

            action.Should().Throw<ConvertValueException>().WithMessage("Impossible convert value '' to the type Decimal.");
        }

        [Fact]
        public void FileToFinancialStatementTest_EmpytValues()
        {
            var fileContent = Properties.Resources.extrato6;

            FinancialStatement financialStatement = this._financialStatementService.FileToFinancialStatement(fileContent).Result;

            financialStatement.Should().NotBeNull();
            financialStatement.BankId.Should().Be(341);
            financialStatement.Account.Should().Be(7037300576);
            financialStatement.CurrencyType.Should().Be(CurrencyType.BRL);
            financialStatement.Transactions.Should().HaveCount(1);
        }
    }
}
