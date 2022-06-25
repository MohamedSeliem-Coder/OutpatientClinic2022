﻿using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.IO;
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


        [Authorize(Roles = "Doctor")]
        public ActionResult AddPrescription(int Id)
        {
            var booking = _bookingBLL.Get_Booking_ById(Id);

            var patient = _patientBLL.Get_Patient_ById(booking.PatientId);
            ViewBag.Patient = patient;

            ViewBag.Diseases = _comonBLL.Get_Diseases_Data(null);

            ViewBag.Medications = _comonBLL.Get_Medications_Data(null);

            return View(booking);
        }


        public PartialViewResult GetMedicationRow(int MedicationId)
        {
            var medication = _comonBLL.Get_Medications_Data(MedicationId).FirstOrDefault();

            return PartialView("_MedicationRow", medication);
        }

        public JsonResult SavePrescriptionData(PrescriptionVM Prescription,List<int> MedicationsIds)
        {
            string result = "success";
            try
            {

                Prescription.PrescriptionDate = DateTime.Now;
                var res = _bookingBLL.Save_Prescription_Data(Prescription, MedicationsIds);

                if(res ==null || res == 0)
                {
                    result = "Prescription Data not save please try again";
                }

               int resStatus =  _bookingBLL.ChangeBookingStatus(Prescription.BookingId, 4); // Finished

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


        #region Profile Setting

        [Authorize(Roles = "Doctor")]
        public ActionResult ProfileSettings()
        {
            int doctorId = _doctorBLL.GetDoctorId(UserID);

            var Doctor = _doctorBLL.Get_Doctor_ById(doctorId);

            ViewBag.Specialities = _comonBLL.Get_Specialities_Data(null);

            ViewBag.Countries = _comonBLL.Get_Countries_Data(null);
            return View(Doctor);
        }


        public JsonResult SaveData(DoctorVM obj)
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
                    obj.DoctorProfileImage = FileName;
                    obj.Image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/ProfileImage/Doctor/"), FileName));
                }
                #endregion


                var res = _doctorBLL.Save_Doctor_Data(obj);
                if(res == null || res == 0)
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