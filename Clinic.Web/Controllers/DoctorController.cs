﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    public class DoctorController : DefaultController
    {
        // GET: Doctor

        [Authorize(Roles = "Doctor")]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}