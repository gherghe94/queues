using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NATS.Client;
using Newtonsoft.Json;
using Queue.Publisher.Messages;
using Queue.Publisher.ViewModels;

namespace Queue.Publisher.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            using (var stanConnection = new ConnectionFactory().CreateConnection("nats://127.0.0.1:4223"))
            {
                var userMessage = CreateOrder(orderViewModel);
                var json = JsonConvert.SerializeObject(orderViewModel);

                stanConnection.Publish("receive-order", Encoding.UTF8.GetBytes(json));

                return Ok($"Order has been published: {json}");
            }
        }

        private OrderMessage CreateOrder(OrderViewModel orderViewModel)
        {
            return new OrderMessage
            {
                UserName = orderViewModel.UserName,
                Products = orderViewModel.Products.Select(CreateProductMessage).ToList(),
            };
        }

        private ProductMessage CreateProductMessage(ProductViewModel arg)
        {
            return new ProductMessage
            {
                Category = arg.Category,
                Name = arg.Name,
                Price = arg.Price
            };
        }
    }
}