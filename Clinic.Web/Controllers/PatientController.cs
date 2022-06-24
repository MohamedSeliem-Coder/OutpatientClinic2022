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
            int PatientId = _patientBLL.GetPatientId(UserID);

            var myAppointments = _bookingBLL.Get_Booking_List(null, null, null, null, null, null, PatientId, null);

            ViewBag.Status = _comonBLL.Get_BookingStatuses_Data(null);

            return View(myAppointments);
        }

        public PartialViewResult GetBooking(byte? BookingStatusId)
        {
            int PatientId = _patientBLL.GetPatientId(UserID);
            var myAppointments = _bookingBLL.Get_Booking_List(null, null, null, BookingStatusId, null, null, PatientId, null);
            return PartialView("_PatientBookingList", myAppointments);
        }
    }
}