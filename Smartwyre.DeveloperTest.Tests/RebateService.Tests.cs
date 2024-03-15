using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateServiceTests
    {
        [Fact]
        public void Calculate_FixedCashAmount_SuccessfulRebate()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12345",
                ProductIdentifier = "1000",
                Volume = 10
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 50
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };


            productDataStoreMock.Setup(repo => repo.GetProduct(It.IsAny<string>())).Returns(product);
            rebateDataStoreMock.Setup(repo => repo.GetRebate(It.IsAny<string>())).Returns(rebate);

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            Assert.True(result.Success);
        }
        [Fact]
        public void Calculate_FixedRateRebate_SuccessfulRebate()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12345",
                ProductIdentifier = "1000",
                Volume = 10
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 0.1m // 10%
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 100
            };

            rebateDataStoreMock.Setup(repo => repo.GetRebate(request.RebateIdentifier)).Returns(rebate);
            productDataStoreMock.Setup(repo => repo.GetProduct(request.ProductIdentifier)).Returns(product);

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            Assert.True(result.Success);

        }

        [Fact]
        public void Calculate_AmountPerUom_SuccessfulRebate()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12345",
                ProductIdentifier = "1000",
                Volume = 10
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 5
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            };

            rebateDataStoreMock.Setup(repo => repo.GetRebate(request.RebateIdentifier)).Returns(rebate);
            productDataStoreMock.Setup(repo => repo.GetProduct(request.ProductIdentifier)).Returns(product);

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            Assert.True(result.Success);

        }

        [Fact]
        public void Calculate_RebateNotFound_Failure()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12344",// this record does not exist
                ProductIdentifier = "1000",
                Volume = 10
            };

            rebateDataStoreMock.Setup(repo => repo.GetRebate(request.RebateIdentifier)).Returns((Rebate)null); // Return null for non-existing rebate

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            Assert.False(result.Success);

        }

        [Fact]
        public void Calculate_ProductNotFound_Failure()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12344",
                ProductIdentifier = "1000",// this record does not exist
                Volume = 10
            };

            productDataStoreMock.Setup(repo => repo.GetProduct(request.RebateIdentifier)).Returns((Product)null); // Return null for non-existing product

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            Assert.False(result.Success);

        }

        [Fact]
        public void Calculate_Non_SupportedIncentiveType_Failure()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12345",
                ProductIdentifier = "1000",
                Volume = 10
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 5
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };

            rebateDataStoreMock.Setup(repo => repo.GetRebate(request.RebateIdentifier)).Returns(rebate);
            productDataStoreMock.Setup(repo => repo.GetProduct(request.ProductIdentifier)).Returns(product);

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            Assert.False(result.Success);

        }


        [Fact]
        public void Calculate_FixedCashAmount_StoresCorrectRebateAmount()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12345",
                ProductIdentifier = "1000",
                Volume = 10
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 50
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };

            rebateDataStoreMock.Setup(repo => repo.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStoreMock.Setup(repo => repo.GetProduct(It.IsAny<string>())).Returns(product);

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            rebateDataStoreMock.Verify(repo => repo.StoreCalculationResult(rebate, 50), Times.Once);
        }
        [Fact]
        public void Calculate_FixedRateRebate_StoresCorrectRebateAmount()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12345",
                ProductIdentifier = "1000",
                Volume = 10
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 0.1m // 10%
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 100
            };

            rebateDataStoreMock.Setup(repo => repo.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStoreMock.Setup(repo => repo.GetProduct(It.IsAny<string>())).Returns(product);

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            rebateDataStoreMock.Verify(repo => repo.StoreCalculationResult(rebate, 100), Times.Once);
        }

        [Fact]
        public void Calculate_AmountPerUom_StoresCorrectRebateAmount()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "12345",
                ProductIdentifier = "1000",
                Volume = 10
            };

            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 5,
                Percentage = 0.1m // 10%

            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom,
                Price = 10,
            };

            rebateDataStoreMock.Setup(repo => repo.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStoreMock.Setup(repo => repo.GetProduct(It.IsAny<string>())).Returns(product);

            var service = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

            // Act
            var result = service.Calculate(request);

            // Assert
            rebateDataStoreMock.Verify(repo => repo.StoreCalculationResult(rebate, 50), Times.Once);
        }

    }
}
