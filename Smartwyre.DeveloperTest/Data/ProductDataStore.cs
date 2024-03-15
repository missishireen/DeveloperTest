using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    // This is just to allow manual testing
    private readonly Dictionary<string, Product> _productData;

    public ProductDataStore()
    {
        _productData = new Dictionary<string, Product>();
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        _productData.Add("1000", new Product { Identifier = "1000", Price = 10, SupportedIncentives = SupportedIncentiveType.FixedRateRebate });
        _productData.Add("1001", new Product { Identifier = "1001", Price = 100, SupportedIncentives = SupportedIncentiveType.AmountPerUom });
        _productData.Add("1002", new Product { Identifier = "1002", Price = 15, SupportedIncentives = SupportedIncentiveType.FixedCashAmount });

    }

    public Product GetProduct(string productIdentifier)
    {
        if (_productData.ContainsKey(productIdentifier))
        {
            return _productData[productIdentifier];
        }
        else
        {
            return null;
        }
    }
}
