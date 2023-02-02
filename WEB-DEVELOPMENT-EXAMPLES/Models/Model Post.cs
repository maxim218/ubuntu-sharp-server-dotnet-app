using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();

[Controller]
public class PostParamsController : Controller {
    [Route("/params/post")]
    public IActionResult PrintParams([FromBody] ClassABC obj) {
        string result = obj.BuildInfo();
        return Content(result);
    }
}

public class ClassABC {
    public int A { get; set; } = 0;
    public int B { get; set; } = 0;
    public int C { get; set; } = 0;

    public string BuildInfo() {
        string sA = "A: " + A;
        string sB = "B: " + B;
        string sC = "C: " + C;
        return sA + '\n' + sB + '\n' + sC;
    }
}