using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the RebateDataStore and ProductDataStore
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        // Initialize the RebateService
        var rebateService = new RebateService(rebateDataStore, productDataStore);

        // Accept user inputs
        Console.WriteLine("Enter Rebate Identifier:");
        var rebateIdentifier = Console.ReadLine();

        Console.WriteLine("Enter Product Identifier:");
        var productIdentifier = Console.ReadLine();

        Console.WriteLine("Enter Volume:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal volume))
        {
            Console.WriteLine("Invalid volume entered. Please enter a valid numeric value.");
            return;
        }

        // Create the CalculateRebateRequest
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };

        // Call the Calculate method of the RebateService
        CalculateRebateResult result = rebateService.Calculate(request);

        // Display the result
        if (result.Success)
        {
            Console.WriteLine("Rebate calculation successful.");
        }
        else
        {
            Console.WriteLine("Rebate calculation failed.");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
