using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingLotApi.Model;
using System;

namespace ParkingLotApi.Dtos
{
    public class OrderDto
    {
        /*private string orderStatus = "open";*/
        public OrderDto()
        {

        }
        public OrderDto(OrderEntity orderEntity)
        {
            OrderNumber = orderEntity.OrderNumber;
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
            CreationTime = orderEntity.CreationTime;
            ClosedTime = orderEntity.ClosedTime;
            OrderStatus = orderEntity.OrderStatus;
        }
        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public string CreationTime { get; set; }
        public string ClosedTime { get; set; }
        public string OrderStatus { get; set; }

        public OrderEntity ToEntity()
        {
            return new OrderEntity()
            {
                OrderNumber = OrderNumber,
                ParkingLotName = ParkingLotName,
                PlateNumber = PlateNumber,
                CreationTime = CreationTime,
                ClosedTime = ClosedTime,
                OrderStatus = OrderStatus,
            };
        }
    }
}
