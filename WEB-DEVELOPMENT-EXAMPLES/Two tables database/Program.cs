using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SchoolsDatabaseApp;

///////////////////////////////////////////////////////

using (ApplicationContext db = new ApplicationContext()) {
    try {
        StringBuilder stringBuilder = new StringBuilder("Choose operation:\n");
        stringBuilder.Append("1 - Create new City\n");
        stringBuilder.Append("2 - Get all Cities\n");
        stringBuilder.Append("3 - Get City by Id\n");
        stringBuilder.Append("4 - Update City by Id\n");
        stringBuilder.Append("5 - Remove City by Id\n");
        stringBuilder.Append("6 - Create new School\n");
        stringBuilder.Append("7 - Get School by Id\n");
        stringBuilder.Append("8 - Get Schools by City Name\n");
        string operationsMessage = stringBuilder.ToString();
        Console.WriteLine(operationsMessage);

        int operation = Reader.ReadInt("operation number");

        if (1 == operation) {
            string cityName = Reader.ReadString("city name");
            City obj = new City {
                CityName = cityName
            };
            db.Cities.Add(obj);
            db.SaveChanges();
        }

        if (2 == operation) {
            List <City> cities = db.Cities.OrderByDescending(obj => obj.Id).ToList();
            foreach (City city in cities) {
                string json = city.ToJson();
                Console.WriteLine(json);
            }
        }

        if (3 == operation) {
            int cityId = Reader.ReadInt("city id");
            City ? city = db.Cities.FirstOrDefault(obj => cityId == obj.Id);
            Console.WriteLine(null == city ? $"City with id {cityId} not found" : city.ToJson());
        }

        if (4 == operation) {
            int cityId = Reader.ReadInt("city id");
            City ? city = db.Cities.FirstOrDefault(obj => cityId == obj.Id);
            if (null == city) {
                Console.WriteLine($"City with id {cityId} not found");
            } else {
                string cityNameNew = Reader.ReadString("new city name");
                city.CityName = cityNameNew;
                db.SaveChanges();
            }
        }

        if (5 == operation) {
            int cityId = Reader.ReadInt("city id");
            City ? city = db.Cities.FirstOrDefault(obj => cityId == obj.Id);
            if (null == city) {
                Console.WriteLine($"City with id {cityId} not found");
            } else {
                db.Cities.Remove(city);
                db.SaveChanges();
            }
        }

        if (6 == operation) {
            string cityName = Reader.ReadString("city name");
            City ? city = db.Cities.FirstOrDefault(obj => cityName == obj.CityName);
            if (null == city) {
                Console.WriteLine($"City with name {cityName} not found");
            } else {
                Console.WriteLine(city.ToJson());
                string schoolName = Reader.ReadString("school name");
                School school = new School {
                    SchoolName = schoolName,
                    Town = city
                };
                db.Schools.Add(school);
                db.SaveChanges();
            }
        }

        if (7 == operation) {
            int schoolId = Reader.ReadInt("school id");
            School? school = db.Schools.Include("Town").FirstOrDefault(obj => schoolId == obj.Id);
            if (null == school) {
                Console.WriteLine($"School with id {schoolId} not found");
            } else {
                string json = school.ToJson();
                Console.WriteLine(json);
            }
        }

        if (8 == operation) {
            string cityName = Reader.ReadString("city name");
            List <School> schools = db.Schools.Include("Town")
                .Where(school => cityName == school.Town.CityName)
                .OrderByDescending(obj => obj.Id)
                .ToList();
            foreach (School school in schools) {
                string json = school.ToJson();
                Console.WriteLine(json);
            }
        }
    } catch (Exception error) {
        Console.WriteLine(error);
    }
}

///////////////////////////////////////////////////////

namespace SchoolsDatabaseApp {
    public sealed class ApplicationContext : DbContext {
        public DbSet <City> Cities { get; set; } = null!;

        public DbSet <School> Schools { get; set; } = null!;
        
        public ApplicationContext() => Database.EnsureCreated();
        private const string ConnectionString = "Host=localhost;Port=5432;Database=schools_base_app;Username=postgres;Password=12345";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(ConnectionString);
    }

    [Index("CityName", IsUnique = true, Name = "city_name_unique_index")]
    public class City {
        public int Id { get; set; } = 0;
        
        [Required] public string CityName { get; set; } = string.Empty;

        public string ToJson() {
            string json = JsonSerializer.Serialize(this);
            return json;
        }
    }

    public class School {
        public int Id { get; set; } = 0;
        
        [Required] public string SchoolName { get; set; } = string.Empty;

        [Required] public City Town { get; set; } = null!;
        
        public string ToJson() {
            string json = JsonSerializer.Serialize(this);
            return json;
        }
    }
    
    public static class Reader {
        public static string ReadString(string message) {
            Console.WriteLine("Input " + message + ":");
            string s = Console.ReadLine()!;
            return string.IsNullOrEmpty(s) ? string.Empty : s;
        }
        
        public static int ReadInt(string message) {
            Console.WriteLine("Input " + message + ":");
            try {
                string s = Console.ReadLine()!;
                int n = int.Parse(s);
                return n;
            } catch {
                return 0;
            }
        }
    }
}

///////////////////////////////////////////////////////

/* 
    SELECT * FROM public."Cities" ORDER BY public."Cities"."Id" DESC;
    
    SELECT * FROM public."Schools" ORDER BY public."Schools"."Id" DESC;
*/
