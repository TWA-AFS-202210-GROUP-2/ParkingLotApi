using ParkingLotApi.Repository;
using System.Collections.Generic;

namespace ParkingLotApi.Dtos
{
    public class ParkinglotDto
    {
        public ParkinglotDto()
        {
        }

        public ParkinglotDto(ParkinglotEntity parkinglotEntity)
        {
            Name = parkinglotEntity.Name;
            capacity = parkinglotEntity.capacity;
            location = parkinglotEntity.location;
        }

        public string Name { get; set; }
        public int capacity { get; set; }
        public string location { get; set; }
        

        public ParkinglotEntity ToEntity()
        {
            return new ParkinglotEntity()
            {
                Name = this.Name,
                capacity = this.capacity,
                location = this.location,
            };
        }

    }
}