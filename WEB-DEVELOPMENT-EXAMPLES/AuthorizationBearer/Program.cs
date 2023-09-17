using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

//////////////////////////////////////////////////////

Dictionary<string, string> dictUsers = new Dictionary<string, string>();
dictUsers["maxim"] = "1234";
dictUsers["nina"] = "6789";
dictUsers["george"] = "9182";

//////////////////////////////////////////////////////

//  dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.4

//////////////////////////////////////////////////////

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

//////////////////////////////////////////////////////

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

//////////////////////////////////////////////////////

app.Map("/user/info/get", appBuilder => {
    appBuilder.Run(async context => {
        try {
            var user = context.User.Identity;
            if (user is not null && user.IsAuthenticated) {
                string login = user.Name!;
                await context.Response.WriteAsync("Login: " + login);
            } else {
                throw new Exception("You are not authorized");
            }
        } catch {
            await context.Response.WriteAsync("You are not authorized");
        }
    });
});

//////////////////////////////////////////////////////

app.Map("/user/auth", appBuilder => {
	appBuilder.Run(async context => {
        try {
            string login = context.Request.Query["login"]!;
            string password = context.Request.Query["password"]!;
            if (password.Trim() == dictUsers[login].Trim()) {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(365 * 24 * 60)),
                    signingCredentials: new SigningCredentials(
                        AuthOptions.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256
                    )
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                await context.Response.WriteAsync(encodedJwt);
            } else {
                throw new Exception("Not correct login or password");
            }
        } catch {
            await context.Response.WriteAsync("Not correct login or password");
        }
	});
});

//////////////////////////////////////////////////////


app.Run();

//////////////////////////////////////////////////////

public static class AuthOptions {
    public const string ISSUER = "ServerPart";
    public const string AUDIENCE = "ClientPart"; 
    private const string KEY = "secret-key-123-789";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
};
