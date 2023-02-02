var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapGet("/perimeter/calc/{a}/{b}", async (context) => {
        string ? a = Convert.ToString(context.Request.RouteValues["a"]);
        string ? b = Convert.ToString(context.Request.RouteValues["b"]);
        int sideA = int.Parse(a + "");
        int sideB = int.Parse(b + "");
        int perimeter = (sideA + sideB) * 2;
        string result = "Perimeter: " + perimeter;
        await context.Response.WriteAsync(result);
    });
});

app.Run(async context => {
    const string message = "Not found";
    await context.Response.WriteAsync(message);
});

app.Run();
