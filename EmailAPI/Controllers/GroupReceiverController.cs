using EmailAPI.Models;
using EmailAPI.Models.Dto;
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
    public class GroupReceiverController : ControllerBase
    {
        private readonly IGroupReceiverService groupReceiverService;

        public GroupReceiverController(IGroupReceiverService groupReceiverService)
        {
            this.groupReceiverService = groupReceiverService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<GroupReceiver> Get()
        {
            return groupReceiverService.GetAll();
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("{id}")]
        public ActionResult<GroupReceiver> Get(int id)
        {
            var groupReceiver = groupReceiverService.GetById(id);

            if (groupReceiver != null)
            {
                return groupReceiver;
            }

            return NotFound();
        }
        /// <summary>
        /// Returns GroupReceivers with group and receiver information in it, by group Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Returns GroupReceivers with group and receiver information in it, by group Id</returns>
        [Authorize(Roles = "Administrator,User")]
        [HttpGet]
        [Route("GetGroupReceivers")]
        public ActionResult<List<GroupReceiver>> GetGroupReceivers([FromQuery]int groupId)
        {
            var groupReceiver = groupReceiverService.GetGroupAndReceivers(groupId);

            if (groupReceiver != null)
            {
                return groupReceiver;
            }

            return NotFound();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<GroupReceiver> Post([FromBody] GroupReceiver groupReceiver)
        {
            groupReceiverService.Create(groupReceiver);

            return CreatedAtAction(nameof(Get), new { id = groupReceiver.Id }, groupReceiver);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<GroupReceiver> Put([FromBody] GroupReceiver groupReceiver)
        {
            groupReceiverService.Update(groupReceiver);

            return CreatedAtAction(nameof(Get), new { id = groupReceiver.Id }, groupReceiver);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var groupReceiver = groupReceiverService.GetById(id);

            if (groupReceiver == null)
            {
                return NotFound();
            }

            groupReceiverService.Delete(groupReceiver);

            return Ok();
        }
    }
}
