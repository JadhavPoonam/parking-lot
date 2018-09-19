using System;
using System.Collections.Generic;

namespace ParkingLot.Models
{
    public class Lot
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int availableSpots { get; set; }
    }
}
