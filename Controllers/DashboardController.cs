using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaudiWorldCupHub.Data;
using SaudiWorldCupHub.DTO;
using SaudiWorldCupHub.Models;

namespace SaudiWorldCupHub.Controllers 
{
    public class DashboardController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DashboardController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get the sum of all booking totals asynchronously
            var totalSum = await _context.Set<Bookings>().AsQueryable()
                .SumAsync(b => b.Total);  // Use SumAsync for async execution
            

            var averageBookingValue = await _context.Set<Bookings>().AsQueryable()
                .AverageAsync(b => b.Total);

            

            var bookingsWithMultipleTravelers = await _context.Set<Bookings>()
                .Where(b => b.Travelers.Count > 1)
                .CountAsync();

            
            var avgTravelersPerBooking = await _context.Set<Bookings>()
                .Select(b => b.Travelers.Count)
                .AverageAsync();
            
            var totalTravelers = await _context.Set<Traveler>().CountAsync();
            var totalBookings = await _context.Set<Bookings>().CountAsync();





            ViewBag.totalSum = totalSum;
            ViewBag.totalBookings = totalBookings;

            ViewBag.totalTravelers = totalTravelers;
            ViewBag.avgTravelersPerBooking = avgTravelersPerBooking.ToString("F2");


            ViewBag.averageBookingValue = averageBookingValue.ToString("F2");
            ViewBag.bookingsWithMultipleTravelers = bookingsWithMultipleTravelers;



            return View();
        }


        [Authorize]
        public async Task<IActionResult> Flights()
        {
            var bookings = await _context.Bookings
            .Include(b => b.Travelers) // Includes Traveler data
            .Select(b => new 
            {
                Booking = b,
                UserEmail = _context.Users
                    .Where(u => u.Id == b.UserId) // Join with AspNetUsers table
                    .Select(u => u.Email)
                    .FirstOrDefault() // Get the email
            })
            .Select(b => new BookingViewModel
            {
                Id = b.Booking.Id,
                ConfirmId = b.Booking.ConfirmId,
                Reference = b.Booking.Reference,
                CreationDate = b.Booking.CreationDate,
                ArrivalAt = b.Booking.ArrivalAt,
                DepartureAt = b.Booking.DepartureAt,
                Currency = b.Booking.Currency,
                Total = b.Booking.Total,
                DepartureCity = b.Booking.DepartureCity,
                ArrivalCity = b.Booking.ArrivalCity,
                UserEmail = b.UserEmail, // Include the email
                Travelers = b.Booking.Travelers.Select(t => new TravelerViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Contact = t.Contact,
                    Gender = t.Gender,
                    DateOfBirth = t.DateOfBirth
                }).ToList()
            }).ToListAsync();


            return View(bookings);

        }




        [Authorize]
        public async Task<IActionResult> Cities()
        {
            var cities = await _context.Cities.ToListAsync();
            return View(cities);
        }


        // POST: Cities/Create
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> CreateCities([FromBody] Cities city)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "City added successfully" });
            }
            return Json(new { success = false, message = "Invalid city data" });
        }

        // POST: Cities/Update
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> UpdateCities([FromBody] Cities city)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Update(city);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "City updated successfully" });
            }
            return Json(new { success = false, message = "Invalid city data" });
        }

        // POST: Cities/Delete
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> DeleteCities([FromBody] int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "City deleted successfully" });
            }
            return Json(new { success = false, message = "City not found" });
        }


        // Nationalities // 
        // Nationalities // 
        // Nationalities // 
        // Nationalities // 
        // Nationalities // 
        // Nationalities // 
        // Nationalities // 
        // Nationalities // 
        [Authorize]
        public async Task<IActionResult> Nationalities()
        {
            var nationalitis = await _context.Nationalitis.ToListAsync();
            return View(nationalitis);
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNationalities([FromBody] Nationalitis nationality)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Nationalitis.Add(nationality);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Nationality added successfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error: " + ex.Message });
                }
            }
            return Json(new { success = false, message = "Invalid data!" });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateNationalities([FromBody] Nationalitis nationality)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingNationality = await _context.Nationalitis.FindAsync(nationality.Id);
                    if (existingNationality == null)
                    {
                        return Json(new { success = false, message = "Nationality not found!" });
                    }

                    existingNationality.Name = nationality.Name;
                    existingNationality.Code = nationality.Code;

                    _context.Nationalitis.Update(existingNationality);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Nationality updated successfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error: " + ex.Message });
                }
            }
            return Json(new { success = false, message = "Invalid data!" });
        }
        
        [Authorize]
        [HttpPost]
        [Route("Dashboard/DeleteNationalities")]
        public async Task<IActionResult> DeleteNationalities([FromBody] int id) // Explicitly bind from JSON body
        {
            try
            {
                Console.WriteLine("Received ID: " + id);

                var nationality = await _context.Nationalitis.FindAsync(id); // Corrected spelling
                if (nationality == null)
                {
                    return Json(new { success = false, message = "Nationality not found!" });
                }

                _context.Nationalitis.Remove(nationality); // Corrected spelling
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Nationality deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }



    }


}