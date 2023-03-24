using MathApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IPerimeterCalculator, PerimeterCalculator>();
builder.Services.AddTransient<ISquareCalculator, SquareCalculator>();
builder.Services.AddTransient<IHeadersSetter, HeadersSetter>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
