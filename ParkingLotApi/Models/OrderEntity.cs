using System;

namespace ParkingLotApi.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? CloseTime { get; set; }

        public OrderStatus IsClose { get; set; } = OrderStatus.Open;
    }

    public enum OrderStatus
    {
        Open,
        Closed,
    }
}
