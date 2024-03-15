using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedCashAmountService : IRebateCalculationService
    {
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }

        public bool IsValidRequest(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.IsSupportsIncentiveType(SupportedIncentiveType.FixedCashAmount) && rebate.Amount != 0;
        }
    }
}
