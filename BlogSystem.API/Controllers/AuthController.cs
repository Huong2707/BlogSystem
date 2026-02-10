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
                _logger.LogWarning("Login failed for email: {Email} . Error:{Error}",request.Email,ex.Message.ToString());
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

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> Register(RegisterRequest request)
        {
            try
            {
                _logger.LogInformation("create an account with email:{Email}",request.Email);
                var user=await _authService.RegisterAsync(request);
                _logger.LogInformation("Register successful for user ID :{UserID}",user.UserId);
                return Ok(new {
                    success= true,
                    data = user,
                    message = "Register successful"
                }
                    );
            }
            catch(ArgumentException ex)
            {
                _logger.LogWarning("Register failed for email:{Email}- error:{Error}",request.Email,ex.Message.ToString());
                return BadRequest(new {
                    success = false,
                    info="loi tham so",
                    message =ex.Message.ToString()
                });


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email: {Email}", request.Email);
                return StatusCode(500, new 
                {
                    success=false,
                    message=ex.Message.ToString(),
                    info="loi khong the tao duoc tai khoan"
                });
            }


        }

    }
}
