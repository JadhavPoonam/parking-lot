using System;
namespace ParkingLot.Models
{
    public class Spot
    {
        public int id { get; set; }
        public bool isOccupied { get; set; }
        public int size { get; set; }
    }
}
