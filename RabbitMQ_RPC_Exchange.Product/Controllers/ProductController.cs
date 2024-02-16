using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ_RPC_Exchange.Product.Services;
using RabbitMQ_RPC_Exchange.Shared.Contracts;
using RabbitMQ_RPC_Exchange.Shared.Models;

namespace RabbitMQ_RPC_Exchange.Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ICatalog _catalog;
        public ProductController(ICatalog catalog)
        {
            _catalog = catalog;
        }
        [HttpGet("/api/GetProductsListFromSameService")]
        public async Task<IActionResult> GetBySlug()
        {
           var products= _catalog.GetAllProducts();
            return Ok(products);
        }
    }
}
