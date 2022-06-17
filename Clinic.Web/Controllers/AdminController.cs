using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    public class AdminController : DefaultController
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}