using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiWorldCupHub.DTO
{
    public class PassengerBooking
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PassportNationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportIssuanceDate { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public string PassportIssuanceLocation { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}