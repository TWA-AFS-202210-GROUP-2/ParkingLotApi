using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Models;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<int> AddNewParkingLot(ParkingLotDto parkingLotDto)
        {
            if (UniqueNameDetect(parkingLotDto.Name))
            {
                throw new ParkingLotNameNotUniqueException("Name not unique!");
            }

            if (CapacityBelowZero(parkingLotDto.Capacity))
            {
                throw new Exception("Capacity below zero!");
            }

            var parkingLotEntity = parkingLotDto.ToEntity();
            await parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await parkingLotContext.SaveChangesAsync();

            return parkingLotEntity.Id;
        }

        public async Task DeleteAll()
        {
            parkingLotContext.ParkingLots
                .RemoveRange(parkingLotContext.ParkingLots);

            await parkingLotContext.SaveChangesAsync();
        }

        public async Task DeleteParkingLot(string parkingLotName)
        {
            var soldParkingLot = parkingLotContext.ParkingLots
                .FirstOrDefault(parkingLot => parkingLot.Name == parkingLotName);

            parkingLotContext.Remove(soldParkingLot);
            await parkingLotContext.SaveChangesAsync();
        }

        public async Task<ParkingLotDto> GetParkingLot(string parkingLotName)
        {
            var foundParkingLot = parkingLotContext.ParkingLots
               .FirstOrDefault(parkingLot => parkingLot.Name == parkingLotName);

            return new ParkingLotDto(foundParkingLot);
        }

        private bool UniqueNameDetect(string name)
        {
            return parkingLotContext.ParkingLots
                .FirstOrDefault(parkingLot => parkingLot.Name == name)
                != null ? true : false;
        }

        private bool CapacityBelowZero(int capacity)
        {
            return capacity < 0;
        }

        public List<ParkingLotDto> GetParkingLotsByIndex(int? pageIndex)
        {
            var parkingLotsEntity =
                parkingLotContext.ParkingLots.Skip((pageIndex.Value - 1) * 15)
                .Take(15).ToList();

            return parkingLotsEntity
                .Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task<ParkingLotDto> UpdateParkingLot(ParkingLotDto parkingLotDto)
        {
            var foundParkingLotEntity = parkingLotContext.ParkingLots
                                        .FirstOrDefault(parkingLot => parkingLot.Name == parkingLotDto.Name);
            foundParkingLotEntity.Capacity = parkingLotDto.Capacity;
            await parkingLotContext.SaveChangesAsync();

            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<int> CreateOrder(string parkingLotName, OrderDto orderDto)
        {
            var parkingLot = parkingLotContext.ParkingLots
                .Include(_ => _.Orders)
                .FirstOrDefault(_ => _.Name.Equals(parkingLotName));
            if (parkingLot.Orders.Where(order => order.IsOpen == true).ToList()
                .Count < parkingLot.Capacity)
            {
                orderDto.ParkingLotName = parkingLot.Name;
                OrderEntity orderEntity = orderDto.ToEntity();
                parkingLot.Orders.Add(orderEntity);
                parkingLotContext.ParkingLots.Update(parkingLot);
                await parkingLotContext.Orders.AddAsync(orderEntity);
                await parkingLotContext.SaveChangesAsync();
                return orderEntity.Id;
            }

            return 0;
        }

        public async Task<OrderDto> ChangeOrder(string parkingLotName, int orderId)
        {
            var parkingLot = parkingLotContext.ParkingLots
                .Include(_ => _.Orders)
                .FirstOrDefault(_ => _.Name.Equals(parkingLotName));

            var order = parkingLotContext.Orders.FirstOrDefault(_ => _.Id == orderId);
            order.IsOpen = false;
            order.CloseTime = DateTime.Now.ToString();

            parkingLotContext.Orders.Update(order);
            await parkingLotContext.SaveChangesAsync();

            return new OrderDto(order);
        }
    }
}