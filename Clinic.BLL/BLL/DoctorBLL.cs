using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class DoctorBLL:BaseBLL
    {
        public List<DoctorVM> Get_Doctor_List(int? doctorId, string nationalID, string phone, string email, string userId)
        {
            return new List<DoctorVM>();
        }
    }
}
