using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ParkingLotApi.Dtos;
using ParkingLotApi.Models;
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

    public async Task<List<ParkingLotDto>> GetbyPage(int pageNumber)
    {
        int start = pageNumber * 15;
        int end = 15 * (pageNumber + 1);
        var allList = context.ParkingLotEntities.ToList();
        var pageList = new List<ParkingLotDto>();
        allList.ForEach(item =>
        {
            if (item.Id > start && item.Id <= end)
            {
                pageList.Add(new ParkingLotDto(item));
            }
        });
        return pageList;
    }

    public async Task<ParkingLotDto> GetbyId(int id)
    {
        var entity = context.ParkingLotEntities.FirstOrDefault(e => e.Id.Equals(id));
        return new ParkingLotDto(entity);
    }

    public async Task<ParkingLotDto> UpdateParkingLot(int id, ParkingLotDto parkingLotDto)
    {
        var entity = context.ParkingLotEntities.FirstOrDefault(e => e.Id.Equals(id));
        if (entity != null)
        {
            entity.Capacity = parkingLotDto.Capacity;
        }

        return new ParkingLotDto(entity);
    }
}

