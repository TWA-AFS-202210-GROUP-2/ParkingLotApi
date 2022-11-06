namespace ParkingLotApiTest.ControllerTest
{
    using System.Net.Mime;
    using System.Text;
    using ParkingLotApi.Dtos;
    using Newtonsoft.Json;
    using ParkingLotApiTest;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Collections.Generic;

    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parkinglot_success()
        {
            // given
            var client = GetClient();
            ParkinglotDto parkinglLotDto = new ParkinglotDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing"
            };

            // when
            var httpContent = JsonConvert.SerializeObject(parkinglLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkinglots", content);

            // then
            var allParkinglotResponse = await client.GetAsync("/parkinglots");
            var body = await allParkinglotResponse.Content.ReadAsStringAsync();

            var returnParkinglot = JsonConvert.DeserializeObject<List<ParkinglotDto>>(body);

            Assert.Single(returnParkinglot);
        }

        [Fact]
        public async Task Should_delete_parkinglot_success()
        {
            var client = GetClient();
            ParkinglotDto parkinglLotDto = new ParkinglotDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing"
            };

            var httpContent = JsonConvert.SerializeObject(parkinglLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/parkinglots", content);
            await client.DeleteAsync(response.Headers.Location);
            var allResponse = await client.GetAsync("/parkinglots");
            var body = await allResponse.Content.ReadAsStringAsync();

            var returnCompanies = JsonConvert.DeserializeObject<List<ParkinglotDto>>(body);

            Assert.Empty(returnCompanies);
        }
    }
}
