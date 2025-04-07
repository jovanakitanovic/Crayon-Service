using CrayonService.Command;
using CrayonService.Repository.Models;
using CrayonService.Repository.Repository;
using CrayonService.Shared.Models;
using CrayonService.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP;
using CrayonService.Repository.AccountRepository;
using OrderServiceModel = CCP.Models.OrderedService;


namespace CrayonService.UnitTests.Commands
{
    [TestClass]
    public class OrderServiceTest
    {

        private static OrderService.CommandHandler _orderServiceMock;
        private static Mock<ICCPApi> _ccpMock;
        private static Mock<IAccountRepository> _accountRepositoryMock;
        private static Mock<ISubscriptionsRepository> _subscriptionRepositoryMock;

        [TestInitialize]
        public void InitializeTest()
        {
            _ccpMock = new Mock<ICCPApi>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _subscriptionRepositoryMock = new Mock<ISubscriptionsRepository>();
            _orderServiceMock = new OrderService.CommandHandler(_ccpMock.Object, _accountRepositoryMock.Object, _subscriptionRepositoryMock.Object);
        }


        [TestMethod]
        public void OrderServiceTest_accountInvalid()
        {
            MockAccoountRepository(null);

            var command = CommandData();

            try
            {
                var result = _orderServiceMock.Handle(command, new CancellationToken()).Result;

                Assert.Fail();

            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.InnerException is CustomBadRequestException);
            }
        }

        [TestMethod]
        public void OrderServiceTest_OrderServiceInvalid()
        {
            MockAccoountRepository(GetAccountForMock());
            MockServiceOrderSerivce(null);

            var command = CommandData();

            try
            {
                var result = _orderServiceMock.Handle(command, new CancellationToken()).Result;

                Assert.Fail();

            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.InnerException is CustomBadRequestException);
            }
        }

        [TestMethod]
        public void OrderServiceTest_successful()
        {
            MockAccoountRepository(GetAccountForMock());

            var orderServiceMockModel = GetOrderServiceMock();
            MockServiceOrderSerivce(orderServiceMockModel);

            var subscriotionId = Guid.NewGuid();
            MockInsertSubscriptionMock(subscriotionId);

            var command = CommandData();

            var result = _orderServiceMock.Handle(command, new CancellationToken()).Result;


            Assert.AreEqual(orderServiceMockModel.ServiceName, result.ServiceName);

            var status = Enum.TryParse(result.ServiceState, out ServiceStatus serviceStatus);
            Assert.AreEqual(orderServiceMockModel.ServiceState, (int)serviceStatus);
            Assert.AreEqual(orderServiceMockModel.ValidThrough, result.ValidThrough );
            Assert.AreEqual(orderServiceMockModel.Quantity, result.Quantity );
            Assert.AreEqual(subscriotionId, result.SubcsriptionId );
        }


        private void MockAccoountRepository(Accounts returnValueAccountId)
        {
            _accountRepositoryMock.Setup(x => x.GetAccountById(It.IsAny<Guid>())).Returns(Task.FromResult(returnValueAccountId));
        }

        private void MockServiceOrderSerivce(OrderServiceModel returnValueOrderService)
        {
            _ccpMock.Setup(x => x.OrderService(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(returnValueOrderService));
        }

        private void MockInsertSubscriptionMock(Guid returnValue)
        {
            _subscriptionRepositoryMock.Setup(x => x.InsertSubscription(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<OrderServiceModel>())).Returns(Task.FromResult(returnValue));
        }

        private OrderService.Command CommandData()
        {
            return new OrderService.Command()
            {
                AccountId = Guid.Parse("6764668c-0e4c-4359-8152-afef3adad4ea"),
                ServiceId = Guid.Parse("471307fc-3c47-49d9-bd53-1256b9083953")
            };
        }

        private Accounts GetAccountForMock()
        {
            return new Accounts()
            {
                AccountId = Guid.Parse("22770fbf-a3b4-413a-a387-d7e99de99bb8")
            };
        }

        private OrderServiceModel GetOrderServiceMock()
        {
            return new OrderServiceModel()
            {
                ServiceName = "test service name",
                LicenceNumber = 12,
                Quantity = 2,
                ServiceState = 1,
                ValidThrough = DateTime.ParseExact("2025-04-04 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
        }

    }
}
