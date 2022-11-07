using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApiTest;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    public class ServiceTest : TestBase
    {
        public ServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_Add_parking()
        {
            // given
            var context = GetparkingDbContext();
            parkingDto parkingDto = new parkingDto();
            parkingDto.Name = "IBM";
            parkingDto.capacity = 100;
            parkingDto.location = "beijing";
            parkingDto.orderDtos = new List<orderDto>
            {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
            };
            parkingService parkingService = new parkingService(context);

            // when
            await parkingService.Addparking(parkingDto);

            // then
            Assert.Equal(1, context.Parkings.Count());
        }

        [Fact]
        public async Task Should_Get_All_parking()
        {
            // given
            var context = GetparkingDbContext();
            parkingDto parkingDto = new parkingDto();
            parkingDto.Name = "IBM";
            parkingDto.capacity = 100;
            parkingDto.location = "beijing";
            parkingDto.orderDtos = new List<orderDto>
            {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
            };
            parkingService parkingService = new parkingService(context);

            // when
            var parkings = await parkingService.GetAll();
            // then
            Assert.Equal(1, parkings.Count());
        }


        [Fact]
        public async Task Should_Get_parking_By_Id()
        {
            // given
            var context = GetparkingDbContext();
            parkingDto parkingDto = new parkingDto();
            parkingDto.Name = "IBM";
            parkingDto.capacity = 100;
            parkingDto.location = "beijing";
            parkingDto.orderDtos = new List<orderDto>
            {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
            };
            parkingService parkingService = new parkingService(context);

            // when
            var parkingid = await parkingService.Addparking(parkingDto);
            var parking = await parkingService.GetById(parkingid);
            // then
            Assert.Equal(parkingDto.Name, parking.Name);
        }

        [Fact]
        public async Task Should_Delete_parking()
        {
            // given
            var context = GetparkingDbContext();
            parkingDto parkingDto = new parkingDto();
            parkingDto.Name = "IBM";
            parkingDto.capacity = 100;
            parkingDto.location = "beijing";
            parkingDto.orderDtos = new List<orderDto>
            {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
            };
            parkingService parkingService = new parkingService(context);

            // when
            var parkingid = await parkingService.Addparking(parkingDto);
            await parkingService.Deleteparking(parkingid);
            // then
            Assert.Equal(0, context.Parkings.Count());
        }
        private parkingDbContext GetparkingDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            parkingDbContext context = scopedService.GetRequiredService<parkingDbContext>();
            return context;
        }
    }
}