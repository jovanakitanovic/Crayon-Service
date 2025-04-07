using CCP;
using CrayonService.Command;
using CrayonService.Queries;
using CrayonService.Repository.AccountRepository;
using CrayonService.Repository.Models;
using CrayonService.Repository.Repository;
using CrayonService.Shared;
using CrayonService.Shared.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.UnitTests.Commands
{
    [TestClass]
    public class CancelSubscriptionTests
    {
        private static CancelSubscription.CommandHandler _cancelSubscriptionMock;
        private static Mock<ISubscriptionsRepository> _subscriptionRepository;
        private static Mock<ICCPApi> _ccpApiMock;

        [TestInitialize]
        public void InitializeTest()
        {
            _subscriptionRepository = new Mock<ISubscriptionsRepository>();
            _ccpApiMock = new Mock<ICCPApi>();
            _cancelSubscriptionMock = new CancelSubscription.CommandHandler(_subscriptionRepository.Object, _ccpApiMock.Object);
        }


        [TestMethod]
        public void CancelSubscription_invalidSubscription()
        {
            MockServiceVerifySubscriptions(false);

            var command = CommandData();

            try
            {
                var result = _cancelSubscriptionMock.Handle(command, new CancellationToken()).Result;

                Assert.Fail();

            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.InnerException is CustomBadRequestException);
            }
        }

        [TestMethod]
        public void CancelSubscription_ccpCancel_failed()
        {
            MockServiceVerifySubscriptions(true);
            MockUpdateCCPSubscriptions(false);

            var command = CommandData();

            try
            {
                var result = _cancelSubscriptionMock.Handle(command, new CancellationToken()).Result;

                Assert.Fail();

            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.InnerException is CustomInternalServerError);
            }
        }

        [TestMethod]
        public void CancelSubscription_successful()
        {
            var returnMockData = GetServiceData();

            MockServiceVerifySubscriptions(true);
            MockCancelSubscription(returnMockData);
            MockUpdateCCPSubscriptions(true);

            var command = CommandData();

            var result = _cancelSubscriptionMock.Handle(command, new CancellationToken()).Result;

            Assert.AreEqual(returnMockData.ServiceName, result.ServiceName);

            var status = Enum.TryParse(result.ServiceState, out ServiceStatus serviceStatus);
            Assert.AreEqual(returnMockData.State, (int)serviceStatus);
            Assert.AreEqual(returnMockData.ValidThrough, result.ValidThrough);
            Assert.AreEqual(returnMockData.Quantity, result.Quantity);
            Assert.AreEqual(returnMockData.ServiceSubscripitonId, result.SubcsriptionId);
        }

        private void MockServiceVerifySubscriptions(bool returnValue)
        {
            _subscriptionRepository.Setup(x => x.VerifySubscriptionst(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(returnValue));
        }

        private void MockCancelSubscription(ServiceOrder returnValueServiceOrder)
        {
            _subscriptionRepository.Setup(x => x.CancelSubscription(It.IsAny<Guid>())).Returns(Task.FromResult(returnValueServiceOrder));
        }

        private void MockUpdateCCPSubscriptions(bool returnValue)
        {
            _ccpApiMock.Setup(x => x.CancelService(It.IsAny<Guid>())).Returns(Task.FromResult(returnValue));
        }

        private CancelSubscription.Command CommandData()
        {
            return new CancelSubscription.Command()
            {
                AccountId = Guid.Parse("6764668c-0e4c-4359-8152-afef3adad4ea"),
                SubscriptionId = Guid.Parse("774c620f-51be-42a5-a375-f7501da4cbab")
            };
        }

        private ServiceOrder GetServiceData()
        {
            return new ServiceOrder()
            {
                Quantity = 1,
                ServiceId = Guid.Parse("49e3ccb0-c835-45b4-a630-a319343002cb"),
                ServiceName = "test service name",
                ServiceSubscripitonId = Guid.Parse("b13796f5-6767-47d3-948b-8ab320a9768b"),
                State = 3,
                ValidThrough = DateTime.ParseExact("2025-04-04 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };

        }

    }
}
