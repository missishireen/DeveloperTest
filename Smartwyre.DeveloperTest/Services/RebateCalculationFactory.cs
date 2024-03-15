using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class RebateCalculationFactory
    {
        public IRebateCalculationService CreateRebateCalculationService(IncentiveType incentiveType)
        {
            switch (incentiveType)
            {
                case IncentiveType.FixedCashAmount:
                    return new FixedCashAmountService();
                case IncentiveType.FixedRateRebate:
                    return new FixedRateRebateService();
                case IncentiveType.AmountPerUom:
                    return new AmountPerUomService();
                default:
                    throw new ArgumentException("Unsupported incentive type", nameof(incentiveType));
            }
        }
    }
}
