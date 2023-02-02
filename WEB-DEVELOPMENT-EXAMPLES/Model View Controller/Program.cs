using Microsoft.AspNetCore.Mvc;
using NameSpaceMyMainPage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();

app.Run();

[Controller]
public class PagesController : Controller {
    [Route("/pages/main")]
    public IActionResult MainPageView() {
        ViewData["myMessage"] = "Hello wonderful world";
        MyPageFields pageFields = new MyPageFields {
            Age = 218,
            Animals = new string[] {"Cat", "Elephant", "Dog", "Monkey"}
        };
        ViewData["myObj"] = pageFields;
        return View("Views/MainPageView.cshtml");
    }
}

namespace NameSpaceMyMainPage {
    public class MyPageFields {
        public int Age = 0;
        public string[] ? Animals = null;
    }
}

namespace NameSpacePerson {
    public class Person {
        private readonly string _name = string.Empty;
        private readonly int _age = 0;

        public Person(string name, int age) {
            this._name = name;
            this._age = age;
        }

        public string GetName() => this._name;
        public int GetAge() => this._age;
    }
}