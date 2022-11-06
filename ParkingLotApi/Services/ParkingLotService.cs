using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<int> AddNewParkingLot(ParkingLotDto parkingLotDto)
        {
            if (UniqueNameDetect(parkingLotDto.Name))
            {
                throw new ParkingLotNameNotUniqueException("Name not unique!");
            }

            if (CapacityBelowZero(parkingLotDto.Capacity))
            {
                throw new ParkingLotCapacityBelowZeroException("Capacity below zero!");
            }

            var parkingLotEntity = parkingLotDto.ToEntity();
            await parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await parkingLotContext.SaveChangesAsync();

            return parkingLotEntity.Id;
        }

        public async Task DeleteAll()
        {
            parkingLotContext.ParkingLots
                .RemoveRange(parkingLotContext.ParkingLots);

            await parkingLotContext.SaveChangesAsync();
        }

        public async Task DeleteParkingLot(string parkingLotName)
        {
            var soldParkingLot = parkingLotContext.ParkingLots
                .FirstOrDefault(parkingLot => parkingLot.Name == parkingLotName);

            parkingLotContext.Remove(soldParkingLot);
            await parkingLotContext.SaveChangesAsync();
        }

        public async Task<ParkingLotDto> GetParkingLot(string parkingLotName)
        {
            var foundParkingLot = parkingLotContext.ParkingLots
               .FirstOrDefault(parkingLot => parkingLot.Name == parkingLotName);

            return new ParkingLotDto(foundParkingLot);
        }

        private bool UniqueNameDetect(string name)
        {
            return parkingLotContext.ParkingLots
                .FirstOrDefault(parkingLot => parkingLot.Name == name)
                != null ? true : false;
        }

        private bool CapacityBelowZero(int capacity)
        {
            return capacity < 0;
        }
    }
}