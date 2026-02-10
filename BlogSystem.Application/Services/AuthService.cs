using AutoMapper;
using BlogSystem.Application.DTOs;
using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BlogSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }
        public async Task<UserDto> GetCurrentUserAsync(string token)
        {
            var validationResponse = await ValidateTokenAsync(token);
            if (!validationResponse.IsValid)
            {
                throw new UnauthorizedAccessException("Invalid Token or Expried Token");
            }
            else
            {
                return validationResponse.User;
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if(string.IsNullOrEmpty(request.Email)|| string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentException("Email and Password is required");

            }
            var user= await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Email or password invalid");

            }
            if(!user.PasswordHash.Equals(request.Password))
            {
                throw new UnauthorizedAccessException("Email or password invalid");
            }
            // sau nay se xu ly phan ma hoa MK
            if(user.IsActive ==0)
            {
                throw new UnauthorizedAccessException("Account is blocked");
            }
            //tao token dua tren thong tin tai khoan nguoi dung
            //tra du lieu nguoi dung, cac thong tin nhay cam ma hoa dua vao token
            var token = GenerateJwtToken(user);
            var roles = user.UserRole?.Select(ur=>ur.Role.RoleName).ToList() ?? new List<string>();
            return new LoginResponse
            { 
                Token=token,
                Expiration=  DateTime.UtcNow.AddHours(Convert.ToDouble(_config["Jwt:ExpireHours"])),
                User = _mapper.Map<UserDto>(user),
                Roles = roles
            };


        }

        public async Task<UserDto> RegisterAsync(RegisterRequest request)
        {
           if(string.IsNullOrWhiteSpace(request.FullName))
            {
                throw new ArgumentException("FullName is required");
            }
           if(string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email is requested");
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Password is requested");
            }
            if(request.Password != request.ConfirmPassword)
            {
                               throw new ArgumentException("Password do not match");
            }
            bool checkEamil= await _userRepository.EmailExistsAsync(request.Email);
            if(checkEamil)
            {
                throw new ArgumentException("Email already registered");
            }
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Bio=request.Bio?.Trim(),
                CreatedAt= DateTime.UtcNow,
                IsActive= 1

            };
            await _userRepository.AddAsync(user);//insert table users
            await _userRepository.AssignDefaultRoleAsync(user.UserId);//userRole
            var userWithRole=await _userRepository.GetUserWithRoleAsync(user.UserId);
            return _mapper.Map<UserDto>(userWithRole);
        }

        public async Task<ValidateTokenResponse> ValidateTokenAsync(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return new ValidateTokenResponse { IsValid = false };
                }
                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = token.Substring(7);
                }
                var tokenHandle = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                tokenHandle.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey =true,
                    IssuerSigningKey= new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero


                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value); 
                var user= await _userRepository.GetUserWithRoleAsync(userId);
                if(user == null)
                {
                    return new ValidateTokenResponse { IsValid = false };
                }
                return new ValidateTokenResponse
                {
                    IsValid = true,
                    User = _mapper.Map<UserDto>(user)
                };
            }
            catch
            {
                return new ValidateTokenResponse { IsValid = false };
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key =Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if(user.UserRole != null)
            {
                foreach(var role in user.UserRole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
                }
            }
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_config["Jwt:ExpireHours"])),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            }; 
            var token = tokenHandler.CreateToken(tokenDesc);
                return tokenHandler.WriteToken(token);
        }
    }
}
