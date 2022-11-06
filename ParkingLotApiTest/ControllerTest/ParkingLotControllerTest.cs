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

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest
    {
        public ParkingLotControllerTest()
        {
        }

        [Fact]
        public async void Should_return_parkinglot_when_post_given_new_parkinglot()
        {
            // given
            var httpClient = SetUpHttpClients();
            var newParkingLotDto = new ParkingLotDto()
            {
                Name = "ParkingLotA",
                Capacity = 100,
                Location = "Zone-A",
            };

            var parkingLotJson = JsonConvert.SerializeObject(newParkingLotDto);
            var postBody = new StringContent(parkingLotJson, Encoding.UTF8, "application/json");

            // when
            var response = await httpClient.PostAsync("api/parkinglots", postBody);

            // then
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var addedParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody);
            Assert.Equal(newParkingLotDto.Capacity, addedParkingLot.Capacity);
        }

        private HttpClient SetUpHttpClients()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            return httpClient;
        }
    }
}
