using BlogSystem.BusinessLogic.DTOs.Accounts;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using BlogSystem.API.ErrorHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto user)
		{
			var UserRegistered =  await _accountService.RegisterAsync(user);
			return Ok(UserRegistered);
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var UserLogged = await _accountService.LoginAsync(loginDto);
			return Ok(UserLogged);
		}
	}
}
