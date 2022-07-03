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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<Group> Get()
        {
            return groupService.GetAll();
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("{id}")]
        public ActionResult<Group> Get(int id)
        {
            var group = groupService.GetById(id);

            if (group != null)
            {
                return group;
            }

            return NotFound();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Group> Post([FromBody] Group group)
        {
            groupService.Create(group);

            return CreatedAtAction(nameof(Get), new { id = group.Id }, group);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Group> Put([FromBody] Group group)
        {
            groupService.Update(group);

            return CreatedAtAction(nameof(Get), new { id = group.Id }, group);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var group = groupService.GetById(id);

            if (group == null)
            {
                return NotFound();
            }

            groupService.Delete(group);

            return Ok();
        }
    }
}
