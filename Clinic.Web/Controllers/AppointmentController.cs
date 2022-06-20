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

       // [Authorize(Roles = "Patient")]
        public ActionResult BookAppointment(int id)
        {
            var doctor = _doctorBLL.Get_Doctor_ById(id);

            ViewBag.Doctor = doctor;

           // int patientId = _patientBLL.GetPatientId(UserID);
           // var patient = _patientBLL.Get_Patient_ById(patientId);

            return View();
        }


       // [Authorize(Roles = "Patient")]
        public ActionResult Checkout()
        {
            return View();
        }

    }
}