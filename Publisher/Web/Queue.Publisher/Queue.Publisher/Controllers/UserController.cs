using System.Text;
using Microsoft.AspNetCore.Mvc;
using NATS.Client;
using Newtonsoft.Json;
using Queue.Publisher.Messages;
using Queue.Publisher.ViewModels;

namespace Queue.Publisher.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] UserViewModel userViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            using (var stanConnection = new ConnectionFactory().CreateConnection("nats://127.0.0.1:4223"))
            {
                var userMessage = CreateUserMessageFromUserViewModel(userViewModel);
                var json = JsonConvert.SerializeObject(userViewModel);

                stanConnection.Publish("receive-user", Encoding.UTF8.GetBytes(json));

                return Ok($"Message has been published: {json}");
            }
        }

        private UserMessage CreateUserMessageFromUserViewModel(UserViewModel userViewModel)
        {
            return new UserMessage
            {
                Email = userViewModel.Email,
                Name = userViewModel.Name
            };
        }
    }
}
