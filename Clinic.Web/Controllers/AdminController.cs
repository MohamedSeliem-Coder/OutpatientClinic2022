using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.DoctorCount = _doctorBLL.Get_Doctor_List(null, null, null, null, null, null, null, null).Count;

            ViewBag.PatientsCount = _patientBLL.Get_Patient_List(null, null, null, null, null, null, null).Count;

            var bookings = _bookingBLL.Get_Booking_List(null, null, null, null, null, null, null, null);

            ViewBag.AppointmentsCount = bookings.Count;

            ViewBag.Revenue = bookings.Where(a => a.BookingStatusId != 5).Sum(a => a.BookingAmount);


            return View();
        }

        public ActionResult Appointments()
        {
            var bookings = _bookingBLL.Get_Booking_List(null, null, null, null, null, null, null, null);

            return View(bookings);
        }

        public ActionResult Specialities()
        {
            var Specialities = _comonBLL.Get_Specialities_Data(null);

            return View(Specialities);
        }

        //Doctors

        public ActionResult Doctors()
        {
            var doctors = _doctorBLL.Get_Doctor_List(null, null, null, null, null, null, null, null);

            return View(doctors);
        }

        public ActionResult Patients()
        {
            var patients = _patientBLL.Get_Patient_List(null, null, null, null, null, null, null);

            return View(patients);
        }




        #region Profile Setting

        [Authorize(Roles = "Employee")]
        public ActionResult ProfileSettings()
        {
            int adminId = _adminBLL.GetEmployeeId(UserID);

            var Employee = _adminBLL.Get_Employee_ById(adminId);

           // ViewBag.BlodGroups = _comonBLL.Get_BlodGroups_Data(null);

            return View(Employee);
        }


        public JsonResult SaveData(AdminVM obj)
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
                    obj.EmployeeProfileImage = FileName;
                    obj.Image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/ProfileImage/Employee/"), FileName));
                }
                #endregion

                var res = _adminBLL.Save_Employee_Data(obj);
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