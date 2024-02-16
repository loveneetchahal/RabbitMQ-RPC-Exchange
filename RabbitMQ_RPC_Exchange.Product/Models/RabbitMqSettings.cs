namespace RabbitMQ_RPC_Exchange.Product.Models;

public class RabbitMqSettings
{
    public string Host { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string QueueName { get; set; } = null!;
}
