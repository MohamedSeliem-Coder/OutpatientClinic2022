using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    public class PatientController : DefaultController
    {
        // GET: Patient
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}