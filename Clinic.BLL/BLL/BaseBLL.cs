using Clinic.DAL.Model;

namespace Clinic.BLL.BLL
{
    public class BaseBLL
    {
        public OutpatientClinic_DBEntities db = new OutpatientClinic_DBEntities();

        public const string PatientProfileImagePath = "/Uploads/ProfileImage/Patient/";
    }
}
