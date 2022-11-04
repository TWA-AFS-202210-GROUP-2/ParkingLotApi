using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkinglots")]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkinglotService parkinglotService;

        public ParkingLotController(ParkinglotService parkinglotService)
        {
            this.parkinglotService = parkinglotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkinglotDto>>> List()
        {
            var parkinglotDtos = await this.parkinglotService.GetAll();

            return Ok(parkinglotDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkinglotDto>> GetById(int id)
        {
            var parkingDto = await this.parkinglotService.GetById(id);
            return Ok(parkingDto);
        }

        [HttpPost]
        public async Task<ActionResult<ParkinglotDto>> Add(ParkinglotDto parkingDto)
        {
            var id = await this.parkinglotService.AddParkinglot(parkingDto);

            return CreatedAtAction(nameof(GetById), new { id = id }, parkingDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await parkinglotService.DeleteParkinglot(id);

            return this.NoContent();
        }
    }
}
