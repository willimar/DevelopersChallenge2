using FinancialCore.Entities;
using FinancialCore.Enums;
using FinancialService.Abstract;
using FinancialService.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace FinancialService.Concret
{
    public class FinancialStatementService : IFinancialStatementService
    {
        #region Publics
        public async Task<FinancialStatement> FileToFinancialStatement(string fileContent)
        {
            if (string.IsNullOrWhiteSpace(fileContent))
            {
                throw new ArgumentException(nameof(fileContent));
            }

            FinancialStatement result = new FinancialStatement();

            await Task.Run(() => {
                var currencyType = this.GetCurrency(fileContent);
                var account = this.GetAccount(fileContent);
                var bankId = this.GetBank(fileContent);

                result.BankId = bankId;
                result.Account = account; 
                result.CurrencyType = currencyType;
            });

            await Task.Run(() => {
                var transactionContent = fileContent.Parse("<BANKTRANLIST>", "</BANKTRANLIST>");

                result.Transactions = this.GetTransactions(transactionContent.RemoveSpecialChars());
            });

            return result;
        }
        #endregion

        #region Field Parser 
        private List<Transaction> GetTransactions(string transactionContent)
        {
            var content = transactionContent;
            var result = new List<Transaction>();

            while (content.ParseCheck("<STMTTRN>", "</STMTTRN>"))
            {
                var (ParsedValue, NewString) = content.ParseAndRemove("<STMTTRN>", "</STMTTRN>");
                content = NewString;

                var transType = this.GetTansactionType(ParsedValue);
                var dtaOper = this.GetDateOper(ParsedValue);
                var value = this.GetValue(ParsedValue);
                var info = this.GetInfo(ParsedValue);

                result.Add(new Transaction(transType, dtaOper, value, info));
            }

            return result;
        }

        private string GetInfo(string content)
        {
            var result = content.Parse("<MEMO>", "<") ?? throw new ParserValueException("Transaction Info");
            result = result.RemoveSpecialChars();

            while (result.IndexOf("  ") >= 0)
            {
                result = result.Replace("  ", string.Empty);
            }

            return result;
        }

        private decimal GetValue(string content)
        {
            var result = content.Parse("<TRNAMT>", "<") ?? throw new ParserValueException("Tansaction Value");
            result = result.RemoveSpecialChars();

            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.Number;
            var provider = new CultureInfo("en-US");
            if (decimal.TryParse(result, style, provider, out decimal converted))
            {
                return converted;
            }

            throw new ConvertValueException(result, converted.GetType());
        }

        private DateTime ConvertToDateTime(string value)
        {
            var year = Convert.ToInt32(value.Substring(0, 4));
            var month = Convert.ToInt32(value.Substring(4, 2));
            var day = Convert.ToInt32(value.Substring(6, 2));
            var hour = Convert.ToInt32(value.Substring(8, 2));
            var minute = Convert.ToInt32(value.Substring(10, 2));
            var second = 0;

            return new DateTime(year, month, day, hour, minute, second);
        }

        private DateTime GetDateOper(string content)
        {
            var result = content.Parse("<DTPOSTED>", "<") ?? throw new ParserValueException("Operation Date");
            result = result.RemoveSpecialChars();

            try
            {
                return this.ConvertToDateTime(result);
            }
            catch
            {
                throw new ConvertValueException(result, typeof(DateTime));
            }
        }

        private TransactionType GetTansactionType(string content)
        {
            var result = content.Parse("<TRNTYPE>", "<") ?? throw new ParserValueException("Transaction Type");
            result = result.RemoveSpecialChars();

            if (Enum.TryParse(result, true, out TransactionType converted))
            {
                return converted;
            }

            throw new ConvertValueException(result, converted.GetType());
        }

        private int GetBank(string fileContent)
        {
            var result = fileContent.Parse("<BANKID>", "<") ?? throw new ParserValueException("BankId");
            result = result.RemoveSpecialChars();

            if (int.TryParse(result, out int converted))
            {
                return converted;
            }

            throw new ConvertValueException(result, converted.GetType());
        }

        private Int64 GetAccount(string fileContent)
        {
            var result = fileContent.Parse("<ACCTID>", "<") ?? throw new ParserValueException("Account");
            result = result.RemoveSpecialChars();

            if (Int64.TryParse(result, out Int64 converted))
            {
                return converted;
            }

            throw new ConvertValueException(result, converted.GetType());
        }

        private CurrencyType GetCurrency(string fileContent)
        {
            var result = fileContent.Parse("<CURDEF>", "<") ?? throw new ParserValueException("CurrencyType");
            result = result.RemoveSpecialChars();

            if (Enum.TryParse(result, true, out CurrencyType converted))
            {
                return converted;
            }

            throw new ConvertValueException(result, converted.GetType());
        }
        #endregion
    }
}
