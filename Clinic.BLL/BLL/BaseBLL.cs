using Clinic.DAL.Model;

namespace Clinic.BLL.BLL
{
    public class BaseBLL
    {
        public OutpatientClinic_DBEntities db = new OutpatientClinic_DBEntities();

        public const string PatientProfileImagePath = "/Uploads/ProfileImage/Patient/";

        public const string MedicationsImagePath = "/Uploads/Medications/";

        public const string DoctorProfileImagePath = "/Uploads/ProfileImage/Doctor/";

        public const string EmployeeProfileImagePath = "/Uploads/ProfileImage/Employee/";

        public const string SpecialityImagePath = "/Uploads/Specialities/";
    }
}
