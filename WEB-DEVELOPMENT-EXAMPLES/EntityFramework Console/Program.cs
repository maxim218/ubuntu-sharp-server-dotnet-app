using System.Diagnostics.CodeAnalysis;
using System.Text;
using ConsoleAppWithPostgres;
using Microsoft.EntityFrameworkCore;

////////////////////////////////////////////////////

using (ApplicationContext db = new ApplicationContext()) {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Choose the operation:\n");
    stringBuilder.Append("1 - Create new Game\n");
    stringBuilder.Append("2 - Show all Games\n");
    stringBuilder.Append("3 - Create Shop\n");
    stringBuilder.Append("4 - Show Shops\n");
    Console.WriteLine(stringBuilder.ToString());

    int operation = 12345;
    
    while (operation > 0) {
        Console.WriteLine("------------------------------------------------");
        operation = ConsoleReader.ReadConsoleInt("Input operation number:");
        Console.WriteLine("------------------------------------------------");
        
        if (1 == operation) {
            string title = ConsoleReader.ReadConsoleString("Input Title:");
            string description = ConsoleReader.ReadConsoleString("Input Description:");
            int ageFrom = ConsoleReader.ReadConsoleInt("Input age from:");
            Game obj = new Game {
                Title = title,
                Description = description,
                AgeFrom = ageFrom
            };
            db.Games.Add(obj);
            db.SaveChanges();
        }

        if (2 == operation) {
            List <Game> games = db.Games.ToList();
            foreach (Game g in games) {
                string message = $"ID - {g.Id} Title - {g.Title} Description - {g.Description} Age from - {g.AgeFrom}";
                Console.WriteLine(message);
            }
        }

        if (3 == operation) {
            string nameShop = ConsoleReader.ReadConsoleString("Input name of the Shop:");
            Shop obj = new Shop {
                NameShop = nameShop
            };
            db.Shops.Add(obj);
            db.SaveChanges();
        }

        if (4 == operation) {
            List <Shop> shops = db.Shops.ToList();
            foreach (Shop s in shops) {
                string message = $"ID - {s.Id} Name - {s.NameShop}";
                Console.WriteLine(message);
            }
        }
    }
}

////////////////////////////////////////////////////

namespace ConsoleAppWithPostgres {
    internal static class ConsoleReader {
        private static string ReadString() {
            string? s = Console.ReadLine();
            return s ?? string.Empty;
        }

        private static int ReadInt() {
            string s = ReadString();
            try {
                int result = int.Parse(s);
                return result;
            } catch {
                return 0;
            }
        }

        public static string ReadConsoleString(string message) {
            Console.WriteLine(message);
            string result = ReadString();
            return result;
        }

        public static int ReadConsoleInt(string message) {
            Console.WriteLine(message);
            int result = ReadInt();
            return result;
        }
    }
    
    public class Game {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AgeFrom { get; set; } = 0;
    }

    public class Shop {
        public int Id { get; set; } = 0;
        public string NameShop { get; set; } = string.Empty;
    }
    
    public sealed class ApplicationContext : DbContext {
        public DbSet <Game> Games { get; set; } = null!;
        public DbSet <Shop> Shops { get; set; } = null!;
        
        public ApplicationContext() => Database.EnsureCreated();
        private const string ConnectionString = "Host=localhost;Port=5432;Database=games_shop_base;Username=postgres;Password=12345";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(ConnectionString);
    }
} 

////////////////////////////////////////////////////

//  SELECT * FROM public."Games";
//  SELECT * FROM public."Shops";
