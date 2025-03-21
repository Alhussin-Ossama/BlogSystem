using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Implementations
{
	public class TagService : ITagService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public TagService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
		{
			var Tags = await _unitOfWork.Repository<Tag>().GetAllAsync();
			if (!Tags.Any()) throw new KeyNotFoundException();
			var MappedTags = _mapper.Map<IEnumerable<TagDto>>(Tags);
			return MappedTags;
		}
		public async Task<TagDto> GetTagByIdAsync(int id)
		{
			var Tag = await _unitOfWork.Repository<Tag>().GetByIdAsync(id);
			if (Tag == null)
			{
				throw new KeyNotFoundException($"Tag with ID {id} not found.");
			}
			var MappedTag = _mapper.Map<TagDto>(Tag);
			return MappedTag;
		}
		public async Task AddTagAsync(string name)
		{
			var MappedTag = new Tag()
			{
				Name = name
			};
			await _unitOfWork.Repository<Tag>().AddAsync(MappedTag);
			int Complete = await _unitOfWork.CompleteAsync();

			if (Complete <= 0) throw new ArgumentException("Saving Faild");
		}
		public async Task EditTagAsync(int id, string name)
		{
			var Tag = await _unitOfWork.Repository<Tag>().GetByIdAsync(id);
			if (Tag == null) throw new KeyNotFoundException($"Tag with ID {id} not found.");

			Tag.Name = name;

			_unitOfWork.Repository<Tag>().Update(Tag);
			int Complete = await _unitOfWork.CompleteAsync();
			if (Complete <= 0) throw new ArgumentException("Saving Faild");
		}
		public async Task DeleteTagAsync(int id)
		{
			var Tag = await _unitOfWork.Repository<Tag>().GetByIdAsync(id);
			if (Tag == null) throw new KeyNotFoundException($"Tag with ID {id} not found.");
			_unitOfWork.Repository<Tag>().Delete(Tag);
			int Complete = await _unitOfWork.CompleteAsync();
			if (Complete <= 0) throw new ArgumentException("Saving Faild");
		}
	}
}
