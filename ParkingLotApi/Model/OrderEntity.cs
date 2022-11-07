using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ParkingLotApi.Model
{
    public class OrderEntity
    {
        public OrderEntity()
        {

        }
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public string CreationTime { get; set; }
        public string ClosedTime { get; set; }
        public string OrderStatus { get; set; }
    }
}
