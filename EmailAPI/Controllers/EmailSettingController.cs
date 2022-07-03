using EmailAPI.Models;
using EmailAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class EmailSettingController : ControllerBase
    {
        private readonly IEmailSettingService emailSettingService;

        public EmailSettingController(IEmailSettingService emailSettingService)
        {
            this.emailSettingService = emailSettingService;
        }

        [HttpGet]
        public IEnumerable<EmailSetting> Get()
        {
            return emailSettingService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<EmailSetting> Get(int id)
        {
            var emailSetting = emailSettingService.GetById(id);

            if (emailSetting != null)
            {
                return emailSetting;
            }

            return NotFound();
        }

        /// <summary>
        /// Right now just first setting is used.
        /// </summary>
        /// <param name="emailSetting"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<EmailSetting> Post([FromBody] EmailSetting emailSetting)
        {
            emailSettingService.Create(emailSetting);

            return CreatedAtAction(nameof(Get), new { id = emailSetting.Id }, emailSetting);
        }

        [HttpPut("{id}")]
        public ActionResult<EmailSetting> Put([FromBody] EmailSetting emailSetting)
        {
            emailSettingService.Update(emailSetting);

            return CreatedAtAction(nameof(Get), new { id = emailSetting.Id }, emailSetting);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var emailSetting = emailSettingService.GetById(id);

            if (emailSetting == null)
            {
                return NotFound();
            }

            emailSettingService.Delete(emailSetting);

            return Ok();
        }
    }
}
