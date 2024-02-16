using RabbitMQ_RPC_Exchange.Shared.Models;

namespace RabbitMQ_RPC_Exchange.Product.Services
{
    public interface ICatalog
    {
        Products GetProductBySlug(string id);
        List<Products> GetAllProducts();
    }
}
