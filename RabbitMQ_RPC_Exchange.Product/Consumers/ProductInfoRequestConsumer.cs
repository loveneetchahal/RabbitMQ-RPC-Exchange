using MassTransit;
using RabbitMQ_RPC_Exchange.Product.Services;
using RabbitMQ_RPC_Exchange.Shared.Contracts;

namespace RabbitMQ_RPC_Exchange.Product.Consumers
{
    public class ProductInfoRequestConsumer
    : IConsumer<ProductInfoRequest>
    {

        readonly ICatalog _svc;

        public ProductInfoRequestConsumer(ICatalog svc)
        {
            _svc = svc;
        }

        public async Task Consume(ConsumeContext<ProductInfoRequest> context)
        {
            var msg = context.Message;
            var slug = msg.Slug;

            // a fake delay
            var delay = 1000 * (msg.Delay > 0 ? msg.Delay : 1);
            await Task.Delay(delay);

            // get the product from ProductService
            var p = _svc.GetProductBySlug(slug);

            // this responds via the queue to our client
            await context.RespondAsync(new ProductInfoResponse
            {
                Product = p
            });
        }
    }
}
