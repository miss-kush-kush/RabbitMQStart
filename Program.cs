using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var configuration = builder.Configuration;
builder.Services.AddMassTransit(x=>
{
    x.UsingRabbitMq((context, cfg)=>
    {
        var host = "192.168.56.1";
        var port = 5672;
        var virtualHost = "";
        var username = "guest";
        var password = "guest";

        cfg.Host(new Uri($"rabbitmq://{host}:{port}/{virtualHost}"), h =>
        {
            h.Username(username);
            h.Password(password);
        });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
