using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.API.ErrorHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BlogSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagController : ControllerBase
	{
		private readonly ITagService _tagService;

		public TagController(ITagService tagService)
		{
			_tagService = tagService;
		}

		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
		{
			var Tags = await _tagService.GetAllTagsAsync();
			return Ok(Tags);
		}

		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpGet("{id}")]
		public async Task<ActionResult<TagDto>> GetTagById(int id)
		{
			var Tag = await _tagService.GetTagByIdAsync(id);
			return Ok(Tag);
		}


		[Authorize(Roles = "Admin,Editor")]
		[HttpPost]
		public async Task<ActionResult> AddTag(string name)
		{
			await _tagService.AddTagAsync(name);
			return Created("Tag Added", new ApiRespons(201, "Tag Updated successfully"));
		}


		[Authorize(Roles = "Admin,Editor")]
		[HttpPut]
		public async Task<ActionResult> EditTag(int id, string name)
		{
			await _tagService.EditTagAsync(id, name);
			return Created("Tag Updated", new ApiRespons(201, "Tag Updated successfully"));
		}



		[Authorize(Roles = "Admin")]
		[HttpDelete]
		public async Task<ActionResult> DeleteTag(int id)
		{
			await _tagService.DeleteTagAsync(id);
			return NoContent();
		}
	}
}
