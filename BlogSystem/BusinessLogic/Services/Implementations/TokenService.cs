using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Implementations
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> CreateTokenAsync(User user, UserManager<User> userManager)
		{
			var AuthClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.GivenName,user.FirstName+" "+user.LastName),
				new Claim(ClaimTypes.Email,user.Email),
			};

			var UserRole = await userManager.GetRolesAsync(user);
			foreach (var Role in UserRole)
			{
				AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
			}

			var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

			var Token = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				expires: DateTime.Now.AddHours(double.Parse(_configuration["JWT:ExpireDate"])),
				claims: AuthClaims,
				signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
				);
			return new JwtSecurityTokenHandler().WriteToken(Token);
		}
	}
}
