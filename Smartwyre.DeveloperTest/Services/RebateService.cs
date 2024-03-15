using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly RebateCalculationFactory _rebateCalculationFactory;


    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateCalculationFactory = new RebateCalculationFactory();

    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        var rebateAmount = 0m;
        if (rebate == null || product == null)
        {
            result.Success = false;
        }
        else
        {
            var rebateCalculator = _rebateCalculationFactory.CreateRebateCalculationService(rebate.Incentive);
            if (!rebateCalculator.IsValidRequest(rebate, product, request))
            {
                result.Success = false;
                return result;
            }

            rebateAmount = rebateCalculator.CalculateRebateAmount(rebate, product, request);
            result.Success = true;

            if (result.Success)
            {
                _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
            }
        }

        return result;
    }


}
