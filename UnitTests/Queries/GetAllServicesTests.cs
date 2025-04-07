using CCP;
using CCP.Models;
using CrayonService.Queries;
using CrayonService.Queries.Validators;
using CrayonService.Repository.AccountRepository;
using CrayonService.Repository.Models;
using CrayonService.Shared;
using CrayonService.Shared.Models;
using MediatR;
using Moq;

namespace CrayonService.UnitTests.Queries
{
    [TestClass]
    public class GetAllServicesTests
    {
        private static GetAllServices.QueryHandler _getAllServicesMock;
        private static Mock<ICCPApi> _ccpApiMock;

        [TestInitialize]
        public void InitializeTest()
        {
            _ccpApiMock = new Mock<ICCPApi>();
            _getAllServicesMock = new GetAllServices.QueryHandler(_ccpApiMock.Object);
        }


        [TestMethod]
        public void GettingAllServices_noServicesRetruned()
        {
            MockListOfServices(new List<Service>());
            var query = new GetAllServices.Query(){};

            var result = _getAllServicesMock.Handle(query, new CancellationToken()).Result;

            Assert.AreEqual(result.Count, 0);
        }


        [TestMethod]
        public void GettingAllServices_multipleServicesReturned()
        {
            var returnResults = GetTestServices();

            MockListOfServices(GetTestServices());
            var query = new GetAllServices.Query() { };

            var result = _getAllServicesMock.Handle(query, new CancellationToken()).Result;

            Assert.AreEqual(result.Count, returnResults.Count);

            for(int i = 0; i < returnResults.Count; i++)
            {
                var mockData = returnResults[i];
                var enpointData = result[i];

                Assert.AreEqual(mockData.Name, enpointData.Name);
                Assert.AreEqual(mockData.ServiceId, enpointData.ServiceId);
                Assert.AreEqual(mockData.Quantity, enpointData.Quantity);
            }

        }

        private void MockListOfServices(List<Service> returnValue)
        {
            _ccpApiMock.Setup(x => x.GetListOfServices()).Returns(Task.FromResult(returnValue));
        }

        private List<Service> GetTestServices()
        {
            return new List<Service>()
            {
                new Service()
                {
                    Name = "test service",
                    Quantity = 123,
                    ServiceId = Guid.Parse("d50cade0-b60a-400f-a3ea-2dd8df962d16")
                },
                new Service() {
                    Name ="test service 2",
                    Quantity = 345,
                    ServiceId = Guid.Parse("b2feeff2-4322-4932-be89-81a7719ddbd5")
                }
            };
        }

    }
}
