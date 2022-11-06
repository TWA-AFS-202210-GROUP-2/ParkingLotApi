using Microsoft.EntityFrameworkCore;

namespace ParkingLotApi.Repository
{
    public class parkingDbContext : DbContext
    {
        public parkingDbContext(DbContextOptions<parkingDbContext> options)
            : base(options)
        {
        }

        public DbSet<parkingEntity> Parkings
        {
            get;
            set;
        }

        public DbSet<orderEntity> orders
        {
            get;
            set;
        }
    }
}