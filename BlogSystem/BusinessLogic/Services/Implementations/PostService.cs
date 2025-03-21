using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.BusinessLogic.Services.Interfaces;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.DataAccess.Interfaces.AppInterface;
using BlogSystem.DataAccess.Specifications.AppSpecification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Implementations
{
	public class PostService : IPostService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public PostService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IEnumerable<BlogPostDto>> GetAllPostsAsync()
		{
			var spec = new BlogPostSpecification();
			var Posts = await _unitOfWork.Repository<BlogPost>().GetAllWithSpecificationAsync(spec);
			if (!Posts.Any()) throw new KeyNotFoundException("No Posts Found");
			var MappedPost = _mapper.Map<IEnumerable<BlogPostDto>>(Posts);
			return MappedPost;
		}

		public async Task<BlogPostDto> GetPostByIdAsync(int id)
		{
			var spec = new BlogPostSpecification(id);
			var Post = await _unitOfWork.Repository<BlogPost>().SearchWithSpecificationAsync(spec);
			if (Post == null) throw new KeyNotFoundException($"Post with ID {id} not found.");
			var MappedPost = _mapper.Map<BlogPostDto>(Post);
			return MappedPost;
		}

		public async Task<BlogPostDto> GetPostByStatusAsync(string status)
		{
			var spec = new BlogPostSpecification(status);
			var Post = await _unitOfWork.Repository<BlogPost>().SearchWithSpecificationAsync(spec);
			if (Post == null) throw new KeyNotFoundException($"Post with Status not found.");
			var MappedPost = _mapper.Map<BlogPostDto>(Post);
			return MappedPost;
		}

		public async Task CreatePostAsync(CreatePostDto postDto)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null) throw new UnauthorizedAccessException("User not authenticated");


			var category = await _unitOfWork.Repository<Category>().GetByIdAsync(postDto.CategoryId);
			if (category == null) throw new KeyNotFoundException("Category not found");

			var tags = await _unitOfWork.Repository<Tag>().GetAllAsync();
			var validTags = tags.Where(t => postDto.TagIds.Contains(t.Id)).ToList();
			if (validTags.Count != postDto.TagIds.Count) throw new ArgumentException("Some tags are invalid");

			var newPost = _mapper.Map<BlogPost>(postDto);
			newPost.AuthorId = userId;
			newPost.CategoryId = postDto.CategoryId;
			newPost.BlogPostTags = validTags.Select(tag => new BlogPostTag { TagId = tag.Id }).ToList();

			await _unitOfWork.Repository<BlogPost>().AddAsync(newPost);
			var complete = await _unitOfWork.CompleteAsync();
			if (complete <= 0) throw new ArgumentException("Saving Failed");
		}

		public async Task UpdatePostAsync(int PostId, UpdatePostDto updatePostDto)
		{
			var post = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(PostId);
			if (post == null) throw new KeyNotFoundException("Post not found");

			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null) throw new UnauthorizedAccessException("User not authenticated");


			var category = await _unitOfWork.Repository<Category>().GetByIdAsync(updatePostDto.CategoryId);
			if (category == null) throw new KeyNotFoundException("Category not found");


			post.Title = updatePostDto.Title;
			post.Content = updatePostDto.Content;
			post.CategoryId = updatePostDto.CategoryId;
			post.Status = updatePostDto.Status.ToString();
			post.UpdatedAt = DateTime.UtcNow;


			var complete = await _unitOfWork.CompleteAsync();
			if (complete <= 0) throw new ArgumentException("Saving Failed");
		}

		public async Task DeletePostAsync(int postId)
		{
			var post = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(postId);

			if (post == null) throw new KeyNotFoundException("Post not found");

			_unitOfWork.Repository<BlogPost>().Delete(post);
			await _unitOfWork.CompleteAsync();
		}

		public async Task<BlogPostDto> SearchAsync(string? title, string? category, string? tag)
		{
			var spec = new BlogPostSpecification(title, category, tag);
			var Post = await _unitOfWork.Repository<BlogPost>().SearchWithSpecificationAsync(spec);
			if (Post == null) throw new KeyNotFoundException($"Post with Title or Catrgory or Tag not found.");
			var MappedPost = _mapper.Map<BlogPostDto>(Post);
			return MappedPost;
		}
	}
}
