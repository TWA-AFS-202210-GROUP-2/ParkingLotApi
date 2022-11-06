using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public interface IParkingLotService
    {
        List<ParkingLotDto> GetAll(int page);
        Task<int> Create(ParkingLotDto parkingLotDto);
        Task<int> Delete(int id);
        ParkingLotDto GetById(int id);
        Task<int> UpdateCapacityByIdAsync(int id, int capacity);
        Task<int> CreateOrder(int parkingLotId, OrderDto orderDto);
        Task<int> UpdateOrder( int orderId);
    }
}
