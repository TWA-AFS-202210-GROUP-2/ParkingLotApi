using Microsoft.EntityFrameworkCore;
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
        public async void Should_return_3parkinglot_when_get_by_page2_given_18parkinglot()
        {
            for (int i = 0; i < 9; i = i + 1)
            {
                NewParkingLotData();
            }

        }

    }
}
