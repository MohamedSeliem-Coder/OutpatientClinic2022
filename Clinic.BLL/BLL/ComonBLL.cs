using Clinic.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class ComonBLL : BaseBLL
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

        public List<SP_Specialities_GetData_Result> Get_Specialities_Data(int? specialityId)
        {
            return db.SP_Specialities_GetData(specialityId).ToList();
        }

        public List<SP_BookingStatuses_GetData_Result> Get_BookingStatuses_Data(byte? statusId)
        {
            return db.SP_BookingStatuses_GetData(statusId).ToList();
        }

        //SP_Diseases_GetData_Result

        public List<SP_Diseases_GetData_Result> Get_Diseases_Data(int? diseaseId)
        {
            return db.SP_Diseases_GetData(diseaseId).ToList();
        }

        //SP_Medications_GetData_Result

        public List<SP_Medications_GetData_Result> Get_Medications_Data(int? medicationId)
        {
            return db.SP_Medications_GetData(medicationId)
                .Select(a => new SP_Medications_GetData_Result
                {
                    MedicationId = a.MedicationId,
                    MedicationAbout = a.MedicationAbout,
                    MedicationImagePath = MedicationsImagePath + a.MedicationImagePath,
                    MedicationName = a.MedicationName
                })
                .ToList();
        }


        //SP_Countries_GetData_Result
        public List<SP_Countries_GetData_Result> Get_Countries_Data(int? countryId)
        {
            return db.SP_Countries_GetData(countryId).ToList();
        }


        public void ChangePassword(string UserID,string HashedPassword,string SecurityStamp)
        {
            try
            {
                db.Users_UpdatePassword(UserID, HashedPassword, SecurityStamp);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //SP_WeekDays_GetData_Result

        public List<SP_WeekDays_GetData_Result> Get_WeekDays_Data(byte? weekDayId,string weekDayName)
        {
            return db.SP_WeekDays_GetData(weekDayId, weekDayName).ToList();
        }
    }
}
