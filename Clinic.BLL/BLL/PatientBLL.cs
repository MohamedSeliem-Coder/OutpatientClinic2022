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

        #region Save

        public int? Save_Patient_Data(PatientVM obj)
        {
            try
            {
                var result = db.SP_Patient_Save(obj.PatientId, obj.PatientName, obj.PatientPhone, obj.PatientEmail, obj.PatientDateOfBirth,
                    obj.PatientAddress, obj.PatientGender, obj.PatientNationalID, obj.PatientWeight, obj.PatientHeight, obj.PatientProfileImage,
                    obj.BlodGroupId, obj.Patient_UserId, obj.Username).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        #endregion

        #region Get Data
        public List<PatientVM> Get_Patient_List(int? patientId, string nationalID, string phone, string email, byte? gender, byte? blodGroupId, string userId)
        {

            try
            {
                var result = db.SP_Patient_GetData(patientId, phone, email, gender, nationalID, blodGroupId, userId).Select(obj => new PatientVM
                {
                    PatientId = obj.PatientId,
                    PatientName = obj.PatientName,
                    PatientPhone = obj.PatientPhone,
                    PatientEmail = obj.PatientEmail,
                    PatientAddress = obj.PatientAddress,
                    PatientGender = obj.PatientGender,
                    PatientGenderName = obj.PatientGenderName,
                    BlodGroupId = obj.BlodGroupId,
                    PatientBlodGroupName = obj.BlodGroupName,
                    PatientDateOfBirth = obj.PatientDateOfBirth,
                    PatientHeight = obj.PatientsHeight,
                    PatientWeight = obj.PatientWeight,
                    CreationDate = obj.CreationDate,
                    PatientNationalID = obj.NationalID,
                    PatientProfileImage = PatientProfileImagePath + obj.PatientProfileImage,
                    Patient_UserId = obj.UserId,
                    Username = obj.UserName,
                    Age = obj.Age
                }).ToList();

                return result;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public PatientVM Get_Patient_ById(int id)
        {
            try
            {
                var result = db.SP_Patient_GetData(id, null, null, null, null, null, null).Select(obj => new PatientVM
                {
                    PatientId = obj.PatientId,
                    PatientName = obj.PatientName,
                    PatientPhone = obj.PatientPhone,
                    PatientEmail = obj.PatientEmail,
                    PatientAddress = obj.PatientAddress,
                    PatientGender = obj.PatientGender,
                    PatientGenderName = obj.PatientGenderName,
                    BlodGroupId = obj.BlodGroupId,
                    PatientBlodGroupName = obj.BlodGroupName,
                    PatientDateOfBirth = obj.PatientDateOfBirth,
                    PatientHeight = obj.PatientsHeight,
                    PatientWeight = obj.PatientWeight,
                    CreationDate = obj.CreationDate,
                    PatientNationalID = obj.NationalID,
                    PatientProfileImage = PatientProfileImagePath+ obj.PatientProfileImage,
                    Patient_UserId = obj.UserId,
                    Username = obj.UserName,
                    Age = obj.Age
                }).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetPatientId(string userId)
        {
            return db.SP_GetPatientId_ByUserId(userId);
        }

        #endregion
    }
}
