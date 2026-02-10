using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
