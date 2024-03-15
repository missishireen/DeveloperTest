using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    // This is just to allow manual testing
    private readonly Dictionary<string, Rebate> _rebateData;

    public RebateDataStore()
    {
        _rebateData = new Dictionary<string, Rebate>();
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        _rebateData.Add("12345", new Rebate
        {
            Identifier = "12345",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 50
        });

        _rebateData.Add("54333", new Rebate
        {
            Identifier = "54333",
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 0.1m // 10%
        });
    }

    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 

        if (_rebateData.ContainsKey(rebateIdentifier))
        {
            return _rebateData[rebateIdentifier];
        }
        else
        {
            return null;
        }
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }
}
