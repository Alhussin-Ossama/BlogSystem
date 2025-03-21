
using BlogSystem.API.Extensions;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.BusinessLogic.Profiles;
using BlogSystem.BusinessLogic.Services.Implementations;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Context;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.DataAccess.Interfaces.AppInterface;
using BlogSystem.DataAccess.Repositories;
using BlogSystem.DataAccess.Repositories.AppRepository;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BlogSystem.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();


			builder.Services.AddDbContext<BlogContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			#region Swagger
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "Blog System API",
					Version = "v1",
					Description = "API Documentation for the Blog System project"
				});
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
					new OpenApiSecurityScheme
					{
					Reference = new OpenApiReference
					{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
					}
					},
					new string[] {}
					}
					});
			});
			#endregion
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			builder.Services.AddHttpContextAccessor();

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IAccountService, AccountService>();
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<ITagService, TagService>();
			builder.Services.AddScoped<ICommentService, CommentService>();
			builder.Services.AddScoped<IPostService, PostService>();
			builder.Services.AddScoped<IPostRepository,PostRepository>();

			builder.Services.AddIdentityService(builder.Configuration);
			builder.Services.AddAutoMapper(typeof(BlogPostProfile));


			var app = builder.Build();

			#region Update Database
			using var Scope = app.Services.CreateScope();
			var Services = Scope.ServiceProvider;
			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				var IdentityDbContext = Services.GetRequiredService<BlogContext>();
				await IdentityDbContext.Database.MigrateAsync();

				var userManager = Services.GetRequiredService<UserManager<User>>();
				var roleManager = Services.GetRequiredService<RoleManager<IdentityRole>>();
				await BlogContextSeed.SeedRolesAndAdminAsync(userManager, roleManager);

			} 
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "An Error During Appling The Migration");
			}
			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();
			app.UseMiddleware<ExceptionMiddleware>();
			
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
