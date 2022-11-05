using System;

namespace ParkingLotApi.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public string ParkingLotName { get; set; }

        public string PlateNumber { get; set; }

        public string CreationTime { get; set; } = DateTime.Now.ToString();

        public string? CloseTime { get; set; }

        public bool IsClose { get; set; } = true;
    }

    public enum OrderStatus
    {
        Open,
        Closed,
    }
}
