using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    public class HomeController : DefaultController
    {
        
        public ActionResult Index()
        {
            ViewBag.Doctors = _doctorBLL.Get_Doctor_List(null, null, null, null, null, null, null, null);

            return View();
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }

        public ActionResult Blog()
        {

            return View();
        }

        //Doctors
        #region Doctors
        public ActionResult Doctors()
        {
            ViewBag.Specialities = _comonBLL.Get_Specialities_Data(null); 
            return View();
        }


        public PartialViewResult GetDoctors(string GenderIds =null , string SpecialityIds = null)
        {
            List<byte> gendersList = new List<byte>();
            List<int> specialitiesList = new List<int>();

            if (!string.IsNullOrEmpty(GenderIds))
            {
                var genders = GenderIds.Split(',');
                if (genders.Length > 0)
                {
                    foreach (var item in genders)
                    {
                        gendersList.Add(byte.Parse(item));
                    }
                }
            }

            if (!string.IsNullOrEmpty(SpecialityIds))
            {
                var specialities = SpecialityIds.Split(',');
                if (specialities.Length > 0)
                {
                    foreach (var item in specialities)
                    {
                        specialitiesList.Add(int.Parse(item));
                    }
                }
            }

            var doctors = _doctorBLL.Get_Doctor_List(null, null, null, null, null, null, null, null);

            if (gendersList.Count > 0)
            {
                doctors = doctors.Where(a => gendersList.Contains(a.DoctorGender)).ToList();
            }

            if (specialitiesList.Count > 0)
            {
                doctors = doctors.Where(a => specialitiesList.Contains(a.SpecialityId)).ToList();
            }

            return PartialView("_Doctors", doctors);
        }

        //Doctor Profile

        public ActionResult DoctorProfile(int id)
        {
            var doctor = _doctorBLL.Get_Doctor_ById(id);
            return View(doctor);
        }

        #endregion

    }
}