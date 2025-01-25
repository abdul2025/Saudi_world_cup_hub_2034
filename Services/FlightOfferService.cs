using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using SaudiWorldCupHub.DTO;

namespace SaudiWorldCupHub.Services
{
    public class GetFlightOffer
    {
        private readonly string _url = "https://test.api.amadeus.com/v2/shopping/flight-offers";
        private readonly string _accessToken;
        private readonly string _originLocationCode;
        private readonly string _destinationLocationCode;
        private readonly DateTime _departureDate;
        private readonly DateTime? _returnDate;
        private readonly int _adults;

        public GetFlightOffer(
            string accessToken,
            string originLocationCode,
            string destinationLocationCode,
            DateTime departureDate,
            DateTime? returnDate,
            int adults)
        {
            _accessToken = accessToken;
            _originLocationCode = originLocationCode;
            _destinationLocationCode = destinationLocationCode;
            _departureDate = departureDate;
            _returnDate = returnDate;
            _adults = adults;
        }

        public async Task<List<FlightOfferViewModel>> GetFlightOffersAsync()
        {
            var parameters = new Dictionary<string, string>
            {
                { "originLocationCode", _originLocationCode },
                { "destinationLocationCode", _destinationLocationCode },
                { "departureDate", _departureDate.ToString("yyyy-MM-dd") },
                { "adults", _adults.ToString() },
                { "max", "5" }
            };

            if (_returnDate.HasValue)
            {
                parameters.Add("returnDate", _returnDate.Value.ToString("yyyy-MM-dd"));
            }

            var requestUrl = QueryHelpers.AddQueryString(_url, parameters);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                try
                {
                    var response = await client.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        using var jsonDocument = JsonDocument.Parse(responseContent);

                        if (jsonDocument.RootElement.TryGetProperty("data", out JsonElement dataElement))
                        {
                            var flightOffers = new List<FlightOfferViewModel>();

                            foreach (var flightOffer in dataElement.EnumerateArray())
                            {
                                var viewModel = new FlightOfferViewModel
                                {
                                    Id = flightOffer.GetProperty("id").GetString(),
                                    Carrier = flightOffer.GetProperty("itineraries")[0]
                                        .GetProperty("segments")[0]
                                        .GetProperty("carrierCode").GetString(),
                                    DepartureCity = flightOffer.GetProperty("itineraries")[0]
                                        .GetProperty("segments")[0]
                                        .GetProperty("departure")
                                        .GetProperty("iataCode").GetString(),
                                    ArrivalCity = flightOffer.GetProperty("itineraries")[0]
                                        .GetProperty("segments")[0]
                                        .GetProperty("arrival")
                                        .GetProperty("iataCode").GetString(),
                                    DepartureTime = flightOffer.GetProperty("itineraries")[0]
                                        .GetProperty("segments")[0]
                                        .GetProperty("departure")
                                        .GetProperty("at").GetString(),
                                    ArrivalTime = flightOffer.GetProperty("itineraries")[0]
                                        .GetProperty("segments")[0]
                                        .GetProperty("arrival")
                                        .GetProperty("at").GetString(),
                                    Duration = flightOffer.GetProperty("itineraries")[0].GetProperty("duration").GetString(),
                                    Price = flightOffer.GetProperty("price").GetProperty("total").GetString(),
                                    Currency = flightOffer.GetProperty("price").GetProperty("currency").GetString(),
                                    CabinClass = flightOffer.GetProperty("travelerPricings")[0]
                                        .GetProperty("fareDetailsBySegment")[0]
                                        .GetProperty("cabin").GetString(),
                                    OneWay = flightOffer.GetProperty("oneWay").GetBoolean(),
                                    RawJson = flightOffer.GetRawText() // Store raw JSON string

                                };
                                
                                flightOffers.Add(viewModel);
                            }

                            return flightOffers;
                        }
                        else
                        {
                            throw new Exception("Response does not contain a 'data' property.");
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Failed to fetch flight offers. Status Code: {response.StatusCode}. Response: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while fetching flight offers: " + ex.Message);
                }
            }
        }
    }
}
