using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    public class AppointmentController : DefaultController
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult BookAppointment()
        {
            return View();
        }


        [Authorize(Roles = "Patient")]
        public ActionResult Checkout()
        {
            return View();
        }

    }
}