using BlogSystem.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Specifications.AppSpecification
{
	public class BlogPostSpecification : BaseSpecifications<BlogPost>
	{
		public BlogPostSpecification() : base()
		{
			Includes.Add(B => B.Author);
			Includes.Add(B => B.Category);
			Includes.Add(B => B.BlogPostTags);
			IncludeStrings.Add("BlogPostTags.Tag");
		}
		public BlogPostSpecification(int id) : base(B => B.Id == id)
		{
			Includes.Add(B => B.Author);
			Includes.Add(B => B.Category);
			Includes.Add(B => B.BlogPostTags);
			IncludeStrings.Add("BlogPostTags.Tag");
		}

		public BlogPostSpecification(string status) : base(B => B.Status == status)
		{
			{
				Includes.Add(B => B.Author);
				Includes.Add(B => B.Category);
				Includes.Add(B => B.BlogPostTags);
				IncludeStrings.Add("BlogPostTags.Tag");
			}
		}
		public BlogPostSpecification(string? title, string? category, string? tag) : base(B =>
		(title.IsNullOrEmpty() || B.Title == title)
		&&
		(category.IsNullOrEmpty() || B.Category.Name == category)
		&&
		(tag.IsNullOrEmpty() || B.BlogPostTags.Select(t => t.Tag.Name).Contains(tag))
		)
		{
			Includes.Add(p => p.Author);
			Includes.Add(p => p.Category);
			Includes.Add(p => p.BlogPostTags);
			IncludeStrings.Add("BlogPostTags.Tag");
		}
	}
}
