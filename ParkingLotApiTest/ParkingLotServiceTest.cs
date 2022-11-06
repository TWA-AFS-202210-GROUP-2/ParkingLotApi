using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dto;
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
    public class ParkingLotServiceTest : ServiceTestBase
    {
        [Fact]
        public async void Should_return_parkinglot_with_orders_when_get_all()
        {
            NewParkingLotData();
            var parkinglots = _parkingLotService.GetAll(0);
            Assert.Equal(2, parkinglots.Count);
        }

        [Fact]
        public async void Should_return_15parkinglot_when_get_by_page1_given_18parkinglot()
        {
            for (int i = 0; i < 9; i = i + 1)
            {
                NewParkingLotData();
            }

            var list = _parkingLotService.GetAll(0);
            Assert.Equal(15, list.Count);
        }

        [Fact]
        public async void Should_change_order_info_when_update_by_close_a_order()
        {
            NewParkingLotData();
            _parkingLotService.UpdateOrder(1);
            var order = _parkingLotContext.Orders.Where(_=> _.Id == 1).FirstOrDefault();
            Assert.Equal(false, order.IsOpen);
        }

        [Fact]
        public async void Should_return_badrequest_when_create_out_of_capacity()
        {
            NewParkingLotData();
            var parkinglot = _parkingLotContext.ParkingLots.Where(_ => _.Capacity == 2).FirstOrDefault();
            var msgNum = await _parkingLotService.CreateOrder(parkinglot.Id, new OrderDto(parkinglot.Orders.FirstOrDefault()));
            Assert.Equal(0, msgNum);
        }
    }
}
