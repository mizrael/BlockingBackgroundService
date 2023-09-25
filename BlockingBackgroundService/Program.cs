using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MyBackgroundService>()
                .AddHostedService(ctx => ctx.GetRequiredService<MyBackgroundService>());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/counter", ([FromServices] MyBackgroundService service ) =>
{
    return service.Counter;
});

app.Run();
