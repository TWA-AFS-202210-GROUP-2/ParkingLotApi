using ParkingLotApi.Dtos;

namespace ParkingLotApi.Models;

public class ParkingLotEntity
{
    public ParkingLotEntity()
    {
    }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }
    public int Id { get; set; }
}