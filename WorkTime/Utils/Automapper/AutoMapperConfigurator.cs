using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTime.Utils.Automapper
{
    public class AutoMapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<DefaultProfile>());
        }
    }
}