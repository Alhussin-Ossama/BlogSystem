using AutoMapper;
using BlogSystem.BusinessLogic.DTOs.Application;
using BlogSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.Profiles
{
	public class CommentProfile : Profile
	{
		public CommentProfile()
		{
			CreateMap<Comment, CommentDto>()
		  .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName))
		  .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies));

			CreateMap<ReplayCommentDto, Comment>();
		}
	}
}
