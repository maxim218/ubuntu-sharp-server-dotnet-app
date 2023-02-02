using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();

[Controller]
public class InfoController : Controller {
    [Route("/get/info/author")]
    public string GetAuthor() {
        const string author = "Maxim";
        return author;
    }

    [Route("/get/info/lang")]
    public string GetLang() {
        const string lang = "C# language";
        return lang;
    }
}

[Controller]
public class CalcController : Controller {
    [Route("/calc/sub")]
    public string SubNums(int a, int b) {
        int sub = a - b;
        string result = "Result: " + sub;
        return result;
    }

    [Route("/calc/kvAndCube")]
    public IActionResult KvAndCube(int x) {
        int kv = x * x;
        int cube = x * x * x;
        KvCube obj = new KvCube { Kv = kv, Cube = cube };
        return Json(obj);
    }
}

[Controller]
public class UserInfoRender : Controller {
    [Route("/user/post/info")]
    public async Task UserInfoPost() {
        try {
            var postBody = await Request.ReadFromJsonAsync<UserClass>();
            string message = postBody.Name + " : " + postBody.Age;
            await Response.WriteAsync(message);
        } catch {
            await Response.WriteAsync("Error");
        }
    }
}

class UserClass {
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
}

class KvCube {
    public int Kv { get; set; } = 0;
    public int Cube { get; set; } = 0;
}