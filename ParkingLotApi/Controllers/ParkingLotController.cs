using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;
using System.Threading.Tasks;

namespace ParkingLotApi.Controllers
{
    [Route("api/parkinglots")]
    [ApiController]
    public class ParkingLotController : ControllerBase
    {
        private ParkingLotService _parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this._parkingLotService = parkingLotService;
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto parkingLotDto)
        {
            await this._parkingLotService.AddNewParkingLot(parkingLotDto);
            return Created($"/api/parkinglots", parkingLotDto);
        }
    }
}
