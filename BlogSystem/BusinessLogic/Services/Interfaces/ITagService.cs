using BlogSystem.BusinessLogic.DTOs.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Interfaces
{
	public interface ITagService
	{
		Task<IEnumerable<TagDto>> GetAllTagsAsync();
		Task<TagDto> GetTagByIdAsync(int id);
		Task AddTagAsync(string name);
		Task EditTagAsync(int id, string name);
		Task DeleteTagAsync(int id);
	}
}
