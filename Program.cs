var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


builder.Configuration.AddJsonFile("customConfig.json");


// get configuration from json file
Console.WriteLine("maxim" + " : " + app.Configuration["maxim"]);
Console.WriteLine("nina" + " : " + app.Configuration["nina"]);
Console.WriteLine("george" + " : " + app.Configuration["george"]);


app.UseDefaultFiles();
app.UseStaticFiles();


// get configuration from cmd
string xxxConf = app.Configuration["xxx"];
string yyyConf = app.Configuration["yyy"];
// print configuration
Console.WriteLine('\n' + "xxxConf: " + xxxConf);
Console.WriteLine("yyyConf: " + yyyConf + '\n');


CustomLogger.Information(app, "Information log");
CustomLogger.Warning(app, "Warning log");
CustomLogger.Error(app, "Error log");


app.Map("/api/calculate/summa", appBuilder => {
    appBuilder.Run(async context => {
	string a = context.Request.Query["a"];
	string b = context.Request.Query["b"];
	int sumInt = int.Parse(a) + int.Parse(b);
        string resultAnswerString = "Summa: " + sumInt;
	await context.Response.WriteAsync(resultAnswerString);
    });
});


app.Map("/api/set/login", appBuilder => {
    appBuilder.Run(async context => {
        string myLogin = context.Request.Query["myLogin"];
        context.Response.Cookies.Append("loginCookie", myLogin);
        string resultAnswerString = "Ok: " + myLogin;
        await context.Response.WriteAsync(resultAnswerString);
    });
});


app.Map("/api/get/cookie/login", appBuilder => {
    appBuilder.Run(async context => {
        string contentString = "undefined";
        if (context.Request.Cookies.ContainsKey("loginCookie")) {
            string ? info = context.Request.Cookies["loginCookie"];
            if (info != null) contentString = info;
        } 
        string resultAnswerString = "Login: " + contentString;
        await context.Response.WriteAsync(resultAnswerString);
    });
});


app.Map("/api/query/headers", appBuilder => {
    appBuilder.Run(async context => {
	context.Response.Headers.AccessControlAllowOrigin = "*";
	context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
        string answerString = "Date string: " + DateTime.Now.ToString();
	await context.Response.WriteAsync(answerString);
    });
});


app.Map("/page/get", appBuilder => {
    appBuilder.Run(async context => {
	const string maximPage = "customPagesHtml/maxim.html";
	const string georgePage = "customPagesHtml/george.html";
	string pageName = context.Request.Query["p"];
	bool isMax = ("max" == pageName);
	string pathResult = isMax ? maximPage : georgePage;
	await context.Response.SendFileAsync(pathResult);
    });
});


app.Map("/api/calculate/multiply/json", appBuilder => {
    appBuilder.Run(async context => {
	string a = context.Request.Query["a"];
	string b = context.Request.Query["b"];
	int multiplyInt = int.Parse(a) * int.Parse(b);
	MultiplyJsonRecord obj = new(multiplyInt, "multiply", DateTime.Now.ToString());
        await context.Response.WriteAsJsonAsync(obj);
    });
});


app.Map("/api/calculate/post/operation", appBuilder => {
    appBuilder.Run(async context => {
	var postBody = await context.Request.ReadFromJsonAsync<OperationJsonRecord>();
	string operation = postBody.operation;
	int first = postBody.first;
	int second = postBody.second;
	int resultInt = -1;
	if("div" == operation) resultInt = first / second;
	if("mod" == operation) resultInt = first % second;
	OperationAnswer obj = new(resultInt, operation);
	await context.Response.WriteAsJsonAsync(obj);
    });
});


app.Map("/get/author/information", () => {
    const string authorName = "Maxim Kolotovkin";
    return authorName;
});


app.Map("/people/info/get/{k}", (string k) => {
    if("max" == k) return "Maximov Maxim";
    if("geo" == k) return "Georgiev George";
    if("nin" == k) return "Ninova Nina";
    return "Man not found";
});


app.Map("/method/math/call/{operation}/{k}", (string operation, string k) => {
    if("kv" == operation) {
	int answerInt = int.Parse(k) * int.Parse(k);
	string msg = "Result: " + answerInt;
	return msg;
    }
    if("cube" == operation) {
	int answerInt = int.Parse(k) * int.Parse(k) * int.Parse(k);
	string msg = "Result: " + answerInt;
	return msg;
    }
    return "Operation not found";
});


app.Map("/square/rectangle/{x:int}", (int x) => {
    int kvInt = x * x;
    string message = "Square of " + x + " is " + kvInt;
    return message;
});


app.Run();


public record MultiplyJsonRecord(
    int ansInt, 
    string messageString, 
    string dateString
);


public record OperationJsonRecord(
    string operation,
    int first,
    int second
);


public record OperationAnswer(
    int result,
    string type
);


public static class CustomLogger {
    public static void Information(WebApplication app, string message) {
        app.Logger.LogInformation(message);
    }

    public static void Warning(WebApplication app, string message) {
        app.Logger.LogWarning(message);
    }

    public static void Error(WebApplication app, string message) {
        app.Logger.LogError(message);
    }
}
