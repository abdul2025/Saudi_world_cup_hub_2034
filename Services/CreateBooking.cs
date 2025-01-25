using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SaudiWorldCupHub.DTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SaudiWorldCupHub.Services
{
    public class CreateBooking
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://test.api.amadeus.com/v1/booking/flight-orders"; // API URL (change it as needed)
        private readonly string _accessToken;
        private readonly List<PassengerBooking> _userInputs;
        private readonly string _rawJson;

        public CreateBooking(string rawJson, List<PassengerBooking> userInputs, string accessToken)
        {
            _rawJson = rawJson;
            _userInputs = userInputs;
            _accessToken = accessToken;
        }


        public async Task<string> CreateBookingAsync()
        {

            var flightOffersJson = JsonSerializer.Deserialize<JsonElement>(_rawJson);
            // Console.WriteLine("flightOffersJson");

            var flightOffersList = flightOffersJson.GetProperty("data").GetProperty("flightOffers").EnumerateArray().ToList();


            // Use the first passenger as a reference for contact details
            var firstPassenger = _userInputs[0];

            // Build the payload
            var requestPayload = new
            {
                data = new
                {
                    type = "flight-order",
                    flightOffers = flightOffersList, // Directly using the flightOffersList here
                    travelers = _userInputs.Select((passenger, index) => new
                    {
                        id = (index + 1).ToString(),
                        dateOfBirth = passenger.DateOfBirth.ToString("yyyy-MM-dd"),
                        name = new
                        {
                            firstName = passenger.FirstName.ToUpper(),
                            lastName = passenger.LastName.ToUpper()
                        },
                        gender = passenger.Gender.ToUpper(), // Ensure correct format
                        contact = new
                        {
                            emailAddress = passenger.EmailAddress,
                            phones = new[]
                            {
                                new
                                {
                                    deviceType = "MOBILE",
                                    countryCallingCode = "34", // Spain country code
                                    number = passenger.PhoneNumber
                                }
                            }
                        },
                        documents = new[]
                        {
                            new
                            {
                                documentType = "PASSPORT",
                                birthPlace = "Madrid", // Replace with actual data if needed
                                issuanceLocation = passenger.PassportIssuanceLocation,
                                issuanceDate = passenger.PassportIssuanceDate.ToString("yyyy-MM-dd"),
                                number = passenger.PassportNumber,
                                expiryDate = passenger.PassportExpiryDate.ToString("yyyy-MM-dd"),
                                issuanceCountry = passenger.PassportNationality,
                                validityCountry = passenger.PassportNationality,
                                nationality = passenger.PassportNationality,
                                holder = true
                            }
                        }
                    }),
                    remarks = new
                    {
                        general = new[]
                        {
                            new
                            {
                                subType = "GENERAL_MISCELLANEOUS",
                                text = "ONLINE BOOKING FROM INCREIBLE VIAJES"
                            }
                        }
                    },
                    ticketingAgreement = new
                    {
                        option = "DELAY_TO_CANCEL",
                        delay = "6D"
                    },
                    contacts = new[]
                    {
                        new
                        {
                            addresseeName = new
                            {
                                firstName = firstPassenger.FirstName,
                                lastName = firstPassenger.LastName
                            },
                            companyName = "INCREIBLE VIAJES",
                            purpose = "STANDARD",
                            phones = new[]
                            {
                                new
                                {
                                    deviceType = "LANDLINE",
                                    countryCallingCode = "34",
                                    number = "480080071"
                                },
                                new
                                {
                                    deviceType = "MOBILE",
                                    countryCallingCode = "33",
                                    number = "480080072"
                                }
                            },
                            emailAddress = firstPassenger.EmailAddress,
                            address = new
                            {
                                lines = new[] { "Calle Prado, 16" },
                                postalCode = "28014",
                                cityName = "Madrid",
                                countryCode = "ES"
                            }
                        }
                    }
                }
            };

            var jsonPayload = JsonSerializer.Serialize(requestPayload);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                
                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                    
                        Console.WriteLine("Booking successful: ");
                        return responseContent;
                    }
                    else
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error booking flight: " + errorResponse);
                        return errorResponse;
                    }
                }catch (HttpRequestException ex)
                {
                    // Handle HTTP request errors
                    Console.WriteLine("HTTP Request Exception: " + ex.Message);
                    throw;
                }
            }
        }
        
    }
}