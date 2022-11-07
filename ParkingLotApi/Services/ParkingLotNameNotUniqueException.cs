using System;

namespace ParkingLotApi.Services
{
    public class ParkingLotNameNotUniqueException : Exception
    {
        public ParkingLotNameNotUniqueException(string message) : base(message)
        {
        }
    }
}