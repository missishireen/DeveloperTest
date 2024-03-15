using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class AmountPerUomService : IRebateCalculationService
    {
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;

        }

        public bool IsValidRequest(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.IsSupportsIncentiveType(SupportedIncentiveType.AmountPerUom) && rebate.Amount != 0 && request.Volume != 0;

        }
    }
}
