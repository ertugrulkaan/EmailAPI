using EmailAPI.Core.Model;
using EmailAPI.Models;
using EmailAPI.Models.Identity;
using EmailAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IConfiguration configuration)
        {
            _logger = logger;
            this.accountService = accountService;
            this.configuration = configuration;
        }
        /// <summary>
        /// Login with your username and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            ApplicationUser appUser;

            if (!ModelState.IsValid
                || model == null
                || (appUser = await accountService.ValidateUserAsync(model.UserName, model.Password)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var token = await accountService.GenerateTokenAsync(appUser, configuration["JwtBearerTokenSettings:Key"], configuration["JwtBearerTokenSettings:Issuer"]);
            _logger.LogInformation("User Logged with token: " + token);
            return new OkObjectResult(new
            {
                BearerToken = token
            });
        }
        /// <summary>
        /// Only admins can create new users
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            ApplicationUser appUser;

            if (!ModelState.IsValid
                || model == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }
            var result = await accountService.RegisterAsync(model);

            _logger.LogInformation("Register Event: " + result);

            return new ObjectResult(new
            {
                Result = result
            });
        }
        /// <summary>
        /// Change password with your username and email address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            ApplicationUser user;
            if ((user = await accountService.GetUserAsync(model.UserName)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid Username." });
            }
            if ((user = await accountService.GetUserByEmailAsync(model.EmailAddress)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Invalid Email." });
            }

            var result = await accountService.ResetUserPassword(user, model.Password);

            _logger.LogInformation("Password Reset Event, " + (result.Succeeded ? model.UserName + " has changed password." : "error occured, model: " + JsonConvert.SerializeObject(model)));

            return Ok(result.Succeeded);
        }
    }
}
