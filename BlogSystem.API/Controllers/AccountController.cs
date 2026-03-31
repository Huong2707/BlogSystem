using BlogSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogSystem.API.Controllers
{
    [Authorize]//phai login moi duoc truy cap vao controller nay
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        [HttpGet("test-auth")]
        public IActionResult TestAuth()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            _logger.LogInformation("TestAuth called by user ID : {UserID} - Email:{Email}", userId, userEmail);
            return Ok(new
            {
                success = true,
                userId = userId,
                userEmail = userEmail,
                message = $"Wellcome authenticated user - {userId} :{userEmail}",
                timestamp = DateTime.UtcNow
            });

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("test-admin")]
        public IActionResult TestAdmin()
        {
            return Ok(new
            {
                success = true,

                message = "Wellcome Admin to my website ",
                timestamp = DateTime.UtcNow
            });
        }


    }
}
