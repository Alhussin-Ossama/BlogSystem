using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Interfaces
{
	public interface IPostService
	{
		Task<IEnumerable<BlogPostDto>> GetAllPostsAsync();
		Task<BlogPostDto> GetPostByIdAsync(int id);
		Task<BlogPostDto> GetPostByStatusAsync(string status);
		Task<BlogPostDto> SearchAsync(string title, string category, string tag);
		Task CreatePostAsync(CreatePostDto postDto);
		Task UpdatePostAsync(int PostId, UpdatePostDto updatePostDto);
		Task DeletePostAsync(int postId);
	}
}
