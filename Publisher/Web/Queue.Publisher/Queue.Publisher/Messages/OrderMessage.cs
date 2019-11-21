using System.Collections.Generic;

namespace Queue.Publisher.Messages
{
    public class OrderMessage
    {
        public string UserName { get; set; }

        public List<ProductMessage> Products { get; set; }
    }
}
