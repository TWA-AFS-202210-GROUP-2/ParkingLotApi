using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dto;
using ParkingLotApi.Services;
using System.Threading.Tasks;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkinglots")]
    public class ParkingLotController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotController(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetParkingLots()
        {
            var parkingLots = await _parkingLotService.GetAll();
            return Ok(parkingLots);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParkingLot([FromBody]ParkingLotDto parkingLotDto)
        {
            var id = await _parkingLotService.Create(parkingLotDto);
            return Created(string.Empty, id);
        }
    }
}
