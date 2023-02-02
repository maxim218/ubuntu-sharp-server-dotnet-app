var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapGet("/name/my", async (context) => {
        const string name = "Maxim";
        await context.Response.WriteAsync(name);
    });

    endpoints.MapGet("/calc/sum", async (context) => {
        string a = context.Request.Query["a"];
        string b = context.Request.Query["b"];
        int sum = int.Parse(a) + int.Parse(b);
        string result = "Sum: " + sum;
        await context.Response.WriteAsync(result);
    });
});

app.Run(async context => {
    const string message = "Not found";
    await context.Response.WriteAsync(message);
});

app.Run();
