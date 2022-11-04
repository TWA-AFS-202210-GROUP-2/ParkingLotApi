namespace ParkingLotApi.Repository
{
    public class ParkinglotEntity
    {
        public ParkinglotEntity()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int capacity { get; set; }
        public string location { get; set; }

    }
}
