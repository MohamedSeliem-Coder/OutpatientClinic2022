using System;
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


        [Authorize(Roles = "Doctor")]
        public ActionResult Appointments()
        {
            int doctorId = _doctorBLL.GetDoctorId(UserID);

            var myAppointment = _bookingBLL.Get_Booking_List(null, null, null, null, null, doctorId, null, null);
            return View(myAppointment);
        }
    }
}