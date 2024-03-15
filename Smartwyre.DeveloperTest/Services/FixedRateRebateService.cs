using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedRateRebateService : IRebateCalculationService
    {
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }

        public bool IsValidRequest(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.IsSupportsIncentiveType(SupportedIncentiveType.FixedRateRebate) && rebate.Percentage != 0 && product.Price != 0 && request.Volume != 0;

        }
    }
}
