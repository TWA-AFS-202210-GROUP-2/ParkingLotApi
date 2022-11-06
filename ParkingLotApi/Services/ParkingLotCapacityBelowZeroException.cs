using System;

namespace ParkingLotApi.Services
{
    public class ParkingLotCapacityBelowZeroException : Exception
    {
        public ParkingLotCapacityBelowZeroException(string message) : base(message)
        {
        }
    }
}