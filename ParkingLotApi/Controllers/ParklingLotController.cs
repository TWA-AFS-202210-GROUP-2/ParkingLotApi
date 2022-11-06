using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Service;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("api/parkinglots")]
    public class ParklingLotController : Controller
    {
     
        private readonly ParkingLotService parkingLotService;

        public ParklingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }
        [HttpPost]
        public async Task<IActionResult> PostOneParkingLot(ParkingLotDto parkingLotDto)
        {
            var id = parkingLotService.AddParkingLot(parkingLotDto);

            return Created("parkinglots",parkingLotDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await parkingLotService.DeleteParkingLot(id);
            return NoContent();
        }

    }
}
