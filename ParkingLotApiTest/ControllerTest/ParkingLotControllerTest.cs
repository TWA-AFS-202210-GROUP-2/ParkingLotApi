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

    /*    [Fact]
        public async void Should_throw_exception_when_add_same_parkinglot_name()
        {
            // given
            var httpClient = this.SetUpHttpClient();

            await httpClient.DeleteAsync("api/parkinglots");

            var parkingLotDtoOne = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };
            var parkingLotDtoTwo = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
                Status = false,
            };

            // when
            await this.PostNewParkingLot(parkingLotDtoOne);
            await this.PostNewParkingLot(parkingLotDtoOne);

            // then
            Assert.Throws<ParkingLotNameNotUniqueException>();
        }*/

        [Fact]
        public async void Should_delete_parkinglot_when_sold_to_other_successfully()
        {
            // given
            var httpClient = SetUpHttpClient();
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

        private async Task<HttpResponseMessage> PostNewParkingLot(ParkingLotDto parkingLotDto)
        {
            var parkingLotJson = JsonConvert.SerializeObject(parkingLotDto);
            var postBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");

            return await this.SetUpHttpClient().PostAsync("api/parkinglots", postBody);
        }

        private HttpClient SetUpHttpClient()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            return httpClient;
        }
    }
}
