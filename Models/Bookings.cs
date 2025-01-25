using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiWorldCupHub.Models
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }
        public string ConfirmId { get; set; }
        public string Reference { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Traveler> Travelers { get; set; }
        public DateTime ArrivalAt { get; set; }
        public DateTime DepartureAt { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }

        // Added properties for departure and arrival cities
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string UserId { get; set; }  // You can also store User information as a foreign key if needed


    }

    public class Traveler
    {
        [Key]
        public int Id { get; set; }
        public string TravelerId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        
        public int BookingId { get; set; } // Foreign key to Bookings
        public Bookings Booking { get; set; }
    }


}