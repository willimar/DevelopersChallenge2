using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinancialCore.Entities;

namespace FinancialService.Abstract
{
    public interface IFinancialStatementService
    {
        Task<FinancialStatement> FileToFinancialStatement(string fileContent);
    }
}
