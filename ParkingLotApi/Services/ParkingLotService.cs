using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dto;
using ParkingLotApi.Model;
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

        public List<ParkingLotDto> GetAll(int page)
        {
            int numberOfObjectsPerPage = 15;
            if (page == -1)
            {
                var parkingLots = _parkingLotContext.ParkingLots.Include(_ => _.Orders).ToList();
                return parkingLots.Select(parkingLot => new ParkingLotDto(parkingLot)).ToList();
            }
            else
            {
                var parkingLots = _parkingLotContext.ParkingLots.Include(_ => _.Orders).ToList();
                return parkingLots
                    .Select(parkingLot => new ParkingLotDto(parkingLot))
                    .Skip(numberOfObjectsPerPage * page)
                    .Take(numberOfObjectsPerPage)
                    .ToList();
            }
       }

        public async Task<int> Create(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = parkingLotDto.toEntity();
            await _parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await _parkingLotContext.SaveChangesAsync();
            return parkingLotEntity.Id;
        }

        public async Task<int> Delete(int id)
        {
            var parkingLot = _parkingLotContext.ParkingLots
                .Include(_ => _.Orders)
                .FirstOrDefault(_ => _.Id == id);
            _parkingLotContext.ParkingLots.Remove(parkingLot);
            await _parkingLotContext.SaveChangesAsync();
            return parkingLot.Id;
        }
    }
}
