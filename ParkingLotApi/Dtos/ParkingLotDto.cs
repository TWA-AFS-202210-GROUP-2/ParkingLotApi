using ParkingLotApi.Model;
using System;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {

        }
        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            Name = parkingLotEntity.Name;
            Capcity=parkingLotEntity.Capcity;
            Location = parkingLotEntity.Location;
        }
        public string Name { get; set; }
        public int Capcity { get; set; }
        public string Location { get; set; }

        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity()
            {
                Name = Name,
                Capcity = Capcity,
                Location = Location,
            };
        }
    }
}
