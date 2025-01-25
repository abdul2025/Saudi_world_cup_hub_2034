using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiWorldCupHub.DTO
{
    public class FlightOfferResponseDto
    {
        public PriceDto Price { get; set; }
        public BookingRequirementsDto BookingRequirements { get; set; }
        public FlightLocationDto Locations { get; set; }
    }

    public class PriceDto
    {
        public string Total { get; set; }
        public string Currency { get; set; }
    }

    public class BookingRequirementsDto
    {
        public bool EmailAddressRequired { get; set; }
        public bool MobilePhoneNumberRequired { get; set; }
        public List<TravelerRequirementDto> TravelerRequirements { get; set; }
    }

    public class TravelerRequirementDto
    {
        public string TravelerId { get; set; }
        public bool GenderRequired { get; set; }
        public bool DateOfBirthRequired { get; set; }
        public bool RedressRequired { get; set; }
        public bool ResidenceRequired { get; set; }
    }

    public class FlightLocationDto
    {
        public LocationDto Departure { get; set; }
        public LocationDto Arrival { get; set; }
    }

    public class LocationDto
    {
        public string CityCode { get; set; }
        public string CountryCode { get; set; }
    }
}