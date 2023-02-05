using System.ComponentModel.DataAnnotations;
using ConsoleApp;
using Microsoft.EntityFrameworkCore;

//////////////////////////////////////////////////////////

using (ApplicationContext db = new ApplicationContext()) {
    try {
        string firstName = Reader.ReadString("FirstName");
        string lastName = Reader.ReadString("LastName");
        int age = Reader.ReadInt("Age");
        string email = Reader.ReadString("Email");
        string login = Reader.ReadString("Login");

        Person obj = new Person {
            FirstName = firstName,
            LastName = lastName,
            Age = age,
            Email = email,
            Login = login
        };
    
        db.Persons.Add(obj);
        db.SaveChanges();
    } catch (Exception error) {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine(error);
        Console.WriteLine("-------------------------------------------------");
    }
}

//////////////////////////////////////////////////////////

namespace ConsoleApp {
    public sealed class ApplicationContext : DbContext {
        public DbSet <Person> Persons { get; set; } = null!;
        
        public ApplicationContext() => Database.EnsureCreated();
        private const string ConnectionString = "Host=localhost;Port=5432;Database=console_app_database;Username=postgres;Password=12345";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(ConnectionString);
    }

    [Index("Email", IsUnique = true, Name = "email_unique_index")]
    [Index("Login", IsUnique = true, Name = "login_unique_index")]
    public class Person {
        public int Id { get; set; } = 0;
        
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public int Age { get; set; } = 0;
        
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Login { get; set; } = string.Empty;
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

//////////////////////////////////////////////////////////

/*
    SELECT tablename, indexname, indexdef  
    FROM pg_indexes  
    WHERE schemaname = 'public'  
    ORDER BY tablename, indexname;  
*/

/*
    SELECT * FROM public."Persons" ORDER BY public."Persons"."Id" DESC; 
*/
