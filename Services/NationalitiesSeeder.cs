using Newtonsoft.Json;
using SaudiWorldCupHub.Data;
using SaudiWorldCupHub.Models;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace SaudiWorldCupHub.Services
{
    public class NationalitiesSeeder
    {
        public static void SeedNationalities(ApplicationDbContext context, IWebHostEnvironment env)
        {
            // Check if the Nationalitis table has data
            if (!context.Nationalitis.Any())
            {
                try
                {
                    // Construct the path to the JSON file in wwwroot
                    var jsonFilePath = Path.Combine(env.WebRootPath, "intalData", "nationalities.json");
                    
                    // Check if the file exists
                    if (!File.Exists(jsonFilePath))
                    {
                        Console.WriteLine("The nationalities.json file was not found.");
                        return;
                    }

                    // Read the JSON file content
                    string json = File.ReadAllText(jsonFilePath);

                    // Deserialize the JSON content
                    var nationalities = JsonConvert.DeserializeObject<NationalitiesList>(json);
                    
                    // Check if the deserialization was successful and if the list is not null
                    if (nationalities?.Nationalitis != null && nationalities.Nationalitis.Any())
                    {
                        // Add nationalities to the database
                        foreach (var nation in nationalities.Nationalitis)
                        {
                            context.Nationalitis.Add(new Nationalitis
                            {
                                Name = nation.Name,
                                Code = nation.Code
                            });
                        }

                        // Save changes to the database
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("No nationalities data found or the list is empty.");
                    }
                }
                catch (System.Exception ex)
                {
                    // Handle any exceptions that occur during seeding
                    Console.WriteLine($"Error seeding nation: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Nationalitis table already contains data.");
            }
        }
    }

    // Corrected model class
    public class NationalitiesList
    {
        [JsonProperty("Nationalities")]
        public List<Nationalitis> Nationalitis { get; set; }
    }
}
