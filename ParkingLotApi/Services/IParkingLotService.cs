using ParkingLotApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public interface IParkingLotService
    {
        Task<List<ParkingLotDto>> GetAll();
        Task<int> Create(ParkingLotDto parkingLotDto);
    }
}
