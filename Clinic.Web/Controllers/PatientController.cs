using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.IO;
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


        #region Appointments

        public ActionResult Appointments()
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

        #endregion


        #region Prescriptions


        public ActionResult Prescriptions()
        {
            int patientId = _patientBLL.GetPatientId(UserID);

            var Prescriptions = _bookingBLL.Get_Prescriptions_Data(null, null, null, null, null, patientId);
            return View(Prescriptions);
        }


        public ActionResult ShowPrescription(int Id)
        {
            var Prescription = _bookingBLL.Get_Prescription_ById(Id);

            //ViewBag.Doctor = _doctorBLL.Get_Doctor_ById(Prescription.DoctorId);

            ViewBag.Patient = _patientBLL.Get_Patient_ById(Prescription.PatientId);
            

            return View(Prescription);
        }


        #endregion

        #region Favourites
        public ActionResult Favourites()
        {
           var Doctors = _doctorBLL.Get_Doctor_List(null, null, null, null, null, null, null, null);
            return View(Doctors);
        }

        #endregion

        #region Change Password 

        public ActionResult ChangePassword()
        {
            return View();
        }

        #endregion


        #region Profile Setting

        [Authorize(Roles = "Patient")]
        public ActionResult ProfileSettings()
        {
            int patientId = _patientBLL.GetPatientId(UserID);

            var Patient = _patientBLL.Get_Patient_ById(patientId);

            ViewBag.BlodGroups = _comonBLL.Get_BlodGroups_Data(null);

            return View(Patient);
        }


        public JsonResult SaveData(PatientVM obj)
        {
            string result = "success";
            try
            {
                #region Image

                if (obj.Image != null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(obj.Image.FileName);
                    string Exten = Path.GetExtension(obj.Image.FileName);
                    FileName = FileName + Guid.NewGuid() + Exten;
                    obj.PatientProfileImage = FileName;
                    obj.Image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/ProfileImage/Patient/"), FileName));
                }
                #endregion


                var res = _patientBLL.Save_Patient_Data(obj);
                if (res == null || res == 0)
                {
                    result = "Your Info Not Saved !";
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}