using EmailAPI.Models;
using EmailAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IReceiverService receiverService;

        public ReceiverController(IReceiverService receiverService)
        {
            this.receiverService = receiverService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<Receiver> Get()
        {
            return receiverService.GetAll();
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("{id}")]
        public ActionResult<Receiver> Get(int id)
        {
            var receiver = receiverService.GetById(id);

            if (receiver != null)
            {
                return receiver;
            }

            return NotFound();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Receiver> Post([FromBody] Receiver receiver)
        {
            receiverService.Create(receiver);

            return CreatedAtAction(nameof(Get), new { id = receiver.Id }, receiver);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Receiver> Put([FromBody] Receiver receiver)
        {
            receiverService.Update(receiver);

            return CreatedAtAction(nameof(Get), new { id = receiver.Id }, receiver);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var receiver = receiverService.GetById(id);

            if (receiver == null)
            {
                return NotFound();
            }

            receiverService.Delete(receiver);

            return Ok();
        }
    }
}
