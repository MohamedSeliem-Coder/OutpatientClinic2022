﻿using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class AdminBLL : BaseBLL
    {
        public List<AdminVM> Get_Employee_List(int? employeeId, string nationalID, string phone, string email, string userId)
        {
            return new List<AdminVM>();
        }
    }
}
