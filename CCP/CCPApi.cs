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

        public CCPApi(IListOfServices listOfServices, IOrderServiceCCP orderService)
        {
            _listOfServices = listOfServices;
            _orderService = orderService;
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

    }
}
