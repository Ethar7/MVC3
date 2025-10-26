using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
namespace GymSystemG2AL.Data.DataSeed
{
    public class GymDbContextSeeding
    {
        public static bool SeedData(GymSystemDBContext dBContext)
        {
            try
            {
                var HasPlans = dBContext.Plans.Any();
                var HasCategories = dBContext.Categories.Any();

                if (HasPlans && HasCategories) return false;

                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFiles<Plan>("plans.json");
                    if (Plans.Any())
                        dBContext.Plans.AddRange(Plans);
                }
                if (!HasCategories)
                {
                    var Catigories = LoadDataFromJsonFiles<Category>("categories.json");
                    if (Catigories.Any())
                        dBContext.Categories.AddRange(Catigories);

                }

            }
            catch (System.Exception)
            {

                throw;
            }
            return dBContext.SaveChanges() > 0;
        }
        public static List<T>LoadDataFromJsonFiles<T> (string FileName)
        {
            // "C:\Users\ragab\Downloads\categories.json"

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FileName);

            if (!File.Exists(FilePath))
                throw new FileNotFoundException();

            string Data = File.ReadAllText(FilePath);

            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? new List<T>();
        }
    }
}