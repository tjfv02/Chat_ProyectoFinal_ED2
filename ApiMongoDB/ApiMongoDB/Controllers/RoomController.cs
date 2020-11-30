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
    [Route("api/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomservice;

        public RoomController(RoomService roomservice)
        {
            _roomservice = roomservice;
        }

        [HttpGet]
        public ActionResult<List<Sala>> Get() =>
            _roomservice.Get();

        [HttpGet("{id:length(24)}", Name = "GetRoom")]
        public ActionResult<Sala> Get(string id)
        {
            var room = _roomservice.Get(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        [HttpPost]
        public ActionResult<Sala> Create(Sala room)
        {
            _roomservice.Create(room);

            return CreatedAtRoute("GetRoom", new { id = room.Id.ToString() }, room);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Sala roomIn)
        {
            var room = _roomservice.Get(id);

            if (room == null)
            {
                return NotFound();
            }

            _roomservice.Update(id, roomIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var room = _roomservice.Get(id);

            if (room == null)
            {
                return NotFound();
            }

            _roomservice.Remove(room.Id);

            return NoContent();
        }
    }
}
