using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SaudiWorldCupHub.Data;
using SaudiWorldCupHub.DTO;
using SaudiWorldCupHub.Models;
using SaudiWorldCupHub.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SaudiWorldCupHub.Controllers
{
    public class FlightController : Controller
    {
        private readonly ILogger<FlightController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        private readonly FlightOffersPricingService _flightOffersPricingService;

        public FlightController(
            ILogger<FlightController> logger,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            FlightOffersPricingService flightOffersPricingService // Use the correct parameter name
            
            )
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _flightOffersPricingService = flightOffersPricingService; // Correctly assign the parameter to the field
            

        }


        [Authorize]
        public async Task<IActionResult> Flights()
        {
            ViewBag.Cities = await _context.Cities.ToListAsync();
            return View();
        }


        [Authorize]
        public async Task<IActionResult> BookingHistory()
        {
            // Get the logged-in user's ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            // Ensure that userId is not null
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not authenticated
            }

            // Retrieve bookings for the logged-in user
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId) // Filter bookings by UserId
                .Include(b => b.Travelers)  // Include travelers for each booking
                .ToListAsync();

            // Pass the list of bookings to the view
            // Pass the username to the view
            var userName = User.Identity.Name;  // This will give you the username
            ViewData["UserName"] = userName;
            return View(bookings);
        }



        // POST: Flights/Search
        [HttpPost]
        public async Task<IActionResult> SearchFlights(FlightSearchModel model)
        {
            // Example: You can log or process the form data here
            if (ModelState.IsValid)
            {
                var clientId = "w807wXhQGwnj5urujzU8jA1QFpLA2a1T";
                var clientSecret = "NJhWvPoA38hDhifM";

                var tokenService = new GetTokenAmadeus(clientId, clientSecret);
                var accessToken = await tokenService.GetAccessTokenAsync();



                // Store accessToken in the session
                HttpContext.Session.SetString("AccessToken", accessToken);

                var flightOfferService = new GetFlightOffer(
                    accessToken: accessToken,
                    originLocationCode: model.DepartureCity,  // Example: JFK for New York
                    destinationLocationCode: model.ArrivalCity,  // Example: LHR for London
                    departureDate: model.DepartureDate,  // Use yyyy-MM-dd format
                    returnDate: model.ReturnDate,  // Optional, can be null for one-way trips
                    adults: model.Passengers
                );

                var flightOffersJson = await flightOfferService.GetFlightOffersAsync();



                return PartialView("_FlightResults", flightOffersJson);

            }
            else
            {

                ViewBag.Message = "Please fill in all the required fields.";
            }

            // Return the view with the updated ViewBag data (do not redirect)
            return View(model);  // Return the current view (or a different one if needed)
        }


        public async Task<IActionResult> FlightOffersPricing()
        {
            // Retrieve data from TempData
            var data = TempData["OfferPricing"] as string;

            if (!string.IsNullOrEmpty(data))
            {
                var airlineCodesList = new List<string>();
                int travelerCount = 0;
                var cityCountryCodes = new Dictionary<string, (string CityCode, string CountryCode)>();
                var departureDetails = new List<(string IataCode, string Terminal, string DepartureTime)>();
                var arrivalDetails = new List<(string IataCode, string Terminal, string ArrivalTime)>();
                string totalPrice = null, currency = null;

                // Deserialize JSON data
                var flightOffers = JsonSerializer.Deserialize<JsonElement>(data);

                // Navigate JSON structure
                if (flightOffers.TryGetProperty("data", out var dataObj))
                {
                    // Extract flight offers
                    if (dataObj.TryGetProperty("flightOffers", out var flightOffersArray) &&
                        flightOffersArray.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var flightOffer in flightOffersArray.EnumerateArray())
                        {
                            // Extract price details
                            if (flightOffer.TryGetProperty("price", out var price))
                            {
                                totalPrice = price.GetProperty("total").GetString();
                                currency = price.GetProperty("currency").GetString();
                            }

                            // Extract validating airline codes
                            if (flightOffer.TryGetProperty("validatingAirlineCodes", out var validatingAirlineCodes) &&
                                validatingAirlineCodes.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var airlineCode in validatingAirlineCodes.EnumerateArray())
                                {
                                    airlineCodesList.Add(airlineCode.GetString());
                                }
                            }

                            // Extract itineraries (departure and arrival details)
                            if (flightOffer.TryGetProperty("itineraries", out var itineraries) &&
                                itineraries.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var itinerary in itineraries.EnumerateArray())
                                {
                                    if (itinerary.TryGetProperty("segments", out var segments) &&
                                        segments.ValueKind == JsonValueKind.Array)
                                    {
                                        foreach (var segment in segments.EnumerateArray())
                                        {
                                            if (segment.TryGetProperty("departure", out var departure))
                                            {
                                                departureDetails.Add((
                                                    departure.GetProperty("iataCode").GetString(),
                                                    departure.GetProperty("terminal").GetString(),
                                                    departure.GetProperty("at").GetString()
                                                ));
                                            }

                                            if (segment.TryGetProperty("arrival", out var arrival))
                                            {
                                                arrivalDetails.Add((
                                                    arrival.GetProperty("iataCode").GetString(),
                                                    arrival.GetProperty("terminal").GetString(),
                                                    arrival.GetProperty("at").GetString()
                                                ));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Extract traveler requirements
                    if (dataObj.TryGetProperty("bookingRequirements", out var bookingRequirements) &&
                        bookingRequirements.TryGetProperty("travelerRequirements", out var travelerRequirements) &&
                        travelerRequirements.ValueKind == JsonValueKind.Array)
                    {
                        travelerCount = travelerRequirements.GetArrayLength();
                    }
                }

                // Extract dictionaries.locations (city and country codes)
                if (flightOffers.TryGetProperty("dictionaries", out var dictionaries) &&
                    dictionaries.TryGetProperty("locations", out var locations) &&
                    locations.ValueKind == JsonValueKind.Object)
                {
                    foreach (var location in locations.EnumerateObject())
                    {
                        var locationData = location.Value;

                        if (locationData.TryGetProperty("cityCode", out var cityCode) &&
                            locationData.TryGetProperty("countryCode", out var countryCode))
                        {
                            cityCountryCodes[location.Name] = (cityCode.GetString(), countryCode.GetString());
                        }
                    }
                }

                // Populate ViewBag
                ViewBag.airlineCodesList = airlineCodesList;
                ViewBag.cityCountryCodes = cityCountryCodes;
                ViewBag.travelerCount = travelerCount.ToString();
                ViewBag.travelerCountInt = travelerCount;
                ViewBag.totalPrice = totalPrice;
                ViewBag.currency = currency;
                ViewBag.departureDetails = departureDetails;
                ViewBag.arrivalDetails = arrivalDetails;

                ViewBag.Nationalitis = await _context.Nationalitis.ToListAsync();

                // Retain TempData for future requests
                TempData.Keep("OfferPricing");

                return View();
            }
            else
            {
                // No data found in TempData
                Console.WriteLine("No data found in TempData.");
                return RedirectToAction("Flights");
            }
        }




        public async Task<IActionResult> BookingConfirmed()
        {
            // Retrieve data from TempData
            var data = TempData["BookingConfirmationData"] as string;

            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    // Deserialize the JSON response into a JsonElement (instead of using a strongly-typed model)
                    var response = JsonSerializer.Deserialize<JsonElement>(data);

                    // Access the 'data' section of the response
                    var dataElement = response.GetProperty("data");
                    var locations = response.GetProperty("dictionaries").GetProperty("locations");

                    // Map the JSON response to the Booking model
                    var booking = new Bookings
                    {
                        ConfirmId = dataElement.GetProperty("id").GetString(),
                        Reference = dataElement.GetProperty("associatedRecords")[0].GetProperty("reference").GetString(),
                        CreationDate = DateTime.Parse(dataElement.GetProperty("associatedRecords")[0].GetProperty("creationDate").GetString()),

                        // Ensure that the DateTime fields are correctly parsed
                        ArrivalAt = DateTime.Parse(dataElement.GetProperty("flightOffers")[0].GetProperty("itineraries")[0]
                            .GetProperty("segments")[0].GetProperty("arrival").GetProperty("at").GetString()),
                        DepartureAt = DateTime.Parse(dataElement.GetProperty("flightOffers")[0].GetProperty("itineraries")[0]
                            .GetProperty("segments")[0].GetProperty("departure").GetProperty("at").GetString()),

                        Currency = dataElement.GetProperty("flightOffers")[0].GetProperty("price").GetProperty("currency").GetString(),
                        Total = decimal.Parse(dataElement.GetProperty("flightOffers")[0].GetProperty("price").GetProperty("total").GetString())
                    };

                    // Get the departure and arrival IATA codes
                    var departureIata = dataElement.GetProperty("flightOffers")[0].GetProperty("itineraries")[0]
                        .GetProperty("segments")[0].GetProperty("departure").GetProperty("iataCode").GetString();
                    var arrivalIata = dataElement.GetProperty("flightOffers")[0].GetProperty("itineraries")[0]
                        .GetProperty("segments")[0].GetProperty("arrival").GetProperty("iataCode").GetString();

                    // Retrieve city names from locations dictionary using IATA codes
                    var departureCity = locations.GetProperty(departureIata).GetProperty("cityCode").GetString();
                    var arrivalCity = locations.GetProperty(arrivalIata).GetProperty("cityCode").GetString();

                    // Add cities to the booking object
                    booking.DepartureCity = departureCity;
                    booking.ArrivalCity = arrivalCity;

                    // Assign the currently logged-in user's ID to the booking
                    booking.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // or User.Identity.Name if you want the username

                    // Get the username of the logged-in user
                    var userName = User.Identity.Name;  // This will give you the username

                    // Create the traveler records and link them to the booking
                    var travelers = new List<Traveler>();
                    foreach (var travelerElement in dataElement.GetProperty("travelers").EnumerateArray())
                    {
                        var traveler = new Traveler
                        {
                            TravelerId = travelerElement.GetProperty("id").GetString(),
                            DateOfBirth = DateTime.Parse(travelerElement.GetProperty("dateOfBirth").GetString()),
                            Gender = travelerElement.GetProperty("gender").GetString(),
                            Name = $"{travelerElement.GetProperty("name").GetProperty("firstName").GetString()} {travelerElement.GetProperty("name").GetProperty("lastName").GetString()}",
                            Contact = travelerElement.GetProperty("contact").GetProperty("emailAddress").GetString()
                        };

                        travelers.Add(traveler);
                    }

                    // Assign the list of travelers to the booking
                    booking.Travelers = travelers;

                    // Add the booking to the database
                    _context.Bookings.Add(booking);
                    await _context.SaveChangesAsync();

                    // Pass the username to the view
                    ViewData["UserName"] = userName;

                    // Return the View to display the booking details
                    return View(booking);
                }
                catch (Exception ex)
                {
                    // Log the error and return to an error view
                    Console.WriteLine($"Error: {ex.Message}");
                    return View("Error"); // You can create an error view to handle this
                }
            }
            else
            {
                // No data found in TempData
                Console.WriteLine("No data found in TempData.");
                return RedirectToAction("Flights");
            }
        }



        // POST: CreateTheBooking (Handles the AJAX request)
        [HttpPost]
        public async Task<IActionResult> CreateTheBooking(List<PassengerBooking> passengers)
        {
            try
            {

                // Retrieve the access token from the session
                var retrievedAccessToken = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(retrievedAccessToken))
                {
                    return Unauthorized(new
                    {
                        Message = "Access token is missing or expired. Please log in again."
                    });
                }


                var data = TempData["OfferPricing"] as string;

                var bookingService = new CreateBooking(data, passengers, retrievedAccessToken);
                var response = await bookingService.CreateBookingAsync();
                TempData["BookingConfirmationData"] = response;


                // For demonstration purposes, let's return a success response
                return Json(new { success = true, message = "Booking successfully created!" });
            }
            catch (Exception ex)
            {
                // If any error occurs, return an error response
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> FlightOffersPricingService([FromBody] string rawJson)
        {
            try
            {
                // Retrieve the access token from the session
                var retrievedAccessToken = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(retrievedAccessToken))
                {
                    return Unauthorized(new
                    {
                        Message = "Access token is missing or expired. Please log in again."
                    });
                }

                // Call the service to process the flight offer pricing
                var response = await _flightOffersPricingService.GetFlightOfferPricing(rawJson, retrievedAccessToken);
                

                // Save data in Temp to the next page
                TempData["OfferPricing"] = response;


                // Pass the response to the FlightOffersPricing.cshtml view
                return Content("Success");
            }
            catch (System.Text.Json.JsonException ex)
            {
                return BadRequest(new
                {
                    Message = "Invalid JSON format.",
                    Error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An internal server error occurred.",
                    Error = ex.Message
                });
            }
        }



        
    }
}