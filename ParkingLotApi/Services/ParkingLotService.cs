﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dto;
using ParkingLotApi.Model;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly ParkingLotContext _parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            _parkingLotContext = parkingLotContext;
        }

        public List<ParkingLotDto> GetAll(int page)
        {
            int numberOfObjectsPerPage = 15;
            if (page == -1)
            {
                var parkingLots = _parkingLotContext.ParkingLots.Include(_ => _.Orders).ToList();
                return parkingLots.Select(parkingLot => new ParkingLotDto(parkingLot)).ToList();
            }
            else
            {
                var parkingLots = _parkingLotContext.ParkingLots.Include(_ => _.Orders).ToList();
                return parkingLots
                    .Select(parkingLot => new ParkingLotDto(parkingLot))
                    .Skip(numberOfObjectsPerPage * page)
                    .Take(numberOfObjectsPerPage)
                    .ToList();
            }
       }

        public async Task<int> Create(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = parkingLotDto.toEntity();
            await _parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await _parkingLotContext.SaveChangesAsync();
            return parkingLotEntity.Id;
        }

        public async Task<int> Delete(int id)
        {
            var parkingLot = _parkingLotContext.ParkingLots
                .Include(_ => _.Orders)
                .FirstOrDefault(_ => _.Id == id);
            _parkingLotContext.ParkingLots.Remove(parkingLot);
            await _parkingLotContext.SaveChangesAsync();
            return parkingLot.Id;
        }

        public ParkingLotDto GetById(int id)
        {
            var parkingLot = _parkingLotContext.ParkingLots
                .Include(_ => _.Orders)
                .FirstOrDefault(_ => _.Id == id);
            return new ParkingLotDto(parkingLot);
        }

        public async Task<int> UpdateCapacityByIdAsync(int id, int capacity)
        {
            var parkingLot = _parkingLotContext.ParkingLots
                .Include(_ => _.Orders)
                .FirstOrDefault(_ => _.Id == id);
            parkingLot.Capacity = capacity;
            _parkingLotContext.ParkingLots.Update(parkingLot);
            await _parkingLotContext.SaveChangesAsync();
            return parkingLot.Id;
        }

        public async Task<int> CreateOrder(int parkingLotId, OrderDto orderDto)
        {
            var parkingLot = _parkingLotContext.ParkingLots
                .Include(_ => _.Orders)
                .FirstOrDefault(_ => _.Id == parkingLotId);
            if (parkingLot.Orders.Where(order => order.IsOpen == true).ToList()
                .Count < parkingLot.Capacity)
            {
                orderDto.ParkingLotName = parkingLot.Name;
                OrderEntity orderEntity = orderDto.ToEntity();
                parkingLot.Orders.Add(orderEntity);
                _parkingLotContext.ParkingLots.Update(parkingLot);
                await _parkingLotContext.Orders.AddAsync(orderEntity);
                await _parkingLotContext.SaveChangesAsync();
                return orderEntity.Id;
            }
            return 0;
        }

        public async Task<int> UpdateOrder( int orderId)
        {
            var order = _parkingLotContext.Orders.FirstOrDefault(_ => _.Id == orderId);
            order.IsOpen = false;
            order.CloseTime = DateTime.Now.ToString();

            _parkingLotContext.Orders.Update(order);
            await _parkingLotContext.SaveChangesAsync();
            return order.Id;
        }
    }
}
