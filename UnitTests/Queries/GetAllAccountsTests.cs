using CrayonService.Queries;
using CrayonService.Repository.AccountRepository;
using CrayonService.Repository.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CrayonService.UnitTests.Queries
{
    [TestClass]
    public class GetAllAccountsTests
    {

        private static GetAllAccounts.QueryHandler _getAllAccountsMock;
        private static Mock<IAccountCustomerLinkRepository> _customerLinkRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            _customerLinkRepository = new Mock<IAccountCustomerLinkRepository>();
            _getAllAccountsMock = new GetAllAccounts.QueryHandler(_customerLinkRepository.Object);
        }


        [TestMethod]
        public void GettingListOfAllRepositories_noAccountsTiedToCustomer()
        {
            MockListOfAccounts(new List<AccountCustomerLink>());
            var query = new GetAllAccounts.Query()
            {
                CustomerId = Guid.Parse("6764668c-0e4c-4359-8152-afef3adad4ea")
            };

            var result = _getAllAccountsMock.Handle(query, new CancellationToken()).Result;

            Assert.AreEqual(result.AccountIds.Count, 0);
        }

        [TestMethod]
        public void GettingListOfAllRepositories_FullListOdAccounts()
        {
            MockListOfAccounts(GetTestAccountCustomerLinks());

            var query = new GetAllAccounts.Query()
            {
                CustomerId = Guid.Parse("6764668c-0e4c-4359-8152-afef3adad4ea")
            };

            var result = _getAllAccountsMock.Handle(query, new CancellationToken()).Result;

            Assert.AreEqual(result.AccountIds.Count, 2);

            Assert.AreEqual(result.AccountIds.ElementAt(0), Guid.Parse("d9e42b3f-5735-4344-b02d-61f510526461"));
            Assert.AreEqual(result.AccountIds.ElementAt(1), Guid.Parse("06cc76f8-5019-4a9c-84b9-b2bd162c38e7"));
        }

        private void MockListOfAccounts(List<AccountCustomerLink> returnValue)
        {
            _customerLinkRepository.Setup(x => x.GetAllAccountsByCustomerId(It.IsAny<Guid>())).Returns(Task.FromResult(returnValue));
        }

        private List<AccountCustomerLink> GetTestAccountCustomerLinks()
        {
            return new List<AccountCustomerLink>()
            {
                new AccountCustomerLink()
                {
                    AccountCustomerLinkId = Guid.Parse("b13796f5-6767-47d3-948b-8ab320a9768b"),
                    AccountId=Guid.Parse("d9e42b3f-5735-4344-b02d-61f510526461"),
                    CustomerId = Guid.Parse("ae0b31cc-c052-48aa-a4c6-c6548abc7650")
                },
                new AccountCustomerLink()
                {
                    AccountCustomerLinkId = Guid.Parse("ef438fe6-2375-407d-ae94-27775680b4b7"),
                    AccountId=Guid.Parse("06cc76f8-5019-4a9c-84b9-b2bd162c38e7"),
                    CustomerId = Guid.Parse("ca3531bc-6ff6-4936-8c93-241230377297")
                }
            };
        }
    }
}
