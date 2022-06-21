using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class AdminBLL : BaseBLL
    {

        #region Save 

        public int? Save_Employee_Data(AdminVM obj)
        {
            try
            {
                var result = db.SP_Employees_Save(obj.EmployeeId, obj.EmployeeName, obj.EmployeePhone, obj.EmployeeEmail,
                    obj.EmployeeGender, obj.EmployeeDateOfBirth, obj.EmployeeProfileImage, obj.EmployeeAddress, obj.EmployeeIsActive,
                    obj.NationalID, obj.Admin_UserId, obj.Username).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Get Data

        public List<AdminVM> Get_Employee_List(int? employeeId, string nationalID, string phone, string email, byte? gender, string userId)
        {
            try
            {
                var result = db.SP_Employees_GetData(employeeId, phone, email, gender, nationalID, userId)
                    .Select(obj => new AdminVM
                    {
                        EmployeeId = obj.EmployeeId,
                        EmployeeName = obj.EmployeeName,
                        EmployeePhone = obj.EmployeePhone,
                        EmployeeEmail = obj.EmployeeEmail,
                        EmployeeGender = obj.EmployeeGender,
                        EmployeeAddress = obj.EmployeeAddress,
                        EmployeeDateOfBirth = obj.EmployeeDateOfBirth,
                        EmployeeIsActive = obj.EmployeeIsActive,
                        EmployeeProfileImage = EmployeeProfileImagePath + obj.EmployeeProfileImage,
                        Admin_UserId = obj.UserId,
                        Username = obj.UserName,
                        Age = obj.Age,
                        EmployeeGenderName = obj.EmployeeGenderName,
                        NationalID = obj.NationalID
                    }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public AdminVM Get_Employee_ById(int id)
        {
            try
            {
                var result = db.SP_Employees_GetData(id,null,null,null,null,null)
                    .Select(obj => new AdminVM
                    {
                        EmployeeId = obj.EmployeeId,
                        EmployeeName = obj.EmployeeName,
                        EmployeePhone = obj.EmployeePhone,
                        EmployeeEmail = obj.EmployeeEmail,
                        EmployeeGender = obj.EmployeeGender,
                        EmployeeAddress = obj.EmployeeAddress,
                        EmployeeDateOfBirth = obj.EmployeeDateOfBirth,
                        EmployeeIsActive = obj.EmployeeIsActive,
                        EmployeeProfileImage = EmployeeProfileImagePath + obj.EmployeeProfileImage,
                        Admin_UserId = obj.UserId,
                        Username = obj.UserName,
                        Age = obj.Age,
                        EmployeeGenderName = obj.EmployeeGenderName,
                        NationalID = obj.NationalID
                    }).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetEmployeeId(string userId)
        {
            return db.SP_GetEmployeeId_ByUserId(userId).FirstOrDefault().Value;
        }
        #endregion
    }
}
