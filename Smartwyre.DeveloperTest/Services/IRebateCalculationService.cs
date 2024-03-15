using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IRebateCalculationService
    {
        public bool IsValidRequest(Rebate rebate, Product product, CalculateRebateRequest request);

        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
