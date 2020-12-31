using FinancialCore.Enums;
using System;
using System.Collections.Generic;

namespace FinancialCore.Entities
{
    public class FinancialStatement
    {
        public FinancialStatement()
        {
            this.Transactions = new List<Transaction>();
            this.Id = Guid.NewGuid();
        }

        public FinancialStatement(int bankId, Int64 account, CurrencyType currencyType)
        {
            this.BankId = bankId;
            this.Account = account;
            this.CurrencyType = currencyType;
            this.Id = Guid.NewGuid();

            this.Transactions = new List<Transaction>();
        }

        public Guid Id { get; set; }
        public int BankId { get; set; }
        public Int64 Account { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public List<Transaction> Transactions { get; set; }

        public override bool Equals(object obj)
        {
            var financialStatement = obj as FinancialStatement ?? throw new ArgumentException(nameof(obj));
            return financialStatement.BankId == this.BankId && financialStatement.Account == this.Account && financialStatement.CurrencyType == this.CurrencyType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BankId, Account, CurrencyType, Transactions);
        }
    }
}
