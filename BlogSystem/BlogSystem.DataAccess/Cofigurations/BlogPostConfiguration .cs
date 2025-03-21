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
	public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
	{
		public void Configure(EntityTypeBuilder<BlogPost> builder)
		{

			builder.Property(b => b.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(b => b.Content)
				.IsRequired();

			builder.Property(b => b.Status)
				.HasDefaultValue("Draft"); 

			
			builder.HasOne(b => b.Author)
				.WithMany(u => u.BlogPosts)
				.HasForeignKey(b => b.AuthorId)
				.OnDelete(DeleteBehavior.Cascade); 

			
			builder.HasOne(b => b.Category)
				.WithMany(c => c.BlogPosts)
				.HasForeignKey(b => b.CategoryId);
		}
	}
}
