using BlogSystem.BusinessLogic.Services.Implementations;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Context;
using BlogSystem.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogSystem.API.Extensions
{
	public static class IdentityExtension
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection Services, IConfiguration configuration)
		{
			Services.AddIdentity<User, IdentityRole>()
							.AddEntityFrameworkStores<BlogContext>();
			//.AddDefaultTokenProviders();

			Services.AddScoped<ITokenService, TokenService>();
			Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:Issuer"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:Audience"],
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
				};
			});

			Services.AddAuthorization(options =>
			{
				options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
				options.AddPolicy("RequireEditorRole", policy => policy.RequireRole("Editor"));
				options.AddPolicy("RequireReaderRole", policy => policy.RequireRole("Reader"));
			});
			return Services;
		}
	}
}
