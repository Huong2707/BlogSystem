using BlogSystem.Application.DTOs;
using BlogSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public  AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            return Ok(new
            {
                success = true,
                data = request,
                message = "Login successful"
            });
            try
            {
                _logger.LogInformation("Login attempt for email: {Email}",request.Email);
                var response = await _authService.LoginAsync(request);
                _logger.LogInformation("Login successful for user ID: {UserId}", response.User.UserId);
                return Ok(new 
                {
                    success=true,
                    data=response,
                    message= "Login successful"
                });

            }
            catch(UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Login failed for email: {Email} . Error:{Error",request.Email,ex.Message.ToString());
                return Unauthorized(new 
                {
                    success=false,

                    message = ex.Message.ToString()
                });
                }
            catch(ArgumentException ex)
            {
                _logger.LogWarning("Invalid login request:{ Error}",ex.Message.ToString());
                return BadRequest(new 
                {
                    success=false,
                    message=ex.Message.ToString()
                });
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "Error during login for email: {Email}", request.Email);
                return StatusCode(500, new 
                {
                    success=false,
                    message=ex.Message.ToString()
                });
            }
        }
    }
}
