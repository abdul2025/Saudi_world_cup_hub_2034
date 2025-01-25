namespace SaudiWorldCupHub.DTO
{

    public class FlightOfferViewModel
    {
        public string Id { get; set; }
        public string Carrier { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string CabinClass { get; set; }
        public bool OneWay { get; set; }
        public string RawJson { get; set; } // New field for raw JSON

    }

}