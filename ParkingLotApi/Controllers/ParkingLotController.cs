using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        [HttpGet("{parkingLotName}")]
        public async Task<ActionResult<ParkingLotDto>> Get(string parkingLotName)
        {
            var parkingLot = await this._parkingLotService.GetParkingLot(parkingLotName);
            return Ok(parkingLot);
        }

        [HttpGet]
        public ActionResult<List<ParkingLotDto>> GetParkingLots([FromQuery] int? pageIndex)
        {
            try
            {
                if (pageIndex != null && pageIndex >= 0)
                {
                    return _parkingLotService.GetParkingLotsByIndex(pageIndex);
                }

                return _parkingLotService.GetParkingLotsByIndex(pageIndex);
            }
            catch (Exception e)
            {
                throw new Exception("Invalid PageIndex!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto parkingLotDto)
        {
            try
            {
                await this._parkingLotService.AddNewParkingLot(parkingLotDto);
                return Created($"/api/parkinglots", parkingLotDto);
            }
            catch (ParkingLotNameNotUniqueException e)
            {
                throw new ParkingLotNameNotUniqueException("Name not unique!");
            }
        }

        [HttpDelete("{parkingLotName}")]
        public async Task<ActionResult> DeleteByName(string parkingLotName)
        {
            await _parkingLotService.DeleteParkingLot(parkingLotName);

            return this.NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            await _parkingLotService.DeleteAll();
            return this.NoContent();
        }
    }
}
