using BlogSystem.BusinessLogic.DTOs.Accounts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Interfaces
{
	public interface IAccountService
	{
		Task<UserDto> RegisterAsync(RegisterDto userDto);
		Task<UserDto> LoginAsync(LoginDto loginDto);
	}
}
