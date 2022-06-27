using Clinic.BLL.VM;
using Clinic.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class DoctorBLL : BaseBLL
    {

        #region Save

        public int? Save_Doctor_Data(DoctorVM obj)
        {
            try
            {
                var result = db.SP_Doctors_Save(obj.DoctorId, obj.DoctorName, obj.DoctorPhone, obj.DoctorEmail, obj.DoctorDateOfBirth,
                    obj.DoctorDegree, obj.DoctorCollege, obj.DoctorAwards, obj.DoctorProfileImage, obj.DoctorMemberSince, obj.DoctorGender,
                    obj.NationalID, obj.DoctorAbout, obj.InsideServicePrice, obj.OutsideServicePrice, obj.CountryId, obj.SpecialityId,
                    obj.DoctorIsActive, obj.Doctor_UserId, obj.Username).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        #endregion

        #region Get Data
        public List<DoctorVM> Get_Doctor_List(int? doctorId, string nationalID, string phone, string email,
            string userId, byte? gender, int? countryId, int? specialityId)
        {
            try
            {
                var result = db.SP_Doctors_GetData(doctorId, phone, email, gender, nationalID, countryId, specialityId, userId)
                    .Select(obj => new DoctorVM
                    {
                        CountryId = obj.CountryId,
                        CountryName = obj.CountryName,
                        CreationDate = obj.CreationDate,
                        DoctorAbout = obj.DoctorAbout,
                        DoctorAwards = obj.DoctorAwards,
                        DoctorCollege = obj.DoctorCollege,
                        DoctorDateOfBirth = obj.DoctorDateOfBirth,
                        DoctorDegree = obj.DoctorDegree,
                        DoctorEmail = obj.DoctorEmail,
                        DoctorGender = obj.DoctorGender,
                        DoctorGenderName=obj.DoctorGenderName,
                        DoctorId = obj.DoctorId,
                        DoctorIsActive = obj.DoctorIsActive,
                        DoctorMemberSince = obj.DoctorMemberSince,
                        DoctorName = obj.DoctorName,
                        DoctorPhone = obj.DoctorPhone,
                        DoctorProfileImage = DoctorProfileImagePath + obj.DoctorProfileImage,
                        Doctor_UserId = obj.UserId,
                        InsideServicePrice = obj.InsideServicePrice,
                        NationalID = obj.NationalID,
                        OutsideServicePrice = obj.OutsideServicePrice,
                        SpecialityId = obj.SpecialityId,
                        SpecialityName=obj.SpecialityName,
                        SpecialityImagePath = SpecialityImagePath + obj.SpecialityImagePath,
                        Username = obj.UserName,
                        Age =obj.Age

                    }).ToList();

                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public DoctorVM Get_Doctor_ById(int id)
        {
            try
            {
                var result = db.SP_Doctors_GetData(id,null,null,null,null,null,null,null)
                    .Select(obj => new DoctorVM
                    {
                        CountryId = obj.CountryId,
                        CountryName = obj.CountryName,
                        CreationDate = obj.CreationDate,
                        DoctorAbout = obj.DoctorAbout,
                        DoctorAwards = obj.DoctorAwards,
                        DoctorCollege = obj.DoctorCollege,
                        DoctorDateOfBirth = obj.DoctorDateOfBirth,
                        DoctorDegree = obj.DoctorDegree,
                        DoctorEmail = obj.DoctorEmail,
                        DoctorGender = obj.DoctorGender,
                        DoctorGenderName = obj.DoctorGenderName,
                        DoctorId = obj.DoctorId,
                        DoctorIsActive = obj.DoctorIsActive,
                        DoctorMemberSince = obj.DoctorMemberSince,
                        DoctorName = obj.DoctorName,
                        DoctorPhone = obj.DoctorPhone,
                        DoctorProfileImage = DoctorProfileImagePath + obj.DoctorProfileImage,
                        Doctor_UserId = obj.UserId,
                        InsideServicePrice = obj.InsideServicePrice,
                        NationalID = obj.NationalID,
                        OutsideServicePrice = obj.OutsideServicePrice,
                        SpecialityId = obj.SpecialityId,
                        SpecialityName = obj.SpecialityName,
                        SpecialityImagePath = SpecialityImagePath + obj.SpecialityImagePath,
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

        public int GetDoctorId(string userId)
        {
            return db.SP_GetDoctorId_ByUserId(userId).FirstOrDefault().Value;
        }

        #endregion


        #region Schedule Timings

        public List<SP_GetScheduleTimings_ByDate_Result> Get_ScheduleTimings(int doctorId , DateTime? from , DateTime ? to)
        {
            from = from == null ? DateTime.Now : from;
            to = to == null ? from.HasValue ? from.Value.AddDays(15) : DateTime.Now.AddDays(15) : to;

            return db.SP_GetScheduleTimings_ByDate(doctorId, from, to).ToList();
        }

        public List<SP_ScheduleTimings_GetData_Result> Get_ScheduleTimings_GetData(int? drTimingId,byte? weekDayId, int? doctorId,string doctorUserId)
        {
            return db.SP_ScheduleTimings_GetData(drTimingId, weekDayId, doctorId, doctorUserId).ToList();
        }

        #endregion
    }
}
