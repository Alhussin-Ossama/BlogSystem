using BlogSystem.BusinessLogic.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Services.Interfaces
{
	public interface ICommentService
	{
		Task<IEnumerable<CommentDto>> GetCommentsAsync(int id);
		Task AddCommentAsync(AddCommentDto comment);
		Task EditeCommentAsync(int id, string content);
		Task AddReplayCommentAsync(ReplayCommentDto comment);
		Task DeleteCommentAsync(int id);
	}
}
