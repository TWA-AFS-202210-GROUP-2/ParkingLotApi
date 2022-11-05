using ParkingLotApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApi.Dto
{
    public class ParkingLotDto
    {
        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            if (parkingLotEntity != null)
            {
                Name = parkingLotEntity.Name;
                Capacity = parkingLotEntity.Capacity;
                Location = parkingLotEntity.Location;
                Orders = parkingLotEntity.Orders.Select(o => new OrderDto(o)).ToList();
            }
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public List<OrderDto>? Orders { get; set; }

        public ParkingLotEntity toEntity()
        {
            return new ParkingLotEntity()
            {
                Name = Name,
                Capacity = Capacity,
                Location = Location,
                Orders = Orders?.Select(orderDto => orderDto.ToEntity()).ToList(),
            };
        }
    }
}
