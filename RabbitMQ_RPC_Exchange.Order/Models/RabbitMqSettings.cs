namespace RabbitMQ_RPC_Exchange.Order.Models;

public class RabbitMqSettings
{
    public string Host { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string QueueName { get; set; } = null!;
}
