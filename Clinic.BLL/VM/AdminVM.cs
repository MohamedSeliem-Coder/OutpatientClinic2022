using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Clinic.BLL.VM
{
    public class AdminVM
    {

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public byte EmployeeGender { get; set; }
        public System.DateTime EmployeeDateOfBirth { get; set; }
        public string EmployeeProfileImage { get; set; }
        public string EmployeeAddress { get; set; }
        public bool EmployeeIsActive { get; set; }
        public string NationalID { get; set; }

        public string Admin_UserId { get; set; }

        public string EmployeeGenderName { get; set; }
        public string Username { get; set; }
        public int? Age { get; set; }

        //===========================
        public HttpPostedFileBase Image { get; set; }
    }
}
