using Clinic.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class ComonBLL :BaseBLL
    {
        public List<AspNetUser> Get_AspNetUsers_List()
        {
            var result = db.AspNetUsers.ToList();
            return result;
        }

        public List<SP_BlodGroups_GetData_Result> Get_BlodGroups_Data(byte? id)
        {
            return db.SP_BlodGroups_GetData(id).ToList();
        }
    }
}
