using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Models;

namespace ParkingLotApi.Repository
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext(DbContextOptions<ParkingLotContext> options)
            : base(options)
        {
        }
        public DbSet<ParkingLotEntity> ParkingLotEntities { get; set; }
        public DbSet<OrderEntity> OderEntities { get; set; }
    }

 
}