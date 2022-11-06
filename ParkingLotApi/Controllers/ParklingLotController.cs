using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("api/parkinglots")]
    public class ParklingLotController : Controller
    {
        private readonly ParkingLotContext dbcontext;

        public ParklingLotController(ParkingLotContext dbContext)
        {
            this.dbcontext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> PostOneParkingLot(ParkingLotDto parkingLotDto)
        {
            var parkingLotEntityy = parkingLotDto.ToEntity();
            dbcontext.ParkingLotEntities.Add(parkingLotEntityy);
            dbcontext.SaveChanges();
            return Created("parkinglots",parkingLotDto);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            dbcontext.ParkingLotEntities.RemoveRange(dbcontext.ParkingLotEntities);
            dbcontext.SaveChanges();
            return NoContent();
        }
    }
}
