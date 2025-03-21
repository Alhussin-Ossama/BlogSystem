using BlogSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Cofigurations
{
	public class BlogPostTagConfiguration : IEntityTypeConfiguration<BlogPostTag>
	{
		public void Configure(EntityTypeBuilder<BlogPostTag> builder)
		{
			builder.HasKey(bt => new { bt.BlogPostId, bt.TagId }); 

			builder.HasOne(bt => bt.BlogPost)
				.WithMany(b => b.BlogPostTags)
				.HasForeignKey(bt => bt.BlogPostId);

			builder.HasOne(bt => bt.Tag)
				.WithMany(t => t.BlogPostTags)
				.HasForeignKey(bt => bt.TagId);
		}
	}
}
