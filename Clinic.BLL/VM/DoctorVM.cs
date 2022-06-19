using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.VM
{
    public class DoctorVM
    {

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhone { get; set; }
        public string DoctorEmail { get; set; }
        public Nullable<System.DateTime> DoctorDateOfBirth { get; set; }
        public string DoctorDegree { get; set; }
        public string DoctorCollege { get; set; }
        public string DoctorAwards { get; set; }
        public string DoctorProfileImage { get; set; }
        public Nullable<System.DateTime> DoctorMemberSince { get; set; }
        public byte DoctorGender { get; set; }
        public string NationalID { get; set; }
        public string DoctorAbout { get; set; }
        public Nullable<double> InsideServicePrice { get; set; }
        public Nullable<double> OutsideServicePrice { get; set; }
        public int SpecialityId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<bool> DoctorIsActive { get; set; }
        public string Doctor_UserId { get; set; }


        public string Username { get; set; }
        public string CountryName { get; set; }
        public string SpecialityName { get; set; }
        public string SpecialityImagePath { get; set; }
        public string DoctorGenderName { get; set; }
        public int? Age { get; set; }
    }
}
