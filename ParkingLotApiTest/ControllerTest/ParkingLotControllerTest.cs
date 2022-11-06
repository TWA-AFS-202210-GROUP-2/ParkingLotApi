using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace ParkingLotApiTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
   
    using System.Net.Http;
    using System.Net;
    using System.Text;

    public class ParkingLotControllerTest:TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_add_new_parkingLot_successfully()
        {
            /*
             * 1. create Application
             * 2. Create httpClient
             * 3. Prepare request body
             * 4. call api
             * 5 .verify status code
             * 6. verify response body
             */

            //given
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();
            var parkinglot = new ParkingLotDto(name: "SLB", capacity: 100, location: "tuspark");
            
            var companyString = JsonConvert.SerializeObject(parkinglot);
            var stringContent = new StringContent(companyString, Encoding.UTF8, "application/json");
            //await httpClient.DeleteAsync("/api/parkinglots");
            //when
            var res = await httpClient.PostAsync("/api/parkinglots", stringContent);
            //then
            Assert.Equal(HttpStatusCode.Created, res.StatusCode);
            var readAsStringAsync = await res.Content.ReadAsStringAsync();
            var deserializeObject = JsonConvert.DeserializeObject<ParkingLotDto>(readAsStringAsync);
            Assert.Equal(parkinglot.Name, deserializeObject.Name);
            
        }
    }
}