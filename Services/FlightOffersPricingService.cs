using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SaudiWorldCupHub.DTO;

namespace SaudiWorldCupHub.Services
{
    public class FlightOffersPricingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://test.api.amadeus.com/v1/shopping/flight-offers/pricing"; // API URL (change it as needed)

        public FlightOffersPricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFlightOfferPricing(string rawJson, string accessToken)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Deserialize the rawJson input into a specific flight offer object
                    var flightOffers = JsonSerializer.Deserialize<object>(rawJson);

                    // Construct the payload
                    var payload = new
                    {
                        data = new
                        {
                            type = "flight-offers-pricing",
                            flightOffers = new[] { flightOffers }
                        }
                    };

                    // Serialize the payload to JSON
                    var jsonPayload = JsonSerializer.Serialize(payload);

                    // Wrap the JSON payload in StringContent with appropriate headers
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Add the Authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Define the API endpoint (ensure _apiUrl is correctly initialized)
                    var response = await client.PostAsync(_apiUrl, content);

                    // Check if the response is successful
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new HttpRequestException($"Error: {response.StatusCode}\n{errorContent}");
                    }

                    // Parse the response content
                    var responseContent = await response.Content.ReadAsStringAsync();

                    
                    return responseContent;
                    
                }
                catch (JsonException ex)
                {
                    // Handle JSON parsing errors
                    Console.WriteLine("JSON Parsing Exception: " + ex.Message);
                    throw new Exception("Failed to parse the flight offer response.", ex);
                }
                catch (HttpRequestException ex)
                {
                    // Handle HTTP request errors
                    Console.WriteLine("HTTP Request Exception: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    // Handle any unexpected exceptions
                    Console.WriteLine("Unexpected Exception: " + ex.Message);
                    throw;
                }
            }
        }

    }
}
