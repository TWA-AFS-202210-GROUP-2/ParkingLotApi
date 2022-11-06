namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    using System.Net.Mime;
    using System.Text;
    using ParkingLotApi.Dtos;
    using Newtonsoft.Json;
    using ParkingLotApiTest;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Collections.Generic;

    public class parkingControllerTest : TestBase
    {
        public parkingControllerTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_parking_success()
        {
            // given
            var client = GetClient();
            parkingDto parkingDto = new parkingDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing",
            };

            // when
            var httpContent = JsonConvert.SerializeObject(parkingDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/Parkings", content);

            // then
            var allParkingsResponse = await client.GetAsync("/Parkings");
            var body = await allParkingsResponse.Content.ReadAsStringAsync();

            var returnParkings = JsonConvert.DeserializeObject<List<parkingDto>>(body);

            Assert.Single(returnParkings);
        }

        [Fact]
        public async Task Should_create_parking_with_profile_and_orders_success()
        {
            // given
            var client = GetClient();
            parkingDto parkingDto = new parkingDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing",
                orderDtos = new List<orderDto>()
                {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
                },
            };

            // when
            var httpContent = JsonConvert.SerializeObject(parkingDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/Parkings", content);

            // then
            var allParkingsResponse = await client.GetAsync("/Parkings");
            var body = await allParkingsResponse.Content.ReadAsStringAsync();

            var returnParkings = JsonConvert.DeserializeObject<List<parkingDto>>(body);

            Assert.Single(returnParkings);
            Assert.Equal(parkingDto.orderDtos.Count, returnParkings[0].orderDtos.Count);
            Assert.Equal(parkingDto.orderDtos[0].CreateTime, returnParkings[0].orderDtos[0].CreateTime);
            Assert.Equal(parkingDto.orderDtos[0].PlateNumber, returnParkings[0].orderDtos[0].PlateNumber);
        }

        [Fact]
        public async Task Should_delete_parking_and_related_order_and_profile_success()
        {
            var client = GetClient();
            parkingDto parkingDto = new parkingDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing",
                orderDtos = new List<orderDto>()
                {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
                },
            };

            var httpContent = JsonConvert.SerializeObject(parkingDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await client.PostAsync("/Parkings", content);
            await client.DeleteAsync(response.Headers.Location);
            var allParkingsResponse = await client.GetAsync("/Parkings");
            var body = await allParkingsResponse.Content.ReadAsStringAsync();

            var returnParkings = JsonConvert.DeserializeObject<List<parkingDto>>(body);

            Assert.Empty(returnParkings);
        }


        [Fact]
        public async Task Should_get_parking_by_id_success()
        {
            var client = GetClient();
            parkingDto parkingDto = new parkingDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing",
                orderDtos = new List<orderDto>()
                {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
                },
            };

            parkingDto parkingDto2 = new parkingDto
            {
                Name = "IB",
                capacity = 10,
                location = "beijing",
                orderDtos = new List<orderDto>()
                {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "9:00",
                        Status = true,
                    },
                },
            };

            var httpContent = JsonConvert.SerializeObject(parkingDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var parkingResponse = await client.PostAsync("/Parkings", content);

            var httpContent2 = JsonConvert.SerializeObject(parkingDto2);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/Parkings", content2);

            var allParkingsResponse = await client.GetAsync(parkingResponse.Headers.Location);
            var body = await allParkingsResponse.Content.ReadAsStringAsync();

            var returnparking = JsonConvert.DeserializeObject<parkingDto>(body);

            Assert.Equal("IBM", returnparking.Name);
        }

        [Fact]
        public async Task Should_change_order_by_id_success()
        {
            var client = GetClient();
            parkingDto parkingDto = new parkingDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing",
                orderDtos = new List<orderDto>()
                {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "10:00",
                        Status = true,
                    },
                },
            };

            parkingDto parkingDto2 = new parkingDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing",
                orderDtos = new List<orderDto>()
                {
                    new orderDto()
                    {
                        PlateNumber = "A12345",
                        CloseTime = "14:00",
                        CreateTime = "9:00",
                        Status = true,
                    },
                },
            };
            var httpContent = JsonConvert.SerializeObject(parkingDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var parkingResponse = await client.PostAsync("/Parkings", content);

            var httpContent2 = JsonConvert.SerializeObject(parkingDto2);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);

            var allParkingsResponse = await client.PutAsync(parkingResponse.Headers.Location, content2);
            var body = await allParkingsResponse.Content.ReadAsStringAsync();

            var returnparking = JsonConvert.DeserializeObject<parkingDto>(body);

            Assert.Equal("9:00", returnparking.orderDtos[0].CreateTime);
        }

        [Fact]
        public async Task Should_change_parking_by_id_success()
        {
            var client = GetClient();
            parkingDto parkingDto = new parkingDto
            {
                Name = "IBM",
                capacity = 100,
                location = "beijing",
            };

            parkingDto parkingDto2 = new parkingDto
            {
                Name = "IBM",
                capacity = 10,
                location = "beijing",
            };
            var httpContent = JsonConvert.SerializeObject(parkingDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var parkingResponse = await client.PostAsync("/Parkings", content);

            var httpContent2 = JsonConvert.SerializeObject(parkingDto2);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);

            var allParkingsResponse = await client.PutAsync(parkingResponse.Headers.Location, content2);
            var body = await allParkingsResponse.Content.ReadAsStringAsync();

            var returnparking = JsonConvert.DeserializeObject<parkingDto>(body);

            Assert.Equal(10, returnparking.capacity);
        }
    }
}