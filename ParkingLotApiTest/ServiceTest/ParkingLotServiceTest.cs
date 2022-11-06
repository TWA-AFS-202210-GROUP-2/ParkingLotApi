using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ParkingLotApi.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ParkingLotApi.Repository;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi.Service;

namespace ParkingLotApiTest.ServiceTest
{
    public class ParkingLotServiceTest:TestBase
    {
        public ParkingLotServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
            

        }

        private ParkingLotContext GetParkingLotContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            ParkingLotContext context = scopedService.GetRequiredService<ParkingLotContext>();
            return context;
        }
        [Fact]
        public async Task Should_create_parkingLot_via_service_success()
        {
            //given
            
            var parkinglotDto = new ParkingLotDto(name: "SLB", capacity: 100, location: "tuspark");

            var context = GetParkingLotContext();
            //when
            ParkingLotService parkingLotService = new ParkingLotService(context);
            //then
            parkingLotService.AddParkingLot(parkinglotDto);
            Assert.Equal(1, context.ParkingLotEntities.Count());
        }
        [Fact]
        public async Task Should_delete_parkingLot_via_service_success()
        {
            //given

            var parkinglotDto = new ParkingLotDto(name: "SLB", capacity: 100, location: "tuspark");
            var context = GetParkingLotContext();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            var id = await parkingLotService.AddParkingLot(parkinglotDto);
            //when
            parkingLotService.DeleteParkingLot(id);
            //then

            Assert.Equal(0, context.ParkingLotEntities.Count());
        }
        [Fact]
        public async Task Should_show_15_item_by_selected_id()
        {
            //given
            var context = GetParkingLotContext();
            List<ParkingLotDto> list = new List<ParkingLotDto>();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            for (int i = 0; i < 100; i++)
            {
                await parkingLotService.AddParkingLot(
                    new ParkingLotDto(name: "SLB"+ i.ToString(), capacity: 100, location: "tuspark"));
            }
            Assert.Equal(100,context.ParkingLotEntities.Count());

            //when
            var returnList = parkingLotService.GetbyPage(1);
            //then

            Assert.Equal(15, returnList.Result.Count);
        }
        [Fact]
        public async Task Should_get_item_item_by_selected_id()
        {
            //given
            var context = GetParkingLotContext();
            List<ParkingLotDto> list = new List<ParkingLotDto>();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            for (int i = 0; i < 10; i++)
            {
                await parkingLotService.AddParkingLot(
                    new ParkingLotDto(name: "SLB" + i.ToString(), capacity: 100, location: "tuspark"));
            }
            //when
            var returnDto = parkingLotService.GetbyId(1);
            //then

            Assert.Equal("SLB"+"0",returnDto.Result.Name);
        }
        [Fact]
        public async Task Should_update_item_item_by_selected_id()
        {
            //given
            var context = GetParkingLotContext();
            List<ParkingLotDto> list = new List<ParkingLotDto>();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            for (int i = 0; i < 10; i++)
            {
                await parkingLotService.AddParkingLot(
                    new ParkingLotDto(name: "SLB" + i.ToString(), capacity: 100, location: "tuspark"));
            }

            var ToBeUpdated = parkingLotService.GetbyId(1);
            ToBeUpdated.Result.Capacity = 200;
            //when
            var returnDto = parkingLotService.UpdateParkingLot(1,ToBeUpdated.Result);
            //then
            ToBeUpdated = parkingLotService.GetbyId(1);
            Assert.Equal(200, ToBeUpdated.Result.Capacity);
        }

    }
}
