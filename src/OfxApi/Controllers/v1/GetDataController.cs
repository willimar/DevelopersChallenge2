using FinancialCore.Entities;
using FinancialCore.Enums;
using FinancialMediator;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfxApi.Controllers.v1
{
    [EnableCors(Program.AllowSpecificOrigins)]
    [ApiController]
    [ApiVersion(Program.API_V_1_0, Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Produces("application/json")]
    public class GetDataController: Controller
    {
        private readonly SaveOfxData _saveOfxData;

        public GetDataController(SaveOfxData saveOfxData)
        {
            this._saveOfxData = saveOfxData;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<List<int>> Banks()
        {
            return await this._saveOfxData.GetBankList();
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<List<long>> Accounts(int bank)
        {
            return await this._saveOfxData.GetAccountList(bank);
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<FinancialStatement> Transactions(int bank, long account, CurrencyType currencyType)
        {
            var data = await this._saveOfxData.GetTransactions(bank, account, currencyType);

            return data.FirstOrDefault();
        }
    }
}
