using System.Collections.Generic;

namespace ParkingLotApi.Repository
{
    public class parkingEntity
    {
        public parkingEntity()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int capacity { get; set; }
        public string location { get; set; }
        public List<orderEntity>? order { get; set; }

    }
}