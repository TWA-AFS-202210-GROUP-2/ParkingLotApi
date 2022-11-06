using System;
using Microsoft.AspNetCore.Mvc;

namespace ParkingLotApi.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
            IsOpen = true;
        }

      public string OrderNumber { get; set; }
      public string ParkingLotName { get; set; }

      public string PlateNumber { get; set; }
      public string CreateTime { get; set; }
      public string CloseTime { get; set; }
      public Boolean IsOpen { get; set; }
      public OrderEntity ToEntity()
      {
          var entity = new OrderEntity
          {
              OrderNumber = this.OrderNumber,
              ParkingLotName = this.ParkingLotName,
              PlateNumber = this.PlateNumber,
              CreateTime = this.CreateTime,
              CloseTime = this.CloseTime,
              IsOpen = this.IsOpen
          };
          return entity;
      }
    }
  
}
