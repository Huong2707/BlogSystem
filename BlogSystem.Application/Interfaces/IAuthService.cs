using BlogSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Interfaces
{
    public  interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<UserDto> RegisterAsync(RegisterRequest request);
        Task<UserDto> GetCurrentUserAsync(string token);
        Task<ValidateTokenResponse> ValidateTokenAsync(string token);
    }
}
