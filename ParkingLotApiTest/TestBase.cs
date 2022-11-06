using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace ParkingLotApiTest
{
    public class TestBase
    {
        internal IParkingLotService _parkingLotService;
        internal ParkingLotContext _parkingLotContext;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<ParkingLotContext>()
                .UseInMemoryDatabase(databaseName: "DB")
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
    }
}