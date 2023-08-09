using DYNAMIC_BOX_Shared;
using DYNAMIC_BOX_WebAPI.Helper;
using DYNAMIC_BOX_WebAPI.Models;
using DYNAMIC_BOX_WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DYNAMIC_BOX_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;



        public AuthController(IUserService userSservice, IEmailService emailService)
        {
            _userService = userSservice;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);

            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPost("Login")]

        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            MailRequest mailRequest = new MailRequest();
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.IsSuccess)
                    mailRequest.ToEmail = "forucov70@gmail.com";
                mailRequest.Subject = "Welcome to Fuad";
                mailRequest.Body = GetHtmlcontent();
                await _emailService.SendEmailAsync(mailRequest);
                return Ok();

                return Ok(result);

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
        private string GetHtmlcontent()
        {
            string response = "<h1>Thanks for subscribing us</h1>";
            return response;
        }



        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

    }
}