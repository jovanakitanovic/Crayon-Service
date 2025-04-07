using CCP.Models;
using CrayonService.Queries;
using CrayonService.Repository.AccountRepository;
using CrayonService.Repository.Models;
using CrayonService.Repository.Repository;
using CrayonService.Shared.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.UnitTests.Queries
{
    [TestClass]
    public class GetAllServicesForAccountTests
    {
        private static GetAllServicesForAccount.QueryHandler _getAllServicesForAccount;
        private static Mock<ISubscriptionsRepository> _subscriptionRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _subscriptionRepository = new Mock<ISubscriptionsRepository>();
            _getAllServicesForAccount = new GetAllServicesForAccount.QueryHandler(_subscriptionRepository.Object);
        }

        [TestMethod]
        public void GetAllServicesForAccountTests_noServicesReturned()
        {
            MockListOfServicesForAccount(new List<ServiceOrder>());
            var query = new GetAllServicesForAccount.Query()
            {
                AccountId = Guid.Parse("6764668c-0e4c-4359-8152-afef3adad4ea")
            };

            var result = _getAllServicesForAccount.Handle(query, new CancellationToken()).Result;

            Assert.AreEqual(result.Count, 0);
        }


        [TestMethod]
        public void GetAllServicesForAccountTests_MultipleServicesReturned()
        {
            var returnResults = GetListOfOrderedServices();

            MockListOfServicesForAccount(returnResults);

            var query = new GetAllServicesForAccount.Query()
            {
                AccountId = Guid.Parse("6764668c-0e4c-4359-8152-afef3adad4ea")
            };

            var result = _getAllServicesForAccount.Handle(query, new CancellationToken()).Result;

            Assert.AreEqual(result.Count, result.Count);

            for (int i = 0; i < returnResults.Count; i++)
            {
                var mockData = returnResults[i];
                var enpointData = result[i];
                var status = Enum.TryParse(enpointData.ServiceState, out ServiceStatus serviceStatus);

                Assert.AreEqual(mockData.ServiceName, enpointData.ServiceName);
                Assert.AreEqual(mockData.ServiceSubscripitonId, enpointData.SubcsriptionId);
                Assert.AreEqual(mockData.Quantity, enpointData.Quantity);
                Assert.AreEqual(mockData.State, (int)serviceStatus);
                Assert.AreEqual(mockData.ValidThrough, enpointData.ValidThrough);
            }
        }



        private void MockListOfServicesForAccount(List<ServiceOrder> returnValue)
        {
            _subscriptionRepository.Setup(x => x.GetAllSubscriptionsForAccount(It.IsAny<Guid>())).Returns(Task.FromResult(returnValue));
        }

        private List<ServiceOrder> GetListOfOrderedServices()
        {
            return new List<ServiceOrder>()
            {
                new ServiceOrder()
                {
                    Quantity = 1,
                    ServiceId = Guid.Parse("49e3ccb0-c835-45b4-a630-a319343002cb"),
                    ServiceName = "test service name",
                    ServiceSubscripitonId = Guid.Parse("b13796f5-6767-47d3-948b-8ab320a9768b"),
                    State = 1,
                    ValidThrough = DateTime.ParseExact("2025-04-04 12:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                },
                new ServiceOrder()
                {
                    Quantity = 2,
                    ServiceId = Guid.Parse("ef438fe6-2375-407d-ae94-27775680b4b7"),
                    ServiceName = "test service name 2",
                    ServiceSubscripitonId = Guid.Parse("06cc76f8-5019-4a9c-84b9-b2bd162c38e7"),
                    State = 0,
                    ValidThrough = DateTime.ParseExact("2025-04-04 13:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                }
            };
        }
    }
}
