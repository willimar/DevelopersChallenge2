using FinancialCore.Enums;
using System;

namespace FinancialCore.Entities
{
    public class Transaction
    {
        public Transaction()
        {

        }

        public Transaction(TransactionType type, DateTime dateTime, decimal value, string info)
        {
            if (dateTime > DateTime.UtcNow)
            {
                throw new ArgumentException(nameof(dateTime));
            }

            this.Type = type;
            this.OperationDate = dateTime;
            this.Value = value;
            this.Info = info ?? throw new ArgumentNullException(nameof(info));

            var hashValue = $"{this.Type}{this.OperationDate}{this.Value}{this.Info}";
            this.HashId = BitConverter.ToString(new System.Security.Cryptography.SHA256Managed().ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashValue))).Replace("-", string.Empty);
        }

        public TransactionType Type { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Value { get; set; }
        public string Info { get; set; }
        public string HashId { get; set; }

        /// <summary>
        /// Usually we have an ID to each transaction like can observable in documentation linked https://dev.gerencianet.com.br/docs/relatorio-ofx.
        /// How we haven't this ID I believe that the object is equal when all property be equals. 
        /// </summary>
        public override bool Equals(object obj)
        {
            var transaction = obj as Transaction ?? throw new ArgumentException(nameof(obj));

            return this.OperationDate == transaction.OperationDate && 
                this.Info.ToLower() == transaction.Info.ToLower() &&
                this.Type == transaction.Type &&
                this.Value == transaction.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperationDate, Value, Info);
        }
    }
}