using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.API.ErrorHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BlogSystem.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlogSystem.API.Controllers
{
	[Authorize(Roles = "Admin,Editor,Reader")]
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(int PostId)
		{
			var Comments = await _commentService.GetCommentsAsync(PostId);
			return Ok(Comments);
		}


		[HttpPost]
		public async Task<ActionResult<AddCommentDto>> AddComment(AddCommentDto addCommentDto)
		{
			await _commentService.AddCommentAsync(addCommentDto);
			return Created("Comment Added", new ApiRespons(201, "Comment Added successfully"));
		}


		[HttpPut]
		public async Task<ActionResult>  EditeCommentAsync(int id, string content)
		{
			await _commentService.EditeCommentAsync(id, content);
			return Created("Comment Edited", new ApiRespons(201, "Comment Edited successfully"));
		}

		[Authorize(Roles = "Admin,Editor,Reader")]
		[HttpPost("reply")]
		public async Task<ActionResult> AddReply(ReplayCommentDto replayCommentDto)
		{
			await _commentService.AddReplayCommentAsync(replayCommentDto);
			return Created("Reply Added", new ApiRespons(201, "Reply Added successfully"));
		}


		[HttpDelete]
		public async Task<ActionResult> DeleteCommentAsync(int id)
		{
			await _commentService.DeleteCommentAsync(id);
			return NoContent();
		}
	}
}
