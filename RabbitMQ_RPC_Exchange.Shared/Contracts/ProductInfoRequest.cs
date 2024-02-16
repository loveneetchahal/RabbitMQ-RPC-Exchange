using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_RPC_Exchange.Shared.Contracts
{
    public class ProductInfoRequest
    {
        public string Slug { get; set; }

        // simulate a fake delay from the remote service
        public int Delay { get; set; }
    }
}
