using MassTransit;
using RabbitMQ_RPC_Exchange.Product.Consumers;
using RabbitMQ_RPC_Exchange.Product.Models;
using RabbitMQ_RPC_Exchange.Product.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();

builder.Services.AddControllers();
builder.Services.AddTransient<ICatalog, Catalog>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductInfoRequestConsumer>();

#pragma warning disable CS0618 // Type or member is obsolete
    x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c =>
    {
        c.Host(new Uri(rabbitMqSettings.Host), "/", c =>
        {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
        c.ReceiveEndpoint(rabbitMqSettings.QueueName, e =>
        {
            e.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(2, 30000));
            e.ConfigureConsumer<ProductInfoRequestConsumer>(context);
        });
        //c.ReceiveEndpoint(rabbitMqSettings.QueueName, ep =>
        //{
        //    ep.ConfigureConsumeTopology = false;
        //    ep.ExchangeType = ExchangeType.Direct;

        //    ep.Bind<Product>(binding =>
        //    {
        //        binding.RoutingKey = "Code_02";
        //    });
        //    ep.ConfigureConsumer<ProductConsumer>(context);
        //});
    }));
#pragma warning restore CS0618 // Type or member is obsolete
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
