using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.VM
{
    public class PatientVM
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public string PatientEmail { get; set; }
        public DateTime PatientDateOfBirth { get; set; }
        public string PatientAddress { get; set; }
        public byte PatientGender{ get; set; }
        public string PatientNationalID { get; set; }
        public double? PatientWeight { get; set; }
        public double? PatientHeight{ get; set; }
        public string PatientProfileImage { get; set; }
        public byte BlodGroupId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Patient_UserId { get; set; }



        public string PatientGenderName { get; set; }
        public string PatientBlodGroupName { get; set; }
        public string Username { get; set; }
        public int? Age { get; set; }

    }
}
