using BlogSystem.BusinessLogic.DTOs.Accounts;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ITokenService _tokenService;

		public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}


		public async Task<UserDto> RegisterAsync(RegisterDto userDro)
		{

			if (await _userManager.FindByEmailAsync(userDro.Email) is not null)
				throw new ArgumentException("Email is already in use.");


			if (await _userManager.FindByNameAsync(userDro.Username) is not null)
				throw new ArgumentException("Username is already taken.");

			var User = new User()
			{
				UserName = userDro.Username,
				FirstName = userDro.FirstName,
				LastName = userDro.LastName,
				Email = userDro.Email,
				PhoneNumber = userDro.PhoneNumber,
				Role = "Reader"
			};

			var Checking = await _userManager.CreateAsync(User, userDro.Password);
			if (!Checking.Succeeded) throw new Exception("User registration failed.");

			const string defaultRole = "Reader";
			if (!await _userManager.IsInRoleAsync(User, defaultRole))
			{
				await _userManager.AddToRoleAsync(User, defaultRole);
			}

			var ReturnedUser = new UserDto()
			{
				Username = userDro.Username,
				Email = userDro.Email,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			};

			return ReturnedUser;
		}
		public async Task<UserDto> LoginAsync(LoginDto loginDto)
		{
			var User = await _userManager.FindByEmailAsync(loginDto.Email);
			if (User is null) throw new UnauthorizedAccessException("Invalid email or password.");

			var Checking = await _signInManager.CheckPasswordSignInAsync(User, loginDto.Password, false);
			if (!Checking.Succeeded) throw new UnauthorizedAccessException("Invalid email or password.");

			var ReturnedUser = new UserDto()
			{
				Username = User.UserName,
				Email = User.Email,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			};
			return ReturnedUser;
		}
	}
}
