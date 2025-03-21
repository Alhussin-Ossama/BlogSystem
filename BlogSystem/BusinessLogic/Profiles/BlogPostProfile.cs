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
	public class BlogPostProfile : Profile
	{
		public BlogPostProfile()
		{
			CreateMap<BlogPost, BlogPostDto>()
				.ForMember(des => des.AuthorName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName))
				.ForMember(des => des.Category, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(des => des.Tags, opt => opt.MapFrom(src => src.BlogPostTags.Select(bt => bt.Tag.Name)));


			CreateMap<CreatePostDto, BlogPost>()
		   .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
		   .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
		   .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => PostStatus.Draft.ToString()))
		   .ForMember(dest => dest.BlogPostTags, opt => opt.Ignore());
		}
	}
}
