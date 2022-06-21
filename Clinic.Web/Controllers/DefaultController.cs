using Clinic.BLL.BLL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    public class DefaultController : Controller
    {
        public string UserID { get { return User.Identity.GetUserId(); } }

        public ComonBLL _comonBLL = new ComonBLL();
        public PatientBLL _patientBLL = new PatientBLL();
        public DoctorBLL _doctorBLL = new DoctorBLL();
        public AdminBLL _adminBLL = new AdminBLL();
        public BookingBLL _bookingBLL = new BookingBLL();


    }
}