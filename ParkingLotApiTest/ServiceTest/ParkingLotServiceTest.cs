using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using ParkingLotApiTestTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest.ServiceTest
{
    [Collection("test")]
    public class ParkingLotServiceTest : TestBase
    {

        public ParkingLotServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async void Should_add_parkiinglot_successfully()
        {
            // given
            var context = GetCompanyDbContext();

            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };

            ParkingLotService parkingLotService = new ParkingLotService(context);
            // when
            await parkingLotService.AddNewParkingLot(newParkingLotDto);
            // then
            Assert.Equal(1, context.ParkingLots.Count());
        }

        [Fact]
        public async void Should_get_parkiinglot_successfully()
        {
            // given
            var context = GetCompanyDbContext();

            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };

            ParkingLotService parkingLotService = new ParkingLotService(context);
            // when
            await parkingLotService.AddNewParkingLot(newParkingLotDto);
            var getItem = await parkingLotService.GetParkingLot("ParkingLotA");
            // then
            Assert.Equal(1, context.ParkingLots.Count());
        }

        [Fact]
        public async void Should_delete_parkiinglot_successfully()
        {
            // given
            var context = GetCompanyDbContext();

            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };

            ParkingLotService parkingLotService = new ParkingLotService(context);
            // when
            await parkingLotService.AddNewParkingLot(newParkingLotDto);
            await parkingLotService.DeleteParkingLot("ParkingLotA");
            // then
            Assert.Equal(1, context.ParkingLots.Count());
        }

        [Fact]
        public async void Should_update_parkiinglot_successfully()
        {
            // given
            var context = GetCompanyDbContext();

            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };

            var updateParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 150,
                Location = "Zone-A",
                Status = false,
            };

            ParkingLotService parkingLotService = new ParkingLotService(context);
            // when
            await parkingLotService.AddNewParkingLot(newParkingLotDto);
            var returnItem = await parkingLotService.UpdateParkingLot(updateParkingLotDto);
            // then
            Assert.Equal(150, returnItem.Capacity);
        }

        private ParkingLotContext GetCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
            return context;
        }
    }
}
