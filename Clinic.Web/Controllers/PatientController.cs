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


        public JsonResult CancelBooking(int Id)
        {
            string result = "success";
            try
            {
                int res = _bookingBLL.ChangeBookingStatus(Id, 5); // Cancel

                if(res <=0)
                {
                    result = "Something went wrong please try again";
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }


        #region Change Password 

        public ActionResult ChangePassword()
        {
            return View();
        }

        #endregion
    }
}