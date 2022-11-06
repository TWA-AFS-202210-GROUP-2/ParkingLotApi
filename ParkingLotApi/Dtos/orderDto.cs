using ParkingLotApi.Repository;

namespace ParkingLotApi.Dtos
{
    public class orderDto
    {
        public orderDto()
        {
        }
        public orderDto(orderEntity orderEntity)
        {
            PlateNumber = orderEntity.PlateNumber;
            CreateTime = orderEntity.CreateTime;
            CloseTime = orderEntity.CloseTime;
            Status = orderEntity.Status;

        }

        public string CloseTime { get; set; }
        public string PlateNumber { get; set; }
        public string CreateTime { get; set; }
        public bool Status { get; set; }

        public orderEntity ToEntity()
        {
            return new orderEntity()
            {
                PlateNumber = PlateNumber,
                CreateTime = CreateTime,
                Status = Status,
                CloseTime = CloseTime,

            };
        }
    }
}