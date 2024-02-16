using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ_RPC_Exchange.Shared.Contracts;
using RabbitMQ_RPC_Exchange.Shared.Models;

namespace RabbitMQ_RPC_Exchange.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        IRequestClient<ProductInfoRequest> _client;
        public OrderController(IRequestClient<ProductInfoRequest> client)
        {
            _client = client;
        }
        [HttpGet("/api/GetProductFromCrossPlatform")]
        public async Task<IActionResult> GetBySlug()
        {
            Products p = null;
            // request from the remote service
            using (var request = _client.Create(new ProductInfoRequest { Slug = "g-ps4", Delay = 2 }))
            {
                var response = await request.GetResponse<ProductInfoResponse>();
                p = response.Message.Product;
            }
            return Ok(p);
        }

    }
}
