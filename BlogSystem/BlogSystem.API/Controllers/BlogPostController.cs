using AutoMapper;
using BlogSystem.API.ErrorHandler;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.DataAccess.Specifications.AppSpecification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BlogSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostController : ControllerBase
	{
		private readonly IPostService _postService;

		public BlogPostController(IPostService postService)
		{
			_postService = postService;
		}



		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetAllPosts()
		{
			var Posts = await _postService.GetAllPostsAsync();
			return Ok(Posts);
		}



		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpGet("{id}")]
		public async Task<ActionResult<BlogPostDto>> GetPostById(int id)
		{
			var Post = await _postService.GetPostByIdAsync(id);
			return Ok(Post);
		}



		[Authorize(Roles = "Admin")]
		[HttpGet("FindByStatus")]
		public async Task<ActionResult<BlogPostDto>> GetPostByStatus(string status)
		{
			var Post = await _postService.GetPostByStatusAsync(status);
			return Ok(Post);
		}



		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpGet("Search")]
		public async Task<ActionResult<BlogPostDto>> Search(string? title, string? category, string? tag)
		{
			var Post = await _postService.SearchAsync(title, category, tag);
			return Ok(Post);
		}



		[Authorize(Roles = "Admin,Editor")]
		[HttpPost]
		public async Task<ActionResult> CreatePost(CreatePostDto postDto)
		{
			await _postService.CreatePostAsync(postDto);
			return Created("Post Created", new ApiRespons(201, "Post Created successfully"));
		}



		[Authorize(Roles = "Admin,Editor")]
		[HttpPut]
		public async Task<ActionResult> UpdatePost(int PostId, UpdatePostDto updatePostDto)
		{
			await _postService.UpdatePostAsync(PostId, updatePostDto);
			return Created("Post Updated", new ApiRespons(201, "Post Updated successfully"));
		}



		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeletePost(int id)
		{
			await _postService.DeletePostAsync(id);
			return NoContent();
		}



	}
}
