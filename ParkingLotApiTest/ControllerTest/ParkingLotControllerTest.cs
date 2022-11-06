using System.Threading.Tasks;
using ParkingLotApi;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using ParkingLotApi.Dtos;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;

    public class ParkingLotControllerTest:TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_add_parkingLot_success()
        {
            //given
            var client=GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parkingLot one",
                Capcity = 100,
                Location = "wudaokou"
            };
            //when
            await PostParkingLot(parkingLotDto, client);
            //then
            var allParkingLotsResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            Assert.Single(returnParkingLots);
            Assert.Equal(parkingLotDto.Name, returnParkingLots[0].Name);
        }
        [Fact]
        public async Task Should_return_BadRequest_when_create_parkingLot_given_parkingLot_capacity_is_minus()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parkingLot one",
                Capcity = -1,
                Location = "wudaokou"
            };
            //when
            var response = await PostParkingLot(parkingLotDto, client);
            //then
            Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
        }

        [Fact]
        public async Task Should_return_BadRequest_when_create_parkingLot_given_parkingLot_name_exists()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parkingLot one",
                Capcity = 100,
                Location = "wudaokou"
            };
            await PostParkingLot(parkingLotDto, client);
            ParkingLotDto parkingLotDtoTwo = new ParkingLotDto
            {
                Name = "parkingLot one",
                Capcity = 200,
                Location = "qinghe"
            };
            //when
            var response = await PostParkingLot(parkingLotDtoTwo,client);
            //then
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_get_parkingLot_by_id_success()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parkingLot one",
                Capcity = 100,
                Location = "wudaokou"
            };
            await PostParkingLot(parkingLotDto, client);
            ParkingLotDto parkingLotDtoTwo = new ParkingLotDto
            {
                Name = "parkingLot two",
                Capcity = 200,
                Location = "qinghe"
            };
            var response = await PostParkingLot(parkingLotDtoTwo, client);
            //when
            var getParkingLot = await client.GetAsync(response.Headers.Location);
            var result = await Deserialize(response);
            //then
            Assert.Equal("parkingLot two", result.Name);
        }

        [Fact]
        public async Task Should_delete_parkingLot_by_id_success()
        {
            //given
            var client = GetClient();
            ParkingLotDto parkingLotDto = new ParkingLotDto
            {
                Name = "parkingLot one",
                Capcity = 100,
                Location = "wudaokou"
            };
            var response = await PostParkingLot(parkingLotDto, client);
            await client.DeleteAsync(response.Headers.Location);
            var allParkingLotsResponse = await client.GetAsync("/parkingLots");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Empty(returnParkingLots);
        }

        [Fact]
        public async Task Should_return_15_parkingLots_success()
        {
            //given
            var client = GetClient();
            for(int i = 0; i < 15; i++)
            {
                await PostParkingLot(GenerateParkingLot($"p{i + 1}"), client);
            }

            var allParkingLotsResponse = await client.GetAsync("/parkingLots?startPage=1");
            var body = await allParkingLotsResponse.Content.ReadAsStringAsync();
            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);

            Assert.Equal(15,returnParkingLots.Count);

        }

        public async Task<HttpResponseMessage> PostParkingLot(ParkingLotDto parkingLotDto,HttpClient client)
        {
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/parkingLots", content);
            return response;
        } 

        public StringContent GetStringContent(ParkingLotDto parkingLotDto)
        {
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            return new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        public async Task<ParkingLotDto> Deserialize(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ParkingLotDto>(body);
        }

        public ParkingLotDto GenerateParkingLot(string name, int capacity=100,string location ="wudaokou")
        {
            return new ParkingLotDto
            {
                Name = name,
                Capcity = capacity,
                Location = location
            };
        }
    }
}