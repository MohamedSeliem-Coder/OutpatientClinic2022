using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class PatientBLL : BaseBLL
    {
        public List<PatientVM> Get_Patient_List(int?patientId , string nationalID,string phone , string email,string userId)
        {
            return new List<PatientVM>();
        }
    }
}
