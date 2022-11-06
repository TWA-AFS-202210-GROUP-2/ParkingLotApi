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
        public IActionResult GetParkingLots([FromQuery]int? page)
        {
            if (page == null)
            {
                var parkingLots = _parkingLotService.GetAll(-1);
                return Ok(parkingLots);
            }
            else
            {
                var parkingLots = _parkingLotService.GetAll((int)page - 1);
                return Ok(parkingLots);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateParkingLot([FromBody]ParkingLotDto parkingLotDto)
        {
            var id = await _parkingLotService.Create(parkingLotDto);
            return Created(string.Empty, id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingLot([FromRoute] int id)
        {
            var idReturn = await _parkingLotService.Delete(id);
            return Ok(idReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParkingLotById([FromRoute] int id)
        {
            var parkinglot = _parkingLotService.GetById(id);
            return Ok(parkinglot);
        }
    }
}
