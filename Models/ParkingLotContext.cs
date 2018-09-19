using System;
using Microsoft.EntityFrameworkCore;

namespace ParkingLot.Models
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext(DbContextOptions<ParkingLotContext> options) : base(options)
        {

        }
        public DbSet<Lot> lot { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Spot> spots { get; set; }

    }
}
