var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<NumbersCheckMiddleware>();
var app = builder.Build();

app.UseMiddleware<NumbersCheckMiddleware>();

app.Map("/api/calculate/sum", appBuilder => {
    appBuilder.Run(async context => {
        string aParam = context.Request.Query["a"];
        string bParam = context.Request.Query["b"];
        int a = int.Parse(aParam);
        int b = int.Parse(bParam);
        int summa = a + b;
        string message = "Summa: " + summa;
        await context.Response.WriteAsync(message);
    });
});

app.Run();

internal class NumbersCheckMiddleware : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        string a = context.Request.Query["a"];
        string b = context.Request.Query["b"];
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) {
            const string message = "Not correct params";
            await context.Response.WriteAsync(message); 
        } else {
            await next(context);
        }
    }
}
