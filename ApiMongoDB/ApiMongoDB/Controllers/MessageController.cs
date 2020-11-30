using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMongoDB.Models;
using ApiMongoDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMongoDB.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly MessageService _messageservice;

        public MessageController(MessageService messageservice)
        {
            _messageservice = messageservice;
        }

        [HttpGet]
        public ActionResult<List<Mensaje>> Get() =>
            _messageservice.Get();

        [HttpGet("{id:length(24)}", Name = "GetMessage")]
        public ActionResult<Mensaje> Get(string id)
        {
            var message = _messageservice.Get(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        [HttpPost]
        public ActionResult<Mensaje> Create(Mensaje message)
        {
            _messageservice.Create(message);

            return CreatedAtRoute("GetMessage", new { id = message.Id.ToString() }, message);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Mensaje messageIn)
        {
            var message = _messageservice.Get(id);

            if (message == null)
            {
                return NotFound();
            }

            _messageservice.Update(id, messageIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var message = _messageservice.Get(id);

            if (message == null)
            {
                return NotFound();
            }

            _messageservice.Remove(message.Id);

            return NoContent();
        }
    }
}
