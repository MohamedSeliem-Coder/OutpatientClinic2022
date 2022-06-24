using Clinic.BLL.VM;
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
        public ActionResult BookAppointment(int id)
        {
            var doctor = _doctorBLL.Get_Doctor_ById(id);

            ViewBag.Doctor = doctor;

            // int patientId = _patientBLL.GetPatientId(UserID);
            // var patient = _patientBLL.Get_Patient_ById(patientId);

            return View();
        }

        public PartialViewResult GetScheduleTimings(int DoctorId, DateTime? StartDate, DateTime? EndDate)
        {
            return PartialView("_DoctorScheduleTimings", _doctorBLL.Get_ScheduleTimings(DoctorId, StartDate, EndDate));
        }


        [Authorize(Roles = "Patient")]
        public ActionResult Checkout(int Id)
        {
            var booking = _bookingBLL.Get_Booking_ById(Id);

            return View(booking);
        }

        public JsonResult SavePayment(int BookingId)
        {
            string result = "success";
            try
            {
                _bookingBLL.ChangeBookingStatus(BookingId, 3); // Confirmed

                _bookingBLL.ChangePaidStatus(BookingId);   // Paid

                return Json(new { Message = result, BookingId = BookingId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(new { Message = e.Message, BookingId = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BookingSuccess(int Id)
        {
            var booking = _bookingBLL.Get_Booking_ById(Id);
            return View(booking);
        }



        #region Save Booking

        public JsonResult SaveBookingData(BookingVM obj)
        {
            string result = "success";
            try
            {
                var doctor = _doctorBLL.Get_Doctor_ById(obj.DoctorId);

                if (obj.IsOutService.HasValue && obj.IsOutService.Value)
                {
                    obj.BookingAmount = doctor.OutsideServicePrice ?? 0;                    
                }
                else
                {
                    obj.BookingAmount = doctor.InsideServicePrice ?? 0;
                }

                obj.BookingFees = (obj.BookingAmount * 10) / 100; 

                obj.PatientId = _patientBLL.GetPatientId(UserID);
                obj.BookingStatusId = 1;
                obj.CreationDate = DateTime.Now;

                var res = _bookingBLL.Save_Booking_Data(obj);

                if(res == null || res == 0)
                {
                    result = "Booking Not Saved Successfuly";
                }

                return Json(new { Message = result  , BookingId = res ?? 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Message = e.Message, BookingId = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

    }
}