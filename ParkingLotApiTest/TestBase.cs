using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;
using ParkingLotApi.Repository;
using ParkingLotApiTestTest;
using ParkingLotApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Services;

namespace ParkingLotApiTest
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        public TestBase(CustomWebApplicationFactory<Program> factory)
        {
            this.Factory = factory;
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ParkingLotContext>();
        }

        protected CustomWebApplicationFactory<Program> Factory { get; }

        public void Dispose()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ParkingLotContext>();
            context.Orders.RemoveRange(context.Orders);
            context.ParkingLots.RemoveRange(context.ParkingLots);

            context.SaveChanges();
        }

        protected HttpClient GetClient()
        {
            return Factory.CreateClient();
        }

        public List<OrderEntity> OrderData(string parkingLotName)
        {
            return new List<OrderEntity>()
            {
                new OrderEntity() { ParkingLotName = parkingLotName, PlateNumber = parkingLotName + "A6666",
                    CreationTime = DateTime.Now.ToString()},
                new OrderEntity() { ParkingLotName = parkingLotName, PlateNumber = parkingLotName + "B6666",
                    CreationTime = DateTime.Now.ToString() },
            };
        }
    }
}
