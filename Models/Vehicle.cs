using System;
namespace ParkingLot.Models
{
    public class Vehicle
    {
        public int id { get; set; }
        public int type { get; set; }
        public string licensePlate { get; set; }

        public int? spotId { get; set; }
    }
}
