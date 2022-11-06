using Newtonsoft.Json;
using ParkingLotApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApiTest
{
    using EFCoreRelationshipsPracticeTest;

    public class ControllerTest : TestBase 
    {

        [Fact]
        public async void Should_get_all_parkinglot()
        {
            var client = GetClient();
            NewParkingLotData();
            var response = await client.GetAsync("parkinglots");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotEntity>>(body);
            _parkingLotContext.ParkingLots.Count();
            Assert.Equal(2, parkinglots.Count);
        }

        [Fact]
        public async void Should_set_a_parkinglot()
        {
            var client = GetClient();
            NewParkingLotData();
            var pl = _parkingLotContext.ParkingLots.FirstOrDefault();
            var postBody = new StringContent(JsonConvert.SerializeObject(pl), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("parkinglots", postBody);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var parkinglots = JsonConvert.DeserializeObject<List<ParkingLotEntity>>(body);
            Assert.Equal(2, parkinglots.Count);
        }

        public ControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }
    }
}
