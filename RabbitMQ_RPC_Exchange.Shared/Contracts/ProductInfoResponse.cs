using RabbitMQ_RPC_Exchange.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_RPC_Exchange.Shared.Contracts
{
    public class ProductInfoResponse
    {
        public Products Product { get; set; }
    }
}
