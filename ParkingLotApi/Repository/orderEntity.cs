namespace ParkingLotApi.Repository
{
    public class orderEntity
    {
        public orderEntity()
        {
        }

        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string CreateTime { get; set; }
        public string CloseTime { get; set; }
        public bool Status { get; set; }
    }
}