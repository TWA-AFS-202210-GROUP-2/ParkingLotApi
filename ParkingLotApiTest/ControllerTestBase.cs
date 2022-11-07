using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;

namespace ParkingLotApiTest
{
    public class ControllerTestBase
    {
        internal IParkingLotService _parkingLotService;
        internal ParkingLotContext _parkingLotContext;

        public ControllerTestBase()
        {
            var options = new DbContextOptionsBuilder<ParkingLotContext>()
                .UseInMemoryDatabase(databaseName: "ControllerDB")
                .Options;

            _parkingLotContext = new ParkingLotContext(options);
            _parkingLotService = new ParkingLotService(_parkingLotContext);
        }

        public void NewParkingLotData()
        {
            _parkingLotContext.ParkingLots.AddRange(new List<ParkingLotEntity>()
            {
                new ParkingLotEntity(){Name = "AAA",Orders = NewOrderData("AAA"), Capacity = 3, Location = "Liaoning"},
                new ParkingLotEntity(){Name = "BBB",Orders = NewOrderData("BBB"), Capacity = 2, Location = "Beijing"}
            });
            _parkingLotContext.SaveChanges();
        }

        public List<OrderEntity> NewOrderData(string parkingLotName)
        {
            return new List<OrderEntity>()
            {
                new OrderEntity() { ParkingLotName = parkingLotName, PlateNumber = parkingLotName + "A6666",
                    CreationTime = DateTime.Now.ToString()},
                new OrderEntity() { ParkingLotName = parkingLotName, PlateNumber = parkingLotName + "B6666",
                    CreationTime = DateTime.Now.ToString() },
            };
        }

        public void Dispose()
        {
            _parkingLotContext.RemoveRange(_parkingLotContext.Orders);
            _parkingLotContext.RemoveRange(_parkingLotContext.ParkingLots);
            _parkingLotContext.SaveChanges();
        }

        public static HttpClient GetClient()
        {
            var factory = new WebApplicationFactory<Program>();
            return factory.CreateClient();
        }
    }
}
