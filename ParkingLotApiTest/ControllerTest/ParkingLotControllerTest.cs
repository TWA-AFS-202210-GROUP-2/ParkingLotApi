using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NuGet.Frameworks;
using ParkingLotApi.Dtos;
using ParkingLotApiTestTest;
using ParkingLotApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ParkingLotApi.Models;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async void Should_return_parkinglot_when_post_given_new_parkinglot()
        {
            // given
            var httpClient = this.SetUpHttpClient();

            await httpClient.DeleteAsync("api/parkinglots");

            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };

            // when
            var response = await this.PostNewParkingLot(newParkingLotDto);

            // then
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var addedParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody);
            Assert.Equal(newParkingLotDto.Capacity, addedParkingLot.Capacity);
        }

        [Fact]
        public async void Should_delete_parkinglot_when_sold_to_other_successfully()
        {
            // given
            var httpClient = this.SetUpHttpClient();
            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };

            // when
            await this.PostNewParkingLot(newParkingLotDto);
            var returnString = await httpClient.DeleteAsync($"api/parkinglots/{"ParkingLotA"}");
            Assert.Equal(HttpStatusCode.NoContent, returnString.StatusCode);
        }

        [Fact]
        public async void Should_return_parkingLot_when_get_given_parkinglot_name()
        {
            // given
            var httpClient = this.SetUpHttpClient();

            for (int i = 0; i < 20; i++)
            {
                var newParkingLotDto = new ParkingLotDto()
                {
                    Name = $"ParkingLot{i + 1}",
                    Capacity = 100,
                    Location = "Zone-A",
                    Status = false,
                };
                await this.PostNewParkingLot(newParkingLotDto);
            }

            // when
            var response = await httpClient.GetAsync($"api/parkinglots/parkingLot{2}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parkingLotDto = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody);

            // then
            Assert.Equal("ParkingLot2", parkingLotDto.Name);
        }

        [Fact]
        public async void Should_return_parkingLot_list_by_pages_successfully()
        {
            // given
            var httpClient = this.SetUpHttpClient();

            for (int i = 0; i < 20; i++)
            {
                var newParkingLotDto = new ParkingLotDto()
                {
                    Name = $"ParkingLot{i + 1}",
                    Capacity = 100,
                    Location = "Zone-A",
                    Status = false,
                };
                await this.PostNewParkingLot(newParkingLotDto);
            }

            // when
            var response = await httpClient.GetAsync($"api/parkinglots?pageIndex=1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parkingLotDtos = JsonConvert.DeserializeObject<List<ParkingLotDto>>(responseBody);

            // then
            Assert.Equal(15, parkingLotDtos.Count());
        }

        [Fact]
        public async void Should_return_updated_parkinglot_when_update_given_new_capacity()
        {
            // given
            var httpClient = this.SetUpHttpClient();
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

            await this.PostNewParkingLot(newParkingLotDto);
            // when
            var updateResponse = await this.PutUpdateParkingLot(updateParkingLotDto);
            var body = await updateResponse.Content.ReadAsStringAsync();
            var returnParkingLotDto = JsonConvert.DeserializeObject<ParkingLotDto>(body);

            Assert.Equal(150, returnParkingLotDto.Capacity);
        }

        [Fact]
        public async void Should_new_a_order_when_create_parkinglot_order()
        {
            // given
            var httpClient = this.SetUpHttpClient();
            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };
            await this.PostNewParkingLot(newParkingLotDto);

            var orderDto = new OrderDto()
            {
                ParkingLotName = "ParkingLotA",
                PlateNumber = "001",
                CreationTime = DateTime.Now.ToString(),
            };

            // when
            var response = await this.PostNewOrder(newParkingLotDto.Name, orderDto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // then
            var body = await response.Content.ReadAsStringAsync();
            var returnObject = JsonConvert.DeserializeObject<int>(body);
            Assert.NotNull(returnObject);
        }

        [Fact]
        public async void Should_change_a_order_when_create_delete_parkinglot_order()
        {
            // given
            var httpClient = this.SetUpHttpClient();
            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };
            await this.PostNewParkingLot(newParkingLotDto);

            var orderDto = new OrderDto()
            {
                ParkingLotName = "ParkingLotA",
                PlateNumber = "001",
                CreationTime = DateTime.Now.ToString(),
            };
            // when
            var response = await this.PostNewOrder(newParkingLotDto.Name, orderDto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var body = await response.Content.ReadAsStringAsync();
            var returnObject = JsonConvert.DeserializeObject<int>(body);

            var putResponse = await this.SetUpHttpClient().PutAsync($"api/parkinglots/{newParkingLotDto.Name}/{returnObject}", null);
            var putBody = await putResponse.Content.ReadAsStringAsync();
            var putOrderDto = JsonConvert.DeserializeObject<OrderDto>(putBody);
            // then

            Assert.Equal(false, putOrderDto.IsOpen);
        }

        private async Task<HttpResponseMessage> PutUpdateParkingLot(ParkingLotDto updateParkingLotDto)
        {
            var parkingLotJson = JsonConvert.SerializeObject(updateParkingLotDto);
            var postBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");

            return await this.SetUpHttpClient().PutAsync($"api/parkinglots/{updateParkingLotDto.Name}", postBody);
        }

        private async Task<HttpResponseMessage> PostNewParkingLot(ParkingLotDto parkingLotDto)
        {
            var parkingLotJson = JsonConvert.SerializeObject(parkingLotDto);
            var postBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");

            return await this.SetUpHttpClient().PostAsync("api/parkinglots", postBody);
        }

        private async Task<HttpResponseMessage> PostNewOrder(string name, OrderDto orderDto)
        {
            var parkingLotJson = JsonConvert.SerializeObject(orderDto);
            var postBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");

            return await this.SetUpHttpClient().PostAsync($"api/parkinglots/{name}/orders", postBody);
        }

        private HttpClient SetUpHttpClient()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            return httpClient;
        }
    }
}
