using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace ParkingLotApi.Services
{
    public class ParkinglotService
    {
        private readonly ParkingLotContext parkinglotDbContext;

        public ParkinglotService(ParkingLotContext parkinglotDbContext)
        {
            this.parkinglotDbContext = parkinglotDbContext;
        }

        public async Task<List<ParkinglotDto>> GetAll()
        {
            var parkings = parkinglotDbContext.Parkinglot.ToList();

            return parkings.Select(ParkinglotEntity => new ParkinglotDto(ParkinglotEntity)).ToList();

        }

        public async Task<int> AddParkinglot(ParkinglotDto parkinglotDto)
        {
            // convert dto to entity
            ParkinglotEntity parkinglotEntity = parkinglotDto.ToEntity();

            // save entity to db
            await parkinglotDbContext.Parkinglot.AddAsync(parkinglotEntity);
            await parkinglotDbContext.SaveChangesAsync();

            // return parking id
            return parkinglotEntity.Id;
        }

        public async Task DeleteParkinglot(int id)
        {
            var foundParking = parkinglotDbContext.Parkinglot
                .FirstOrDefault(_ => _.Id == id);

            parkinglotDbContext.Parkinglot.Remove(foundParking);
            await parkinglotDbContext.SaveChangesAsync();

        }

        public async Task<ParkinglotDto> GetById(int id)
        {
            var foundParking = parkinglotDbContext.Parkinglot
                .FirstOrDefault(_ => _.Id == id);

            return new ParkinglotDto(foundParking);

        }
    }
}