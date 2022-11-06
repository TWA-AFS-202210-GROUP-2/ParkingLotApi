using ParkingLotApi.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApi.Dtos
{
    public class parkingDto
    {
        public parkingDto()
        {
        }

        public parkingDto(parkingEntity parkingEntity)
        {
            Name = parkingEntity.Name;
            capacity = parkingEntity.capacity;
            location = parkingEntity.location;
            orderDtos = parkingEntity.order?.Select(_ => new orderDto(_)).ToList();
        }

        public string Name { get; set; }
        public int capacity { get; set; }
        public string location { get; set; }


        public List<orderDto>? orderDtos { get; set; }

        public parkingEntity ToEntity()
        {
            return new parkingEntity()
            {
                Name = this.Name,
                capacity = this.capacity,
                location = this.location,
                order = this.orderDtos?.Select(_ => _.ToEntity()).ToList()
            };
        }

    }
}