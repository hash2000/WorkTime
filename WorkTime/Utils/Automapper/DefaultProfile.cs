using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Automapper
{
    public class DefaultProfile : Profile
    {
        public const string PROFILE_NAME = "Default";
        public override string ProfileName
        {
            get
            {
                return PROFILE_NAME;
            }
        }

        protected override void Configure()
        {
            //  CreateMap<EntityType, DtoType>();
        }
    }
}