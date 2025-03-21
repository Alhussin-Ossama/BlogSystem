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
	internal class TagProfile:Profile
	{
        public TagProfile()
        {
			CreateMap<Tag, TagDto>();
		}
    }
}
