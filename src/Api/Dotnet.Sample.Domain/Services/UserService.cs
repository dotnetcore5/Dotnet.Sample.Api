using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Dotnet.Sample.Domain.Models;

namespace Dotnet.Sample.Domain.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(string username, string password);

        User GetById(int userId);
    }

    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;

        public UserService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public AuthenticateResponse Authenticate(string username, string password)
        {
            var user = User.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return new AuthenticateResponse { Id = user.Id, Token = GenerateJWTToken(user), Name = user.Name, Role = user.Role, Username = user.Username };
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
                claims: new[] {
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