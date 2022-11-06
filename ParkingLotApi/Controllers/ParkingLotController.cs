using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("Parkings")]
    public class parkingController : ControllerBase
    {
        private readonly parkingService parkingService;

        public parkingController(parkingService parkingService)
        {
            this.parkingService = parkingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<parkingDto>>> List()
        {
            var parkingDtos = await this.parkingService.GetAll();

            return Ok(parkingDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<parkingDto>> GetById(int id)
        {
            var parkingDto = await this.parkingService.GetById(id);
            return Ok(parkingDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<parkingDto>> PutById(int id, parkingEntity parking)
        {
            var parkingDto = await this.parkingService.PutById(id, parking);
            return Ok(parkingDto);
        }

        [HttpPost]
        public async Task<ActionResult<parkingDto>> Add(parkingDto parkingDto)
        {
            var id = await this.parkingService.Addparking(parkingDto);

            return CreatedAtAction(nameof(GetById), new { id = id }, parkingDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await parkingService.Deleteparking(id);

            return this.NoContent();
        }
    }
}