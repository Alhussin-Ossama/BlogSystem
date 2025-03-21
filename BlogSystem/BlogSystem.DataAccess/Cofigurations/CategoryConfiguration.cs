﻿using BlogSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Cofigurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(100); 

			builder.HasMany(c => c.BlogPosts)
				.WithOne(b => b.Category)
				.HasForeignKey(b => b.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
