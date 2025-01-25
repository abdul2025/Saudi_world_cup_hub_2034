using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiWorldCupHub.DTO
{

    public class BookingViewModel
    {
        public int Id { get; set; }
        public string ConfirmId { get; set; }
        public string Reference { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ArrivalAt { get; set; }
        public DateTime DepartureAt { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string UserEmail { get; set; } // Email from AspNetUsers
        public List<TravelerViewModel> Travelers { get; set; }
    }


    public class TravelerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


}