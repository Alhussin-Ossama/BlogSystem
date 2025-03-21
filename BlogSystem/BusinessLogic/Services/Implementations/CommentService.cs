using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.DataAccess.Specifications.AppSpecification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogSystem.BusinessLogic.Services.Implementations
{
	public class CommentService : ICommentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CommentService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<IEnumerable<CommentDto>> GetCommentsAsync(int PostId)
		{
			var Post = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(PostId);
			if (Post is null) throw new KeyNotFoundException($"Post with ID {PostId} not found.");

			var spec = new CommentSpecification(PostId);
			var Comments = await _unitOfWork.Repository<Comment>().GetAllWithSpecificationAsync(spec);
			if (!Comments.Any()) throw new KeyNotFoundException("No comments found for this post");
			var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(Comments);
			return commentDtos;

		}
		public async Task AddCommentAsync(AddCommentDto comment)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null) throw new UnauthorizedAccessException("User not authenticated");

			var post = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(comment.PostId);
			if (post == null) throw new KeyNotFoundException("Post not found");

			var newComment = new Comment()
			{
				Content = comment.Comment,
				PostId = comment.PostId,
				AuthorId = userId,
				CreatedAt = DateTime.UtcNow,
				ParentCommentId = null
			};
			await _unitOfWork.Repository<Comment>().AddAsync(newComment);

			var Complete = await _unitOfWork.CompleteAsync();
			if (Complete <= 0) throw new ArgumentException("Saving Faild");
		}

		public async Task EditeCommentAsync(int id, string content)
		{
			var Comment = await _unitOfWork.Repository<Comment>().GetByIdAsync(id);
			if (Comment == null) throw new KeyNotFoundException("Comment Not Found");

			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var userRoles = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

			if (Comment.AuthorId != userId && !(userRoles?.Contains("Admin") ?? false))
				throw new UnauthorizedAccessException("You are not allowed to edit this comment.");

			Comment.Content = content;

			_unitOfWork.Repository<Comment>().Update(Comment);

			var Complete = await _unitOfWork.CompleteAsync();
			if (Complete <= 0) throw new ArgumentException("Saving Faild");


		}

		public async Task DeleteCommentAsync(int id)
		{
			var Comment = await _unitOfWork.Repository<Comment>().GetByIdAsync(id);
			if (Comment == null) throw new KeyNotFoundException("Comment Not Found");


			var repliesSpec = new RepliesByParentIdSpecification(id);
			var replies = await _unitOfWork.Repository<Comment>().GetAllWithSpecificationAsync(repliesSpec);

			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var userRoles = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

			if (Comment.AuthorId != userId && !(userRoles?.Contains("Admin") ?? false))
				throw new UnauthorizedAccessException("You are not allowed to edit this comment.");

			foreach (var reply in replies)
			{
				_unitOfWork.Repository<Comment>().Delete(reply);
			}

			_unitOfWork.Repository<Comment>().Delete(Comment);


			var Complete = await _unitOfWork.CompleteAsync();
			if (Complete <= 0) throw new ArgumentException("Saving Faild");

		}

		public async Task AddReplayCommentAsync(ReplayCommentDto comment)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null) throw new UnauthorizedAccessException("User not authenticated");

			var parentComment = await _unitOfWork.Repository<Comment>().GetByIdAsync(comment.ParentCommentId);
			if (parentComment == null) throw new KeyNotFoundException("Parent comment not found");

			var reply = new Comment()
			{
				Content = comment.Content,
				CreatedAt = DateTime.UtcNow,
				AuthorId = userId,
				PostId = parentComment.PostId,
				 ParentCommentId = comment.ParentCommentId
			};

			await _unitOfWork.Repository<Comment>().AddAsync(reply);
			var complete = await _unitOfWork.CompleteAsync();
			if (complete <= 0) throw new ArgumentException("Saving Failed");
		}
	}
}
