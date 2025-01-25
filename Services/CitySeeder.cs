using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SaudiWorldCupHub.Data;
using SaudiWorldCupHub.Models;

namespace SaudiWorldCupHub.Services
{
    public class CitySeeder
    {
        public static void SeedCities(ApplicationDbContext context, IWebHostEnvironment env)
        {
            // Check if the Cities table has data
            if (!context.Cities.Any())
            {
                // Construct the path to the JSON file in wwwroot
                var jsonFilePath = Path.Combine(env.WebRootPath, "intalData", "KSA_cities.json");
                
                // Read the JSON file content
                string json = File.ReadAllText(jsonFilePath);

                // Deserialize the JSON content
                var cities = JsonConvert.DeserializeObject<CityList>(json);

                // Add cities to the database
                foreach (var city in cities.Cities)
                {
                    context.Cities.Add(new Cities
                    {
                        Name = city.Name,
                        Code = city.Code
                    });
                }

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}

public class CityList
{
    public required List<Cities> Cities { get; set; }
}