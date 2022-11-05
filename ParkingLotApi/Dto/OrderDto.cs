using ParkingLotApi.Model;
using System;

namespace ParkingLotApi.Dto
{
    public class OrderDto
    {
        public OrderDto(OrderEntity orderEntity)
        {
            if (orderEntity != null)
            {
                ParkingLotName = orderEntity.ParkingLotName;
                PlateNumber = orderEntity.PlateNumber;
                CreationTime = orderEntity.CreationTime;
                CloseTime = orderEntity?.CloseTime;
                IsClose = orderEntity.IsClose;
            }
        }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public string CreationTime { get; set; } = DateTime.Now.ToString();

        public string? CloseTime { get; set; }

        public bool IsClose { get; set; } = true;

        public OrderEntity ToEntity()
        {
            return new OrderEntity()
            {
                ParkingLotName = ParkingLotName,
                PlateNumber = PlateNumber,
                CreationTime = CreationTime,
                CloseTime = this?.CloseTime,
                IsClose = IsClose,
            };
        }
    }
}
