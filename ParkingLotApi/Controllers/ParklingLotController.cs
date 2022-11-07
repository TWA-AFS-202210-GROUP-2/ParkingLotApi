using System.Collections.Generic;
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
            var id = await parkingLotService.AddParkingLot(parkingLotDto);

            return Created("parkinglots",parkingLotDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await parkingLotService.DeleteParkingLot(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<List<ParkingLotDto>> GetByPage([FromQuery] int pageNumber)
        {
            var list = await parkingLotService.GetbyPage(pageNumber);
            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingLotDto>> GetById(int id)
        {
            var parkingLotDto = await parkingLotService.GetbyId(id);
            return parkingLotDto;
        }
        [HttpPut("{id}")]
        public async Task<ParkingLotDto> Update(int id, ParkingLotDto parkingLotDto)
        {
            var parkingLot = await parkingLotService.UpdateParkingLot(id,parkingLotDto);
            return parkingLot;
        }

    }
}
