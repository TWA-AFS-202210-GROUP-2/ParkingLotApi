namespace EFCoreRelationshipsPracticeTest
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.Extensions.DependencyInjection;
    using ParkingLotApi.Model;
    using ParkingLotApi.Repository;

    public class TestBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        internal ParkingLotContext _parkingLotContext;

        public TestBase(CustomWebApplicationFactory<Program> factory)
        {
            this.Factory = factory;
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            _parkingLotContext = scopedServices.GetRequiredService<ParkingLotContext>();
        }

        protected CustomWebApplicationFactory<Program> Factory { get; }

        public void Dispose()
        {
            _parkingLotContext.ParkingLots.RemoveRange(_parkingLotContext.ParkingLots);

            _parkingLotContext.SaveChanges();
        }

        public void NewParkingLotData()
        {
            _parkingLotContext.ParkingLots.AddRange(new List<ParkingLotEntity>()
            {
                new ParkingLotEntity()
                    { Name = "AAA", Orders = NewOrderData("AAA"), Capacity = 3, Location = "Liaoning" },
                new ParkingLotEntity()
                    { Name = "BBB", Orders = NewOrderData("BBB"), Capacity = 2, Location = "Beijing" }
            });
            _parkingLotContext.SaveChanges();
        }

        public List<OrderEntity> NewOrderData(string parkingLotName)
        {
            return new List<OrderEntity>()
            {
                new OrderEntity()
                {
                    ParkingLotName = parkingLotName, PlateNumber = parkingLotName + "A6666",
                    CreationTime = DateTime.Now.ToString()
                },
                new OrderEntity()
                {
                    ParkingLotName = parkingLotName, PlateNumber = parkingLotName + "B6666",
                    CreationTime = DateTime.Now.ToString()
                },
            };
        }

        protected HttpClient GetClient()
        {
            return Factory.CreateClient();
        }
    }
}