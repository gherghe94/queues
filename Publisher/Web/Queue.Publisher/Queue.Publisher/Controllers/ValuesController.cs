using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NATS.Client;
using Newtonsoft.Json;
using Queue.Publisher.Messages;
using Queue.Publisher.ViewModels;

namespace Queue.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

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

                stanConnection.Publish("receive-employees", Encoding.UTF8.GetBytes(json));

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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
