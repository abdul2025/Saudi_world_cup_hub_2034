using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiWorldCupHub.DTO
{
    public class FlightSearchModel
    {
        public required string DepartureCity { get; set; }
        public required string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }  // Nullable for one-way trips
        public int Passengers { get; set; }
    }
}