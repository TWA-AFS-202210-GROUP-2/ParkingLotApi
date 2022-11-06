using Microsoft.Net.Http.Headers;
using ParkingLotApi.Models;

namespace ParkingLotApi.Dtos;

public class ParkingLotDto
{
    public string Name { get; }
    public int Capacity { get; set; }
    public string Location { get; }

    public ParkingLotDto(string name, int capacity, string location)
    {
        Name = name;
        Capacity = capacity;
        Location = location;
    }

    public ParkingLotDto()
    {

    }
    public ParkingLotDto(ParkingLotEntity parkingLotEntity)
    {
        Name = parkingLotEntity.Name;
        Capacity = parkingLotEntity.Capacity;
        Location = parkingLotEntity.Location;

    }
    public ParkingLotEntity ToEntity()
    {
        return new ParkingLotEntity()
        {
            Name = this.Name,
            Capacity = this.Capacity,
            Location = this.Location,

        };
    }
}