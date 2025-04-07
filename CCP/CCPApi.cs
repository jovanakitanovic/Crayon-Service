using CCP.CCPServices;
using CCP.Models;
using CrayonService.Shared;
using Moq;

namespace CCP
{
    public class CCPApi : ICCPApi
    {

        private readonly IListOfServices _listOfServices;
        private readonly IOrderServiceCCP _orderService;
        private readonly ISubscriptionEditService _subscriptionEditService;

        public CCPApi(IListOfServices listOfServices, IOrderServiceCCP orderService, ISubscriptionEditService subscriptionEditService)
        {
            _listOfServices = listOfServices;
            _orderService = orderService;
            _subscriptionEditService = subscriptionEditService;
        }

        public async Task<List<Service>> GetListOfServices()
        {
            try
            {
                var listOfServices = new Mock<IListOfServices>();

                listOfServices.Setup(x => x.GetListOfServices()).Returns(Task.FromResult(GetServices()));

                var result = await listOfServices.Object.GetListOfServices();

                return result;
            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }


        public List<Service> GetServices()
        {
            var listOfServices = new List<Service>();

            listOfServices.Add(new Service() { Name = "Microsoft Office", Quantity = 4, ServiceId = Guid.Parse("cc4c8454-7c5a-4611-a351-b1d35a0e39a6") });
            listOfServices.Add(new Service() { Name = "Microsoft Visual Studio", Quantity = 2, ServiceId = Guid.Parse("cb5bfa8f-bbc2-4b4f-88a4-530241c21a94") });
            listOfServices.Add(new Service() { Name = "Jira", Quantity = 14, ServiceId = Guid.Parse("3690717e-33cd-45aa-9287-ab03cc0ffc66") });
            listOfServices.Add(new Service() { Name = "Figma", Quantity = 10, ServiceId = Guid.Parse("a4ec9353-ee8a-49c6-ba5d-6edfc7bedc81") });
            listOfServices.Add(new Service() { Name = "Lucid Chart", Quantity = 8, ServiceId = Guid.Parse("05fdd6ec-0fcc-4871-b46a-69c35c094162") });

            return listOfServices;

        }

        public async Task<OrderedService> OrderService(Guid serviceId, Guid accoutnId)
        {
            try
            {

                var orderService = new Mock<IOrderServiceCCP>();

                orderService.Setup(x => x.OrderServiceFromCCP(serviceId, accoutnId)).Returns(Task.FromResult(OrderServiceData(serviceId)));

                var result = await orderService.Object.OrderServiceFromCCP(serviceId, accoutnId);

                return result;
            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }

        }

        public OrderedService OrderServiceData(Guid serviceId)
        {
            Random random = new Random();
            var service = GetServices().FirstOrDefault(x => x.ServiceId == serviceId);

            if (service != null)
                return new OrderedService
                {
                    LicenceNumber = random.Next(100),
                    ServiceName = service.Name,
                    ValidThrough = DateTime.Now.AddDays(random.Next(100)),
                    ServiceState = random.Next(1),
                    Quantity = service.Quantity
                };

            return null;

        }

        public async Task<bool> CancelService(Guid serviceId)
        {
            try
            {
                var orderService = new Mock<ISubscriptionEditService>();

                orderService.Setup(x => x.CancelSubscription(serviceId)).Returns(Task.FromResult(true));

                var result = await orderService.Object.CancelSubscription(serviceId);

                return result;
            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }

        }

        public async Task<bool> ExtendService(Guid serviceId, DateTime validityDate)
        {
            try
            {
                var orderService = new Mock<ISubscriptionEditService>();

                orderService.Setup(x => x.ExtendsSubscription(serviceId, validityDate)).Returns(Task.FromResult(true));

                var result = await orderService.Object.ExtendsSubscription(serviceId,validityDate);

                return result;
            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }

        }

        public async Task<bool> UpdateServiceQuantity(Guid serviceId, int quantity)
        {
            try
            {
                var orderService = new Mock<ISubscriptionEditService>();

                orderService.Setup(x => x.QuantityUpdateOnSubscription(serviceId, quantity)).Returns(Task.FromResult(true));

                var result = await orderService.Object.QuantityUpdateOnSubscription(serviceId, quantity);

                return result;

            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }

        }

    }
}
