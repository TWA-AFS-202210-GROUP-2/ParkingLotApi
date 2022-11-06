﻿using ParkingLotApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public interface IParkingLotService
    {
        List<ParkingLotDto> GetAll(int page);
        Task<int> Create(ParkingLotDto parkingLotDto);
    }
}
