using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xero.Demo.Api.Xero.Demo.Domain.Models;

namespace Xero.Demo.Api.Xero.Demo.Domain.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate();

        User GetById(int userId);
    }

    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;

        public UserService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public AuthenticateResponse Authenticate()
        {
            var user = User.Users.First();

            return new AuthenticateResponse(user, GenerateJWTToken(user), user.Role);
        }

        public User GetById(int userId)
        {
            return User.Users.FirstOrDefault(u => u.Id == userId);
        }

        private string GenerateJWTToken(User userInfo)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: new[]
                        {
                            new Claim("id", userInfo.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                            new Claim("Name", userInfo.Name+userInfo.Name.ToString()),
                            new Claim(ClaimTypes.Role,userInfo.Role),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}