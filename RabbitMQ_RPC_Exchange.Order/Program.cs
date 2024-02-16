using MassTransit;
using RabbitMQ_RPC_Exchange.Order.Models;
using RabbitMQ_RPC_Exchange.Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();

builder.Services.AddControllers();



builder.Services.AddMassTransit(x =>
{
#pragma warning disable CS0618 // Type or member is obsolete
    x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c =>
    {
        c.Host(new Uri(rabbitMqSettings.Host), "/", c =>
        {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
        c.ConfigureEndpoints(context);
    }));
#pragma warning restore CS0618 // Type or member is obsolete

    x.AddRequestClient<ProductInfoRequest>();
});

//builder.Services.AddMassTransitHostedService();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
