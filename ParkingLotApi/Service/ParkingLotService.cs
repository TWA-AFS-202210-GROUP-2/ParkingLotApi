using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Service;

public class ParkingLotService
{
    private readonly ParkingLotContext context;

    public ParkingLotService(ParkingLotContext context)
    {
        this.context = context;
    }

    public async Task<int> AddParkingLot(ParkingLotDto parkingLotDto)
    {
        var parkingLotEntity = parkingLotDto.ToEntity();
        context.ParkingLotEntities.Add(parkingLotEntity);
        context.SaveChanges();
        return parkingLotEntity.Id;
    }

    public async Task DeleteParkingLot(int id)
    {
        var parkingLot = await context.ParkingLotEntities.FirstOrDefaultAsync(item => item.Id.Equals(id));
        context.ParkingLotEntities.Remove(parkingLot);
        context.SaveChanges();
    }
}

