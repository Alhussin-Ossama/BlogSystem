using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Implementations
{
	public class CategoryService : ICategoryService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
		{
			var Categories = await _unitOfWork.Repository<Category>().GetAllAsync();
			if (!Categories.Any()) throw new KeyNotFoundException("No Categories Found");
			var MappedCategories = _mapper.Map<IEnumerable<CategoryDto>>(Categories);
			return MappedCategories;
		}


		public async Task<CategoryDto> GetCategoryByIdAsync(int id)
		{
			var Category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
			if (Category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
			var MappedCategory = _mapper.Map<CategoryDto>(Category);
			return MappedCategory;
		}


		public async Task AddCategoryAsync(string name)
		{
			var MappedCategory = new Category()
			{
				Name = name
			};
			await _unitOfWork.Repository<Category>().AddAsync(MappedCategory);
			int Complete = await _unitOfWork.CompleteAsync();

			if (Complete <= 0) throw new ArgumentException("Saving Faild");

		}


		public async Task EditCategoryAsync(int id, string name)
		{
			var Category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
			if (Category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
			Category.Name = name;
			_unitOfWork.Repository<Category>().Update(Category);
			int Complete = await _unitOfWork.CompleteAsync();
			if (Complete <= 0) throw new ArgumentException("Saving Faild");
		}


		public async Task DeleteCategoryAsync(int id)
		{
			var Category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
			if (Category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
			_unitOfWork.Repository<Category>().Delete(Category);
			int Complete = await _unitOfWork.CompleteAsync();
			if (Complete <= 0) throw new ArgumentException("Saving Faild");
		}
	}
}
