using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<MyBackgroundService>(ctx =>
                {
                    var blockingService = bool.Parse(builder.Configuration["blockingService"]);
                    return new MyBackgroundService(blockingService);
                })
                .AddHostedService(ctx => ctx.GetRequiredService<MyBackgroundService>());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/counter", ([FromServices] MyBackgroundService service) =>
{
    return service.Counter;
});

app.Run();
