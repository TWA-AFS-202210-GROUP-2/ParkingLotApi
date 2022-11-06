using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Service;

public class OrderService
{
    private readonly ParkingLotContext context;

    public OrderService(ParkingLotContext context)
    {
        this.context = context;
    }
    public async Task<OrderDto> CreateOrder(int parkingLotId,OrderDto orderDto)
    {
        var parkingLotEntity = context.ParkingLotEntities.Include(e=>e.Orders).FirstOrDefault(i=> i.Id.Equals(parkingLotId));
        parkingLotEntity.Orders.Add(orderDto.ToEntity(parkingLotEntity.Name));
        context.SaveChanges();
        return orderDto;
    }
}