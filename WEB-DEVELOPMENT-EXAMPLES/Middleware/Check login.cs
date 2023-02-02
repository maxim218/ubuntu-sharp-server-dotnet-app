var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) => {
    string login = context.Request.Query["login"];
    if (string.IsNullOrEmpty(login)) {
        const string message = "Login should be set";
        await context.Response.WriteAsync(message);
    } else {
        await next(context);
    }
});

app.Map("/api/my/login", appBuilder => {
    appBuilder.Run(async context => {
        string login = context.Request.Query["login"];
        string message = "Your login is " + login;
        await context.Response.WriteAsync(message);
    });
});

app.Run();
