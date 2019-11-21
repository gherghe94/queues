using System.Collections.Generic;

namespace Queues.Desktop.Services.Messages
{
    public class OrderCreatedMessage
    {
        public string UserName { get; set; }

        public List<ProductMessage> Products { get; set; }
    }
}
