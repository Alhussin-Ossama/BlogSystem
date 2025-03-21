using BlogSystem.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Context
{
	public static class BlogContextSeed
	{
		public static async Task SeedRolesAndAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{

			var roles = new[] { "Admin", "Editor", "Reader" };
			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}


			var adminEmail = "AlhussinOssama@gmail.com";
			var adminUser = await userManager.FindByEmailAsync(adminEmail);

			if (adminUser == null)
			{
				var newAdmin = new User
				{
					FirstName = "Alhussin",
					LastName = "Osama",
					UserName = "AlhussinOssama",
					Email = adminEmail,
					PhoneNumber = "01280778098",
					Role = "Admin",
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(newAdmin, "Pa$$w0rd");

				if (result.Succeeded)
				{

					await userManager.AddToRoleAsync(newAdmin, newAdmin.Role);


					var rolesAssigned = await userManager.GetRolesAsync(newAdmin);
					if (!rolesAssigned.Contains("Admin"))
					{
						throw new System.Exception("Failed to assign Admin role.");
					}
				}
			}
		}
	}
}
