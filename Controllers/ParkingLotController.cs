using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLot.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingLot.Controllers
{
    public class ParkingLotController : Controller
    {
        public readonly ParkingLotContext _context;

        public ParkingLotController(ParkingLotContext context)
        {
            _context = context;
            if (_context.lot == null || _context.lot.Count() == 0)
            {
                List<Spot> spots = new List<Spot>();
                for (int i = 1; i <= 3; i++)
                {
                    spots.Add(new Spot { isOccupied = false, size = i });
                }
                _context.spots.AddRange(spots);


                _context.lot.Add(new Lot { availableSpots = spots.Count, name="Poonam's Lot", address="Downtown" });
                _context.SaveChanges();
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.lot.FirstOrDefault());
        }

        public IActionResult AddVehicle()
        {
            if (_context.lot.FirstOrDefault().availableSpots == 0)
            {
                ViewData.Add("Message", "Parking lot is full!");
            }
            return View(new Vehicle());
        }

        [HttpPost]
        public IActionResult AddVehicle(Vehicle newVehicle)
        {

                var availableSpots = _context.spots.Where(s => !s.isOccupied).Select(s => s).ToList().OrderBy(s => s.size);
                if (availableSpots != null && availableSpots.Count() > 0)
                {

                    Spot selected;
                    if (availableSpots.FirstOrDefault().size == newVehicle.type)
                    {
                        selected = availableSpots.FirstOrDefault();
                    }
                    else if (availableSpots.LastOrDefault().size == newVehicle.type)
                    {
                        selected = availableSpots.LastOrDefault();
                    }
                    else
                    {
                        selected = availableSpots.Where(s => s.size == newVehicle.type || s.size > newVehicle.type).Select(s => s).FirstOrDefault();
                    }
                    selected.isOccupied = true;
                    _context.spots.Update(selected);
                    newVehicle.spotId = selected.id;
                    _context.Vehicles.Add(newVehicle);

                    var lot = _context.lot.FirstOrDefault();

                    lot.availableSpots -= 1;
                    _context.lot.Update(lot);
                    _context.SaveChanges();

            }
            return View(newVehicle);


        }

        public IActionResult RemoveVehicle()
        {
            if (_context.lot.FirstOrDefault().availableSpots == 3)
            {
                ViewData.Add("Message", "Parking lot is empty!");
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult RemoveVehicle(Vehicle oldVehicle)
        {

                var vehicle = _context.Vehicles.Where(v => v.licensePlate == oldVehicle.licensePlate).Select(v => v).FirstOrDefault();
                if (vehicle != null)
                {
                    Spot selected = _context.spots.Where(s => s.id == vehicle.spotId).Select(s => s).FirstOrDefault();

                    selected.isOccupied = false;
                    _context.spots.Update(selected);
                    vehicle.spotId = null;
                    _context.Vehicles.Update(vehicle);

                    var lot = _context.lot.FirstOrDefault();

                    lot.availableSpots += 1;
                    _context.lot.Update(lot);
                    _context.SaveChanges();

                }
                return View(vehicle);


        }
    }
}
