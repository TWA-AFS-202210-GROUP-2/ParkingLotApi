using ParkingLotApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if(parkingLotEntity.Orders == null)
            {
                OrderDtos = new List<OrderDto>();
            }
            else
            {
                OrderDtos = parkingLotEntity.Orders?.Select(orderEntity => new OrderDto(orderEntity)).ToList();
            }
        }
        public string Name { get; set; }
        public int Capcity { get; set; }
        public string Location { get; set; }
        public List<OrderDto>? OrderDtos { get; set; }

        public ParkingLotEntity ToEntity()
        {
            return new ParkingLotEntity()
            {
                Name = Name,
                Capcity = Capcity,
                Location = Location,
                Orders = this.OrderDtos?.Select(orderDto => orderDto.ToEntity()).ToList(),
            };
        }
    }
}
