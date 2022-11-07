﻿using System.Collections.Generic;

namespace ParkingLotApi.Model
{
    public class ParkingLotEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public List<OrderEntity>? Orders { get; set; }
    }
}
