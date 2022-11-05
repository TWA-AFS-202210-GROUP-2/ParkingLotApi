using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dto;
using ParkingLotApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly ParkingLotContext _parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            _parkingLotContext = parkingLotContext;
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {
            var parkingLots = _parkingLotContext.ParkingLots
                .Include(parkingLot => parkingLot.Orders)
                .ToList();
            return parkingLots.Select(parkingLot => new ParkingLotDto(parkingLot)).ToList();
        }
    }
}
