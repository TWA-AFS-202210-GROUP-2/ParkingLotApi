using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Service;

namespace ParkingLotApiTest.ServiceTest
{
    public class OrderServiceTest:TestBase
    {
        public OrderServiceTest(CustomWebApplicationFactory<Program> factory):base(factory)
        {}
        private ParkingLotContext GetParkingLotContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
            return context;
        }
        [Fact]
        public async Task Should_create_order_via_service_success()
        {
            //given

            var context = GetParkingLotContext();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            OrderService orderService = new OrderService(context);

            var parkinglotDto = new ParkingLotDto(name: "SLB", capacity: 100, location: "tuspark");
            var newOrder = new OrderDto
            {
                OrderNumber = "asd",
                ParkingLotName = "SLB",
                PlateNumber = "AABB",
                CreateTime = "null",
                CloseTime = "asdas",
                IsOpen = true
            };
            parkingLotService.AddParkingLot(parkinglotDto);

            //when
            var res = orderService.CreateOrder(1, newOrder);
            //then

            Assert.Equal(1, context.OderEntities.Count());
        }
        [Fact]
        public async Task Should_update_order_info_when_update()
        {
            //given

            var context = GetParkingLotContext();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            OrderService orderService = new OrderService(context);

            var parkinglotDto = new ParkingLotDto(name: "SLB", capacity: 100, location: "tuspark");
            var newOrder = new OrderDto
            {
                OrderNumber = "asd",
                ParkingLotName = "SLB",
                PlateNumber = "AABB",
                CreateTime = "null",
                CloseTime = "asdas",
                IsOpen = true
            };
            parkingLotService.AddParkingLot(parkinglotDto);
            await orderService.CreateOrder(1, newOrder);
            //when
            newOrder.IsOpen = false;
            var res = orderService.UpdateOrder(1,1, newOrder);
            //then
            Assert.Equal(false, res.Result.IsOpen);
        }
    }
}
