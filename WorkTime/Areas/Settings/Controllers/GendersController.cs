﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTime.Models;
using WorkTime.Utils.ExtJs;
using WorkTime.Utils.Stores;


namespace WorkTime.Areas.Settings.Controllers
{
    public class GendersController : DtoStore<Gender, ContributorsEntities>
    {
    }
}