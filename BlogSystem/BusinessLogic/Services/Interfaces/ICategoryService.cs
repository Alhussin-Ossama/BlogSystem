using BlogSystem.BusinessLogic.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
		Task<CategoryDto> GetCategoryByIdAsync(int id);
		Task AddCategoryAsync(string name);
		Task EditCategoryAsync(int id, string name);
		Task DeleteCategoryAsync(int id);
	}
}
