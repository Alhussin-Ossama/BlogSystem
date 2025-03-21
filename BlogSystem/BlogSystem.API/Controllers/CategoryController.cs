using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.API.ErrorHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogSystem.BusinessLogic.Services.Interfaces;

namespace BlogSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
		{
			var Categories = await _categoryService.GetAllCategoryAsync();
			return Ok(Categories);
		}

		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
		{
			var Category = await _categoryService.GetCategoryByIdAsync(id);
			return Ok(Category);
		}


		[Authorize(Roles = "Admin,Editor")]
		[HttpPost]
		public async Task<ActionResult> AddCategory(string name)
		{
			await _categoryService.AddCategoryAsync(name);
			return Created("Category Added", new ApiRespons(201, "Category Added successfully"));
		}

		[Authorize(Roles = "Admin,Editor")]
		[HttpPut]
		public async Task<ActionResult> EditCategory(int id, string name)
		{
			await _categoryService.EditCategoryAsync(id, name);
			return Created("Category Updated", new ApiRespons(201, "Category Updated successfully"));
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete]
		public async Task<ActionResult> DeleteCategory(int id)
		{
			await _categoryService.DeleteCategoryAsync(id);
			return NoContent();
		}
	}
}
