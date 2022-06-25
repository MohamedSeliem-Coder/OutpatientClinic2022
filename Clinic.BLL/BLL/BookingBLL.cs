using Clinic.BLL.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.BLL
{
    public class BookingBLL : BaseBLL
    {
        #region Save

        public int? Save_Booking_Data(BookingVM obj)
        {
            try
            {
                var result = db.SP_Booking_Insert(obj.BookingDate, obj.BookingAmount, obj.BookingFees, obj.BookingStatusId, obj.IsPaid,
                    obj.PaidDate, obj.DoctorId, obj.PatientId, obj.ScheduleTimingId, obj.IsOutService)
                    .FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Get Data

        public BookingVM Get_Booking_ById(int id)
        {
            try
            {
                var result = db.SP_Bookings_GetData(id, null, null, null, null, null, null, null)
                    .Select(obj => new BookingVM
                    {
                        BookingId = obj.BookingId,
                        BookingAmount = obj.BookingAmount,
                        BookingDate = obj.BookingDate??DateTime.Now,
                        BookingFees = obj.BookingFees,
                        BookingNo = obj.BookingNo,
                        BookingStatusId = obj.StatusId,
                        CreationDate = obj.BookingCreationDate,
                        DoctorId = obj.DoctorId,
                        IsOutService = obj.IsOutService,
                        IsPaid = obj.IsPaid,
                        PaidDate = obj.PaidDate,
                        PatientId = obj.PatientId,
                        ScheduleTimingId = obj.DrTimingId,
                        CountryName = obj.CountryName,
                        DoctorName = obj.DoctorName,
                        DoctorProfileImage = DoctorProfileImagePath + obj.DoctorProfileImage,
                        PatientAddress = obj.PatientAddress,
                        PatientEmail = obj.PatientEmail,
                        PatientName = obj.PatientName,
                        PatientPhone = obj.PatientPhone,
                        DrTimingSlotFrom = obj.DrTimingSlotFrom,
                        DrTimingSlotTo = obj.DrTimingSlotTo,
                        SpecialityName = obj.SpecialityName,
                        StatusName = obj.StatusName,
                        PatientProfileImage = PatientProfileImagePath + obj.PatientProfileImage,
                        PrescriptionId = obj.PrescriptionId
                    }).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<BookingVM> Get_Booking_List(int? bookingId, DateTime? bookingDateFrom, DateTime? bookingDateTo, byte? bookingStatusId, bool? isPaid
            , int? doctorId, int? patientId, bool? isOutService)
        {
            try
            {
                var result = db.SP_Bookings_GetData(bookingId, bookingDateFrom, bookingDateTo, bookingStatusId, isPaid, doctorId, patientId, isOutService)
                    .Select(obj => new BookingVM
                    {
                        BookingId = obj.BookingId,
                        BookingAmount = obj.BookingAmount,
                        BookingDate = obj.BookingDate??DateTime.Now,
                        BookingFees = obj.BookingFees,
                        BookingNo = obj.BookingNo,
                        BookingStatusId = obj.StatusId,
                        CreationDate = obj.BookingCreationDate,
                        DoctorId = obj.DoctorId,
                        IsOutService = obj.IsOutService,
                        IsPaid = obj.IsPaid,
                        PaidDate = obj.PaidDate,
                        PatientId = obj.PatientId,
                        ScheduleTimingId = obj.DrTimingId,
                        CountryName = obj.CountryName,
                        DoctorName = obj.DoctorName,
                        DoctorProfileImage = DoctorProfileImagePath + obj.DoctorProfileImage,
                        PatientAddress = obj.PatientAddress,
                        PatientEmail = obj.PatientEmail,
                        PatientName = obj.PatientName,
                        PatientPhone = obj.PatientPhone,
                        DrTimingSlotFrom = obj.DrTimingSlotFrom,
                        DrTimingSlotTo = obj.DrTimingSlotTo,
                        SpecialityName = obj.SpecialityName,
                        StatusName = obj.StatusName,
                        PatientProfileImage = PatientProfileImagePath + obj.PatientProfileImage,
                        PrescriptionId = obj.PrescriptionId
                    }).ToList();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Payment

        public int ChangePaidStatus(int BookingId)
        {
            try
            {
                var booking = db.Bookings.Find(BookingId);

                booking.IsPaid = true;

                booking.PaidDate = DateTime.Now;

                db.SaveChanges();

                return booking.BookingId;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int ChangeBookingStatus(int bookingId, byte statusId)
        {
            return db.SP_Booking_ChangeStatus(bookingId, statusId);
        }

        #endregion


        #region Prescription

        public int? Save_Prescription_Data(PrescriptionVM obj, List<int> medicationsIds)
        {
            try
            {

                var result = db.SP_Prescriptions_Insert(obj.PrescriptionDate, obj.PrescriptionNotes, obj.DoctorReport, obj.DiseaseId, obj.BookingId)
                    .FirstOrDefault();

                if (result != null && result > 0)
                {
                    if (medicationsIds != null && medicationsIds.Count > 0)
                    {
                        foreach (var item in medicationsIds)
                        {
                            var res = db.SP_Prescription_Medications_Insert(result, item, null).FirstOrDefault();
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Get Prescription Data


        public PrescriptionVM Get_Prescription_ById(int id)
        {
            try
            {
                var result = db.SP_Prescriptions_GetData(id,null,null,null,null,null)
                    .Select(obj => new PrescriptionVM
                    {
                        BookingDate = obj.BookingDate,
                        BookingId = obj.BookingId,
                        BookingNo = obj.BookingNo,
                        DiseaseId = obj.DiseaseId,
                        DiseaseName = obj.DiseaseName,
                        DoctorEmail = obj.DoctorEmail,
                        DoctorName = obj.DoctorName,
                        DoctorPhone = obj.DoctorPhone,
                        DoctorProfileImage = DoctorProfileImagePath + obj.DoctorProfileImage,
                        DoctorReport = obj.DoctorReport,
                        PatientAddress = obj.PatientAddress,
                        PatientId = obj.PatientId,
                        PatientName = obj.PatientName,
                        PatientPhone = obj.PatientPhone,
                        PatientProfileImage = PatientProfileImagePath + obj.PatientProfileImage,
                        PrescriptionDate = obj.PrescriptionDate,
                        PrescriptionId = obj.PrescriptionId,
                        PrescriptionNotes = obj.PrescriptionNotes,
                        Medications = Get_Medications_Data(null, obj.PrescriptionId),
                        DoctorId = obj.DoctorId
                    }).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PrescriptionVM> Get_Prescriptions_Data(int? prescriptionId, DateTime? prescriptionDateFrom, DateTime? prescriptionDateTo, int? bookingId, int? doctorId, int? patientId)
        {
            try
            {
                var result = db.SP_Prescriptions_GetData(prescriptionId, prescriptionDateFrom, prescriptionDateTo, bookingId, doctorId, patientId)
                    .Select(obj => new PrescriptionVM
                    {
                        BookingDate = obj.BookingDate,
                        BookingId = obj.BookingId,
                        BookingNo = obj.BookingNo,
                        DiseaseId = obj.DiseaseId,
                        DiseaseName = obj.DiseaseName,
                        DoctorEmail = obj.DoctorEmail,
                        DoctorName = obj.DoctorName,
                        DoctorPhone = obj.DoctorPhone,
                        DoctorProfileImage = DoctorProfileImagePath + obj.DoctorProfileImage,
                        DoctorReport = obj.DoctorReport,
                        PatientAddress = obj.PatientAddress,
                        PatientId = obj.PatientId,
                        PatientName = obj.PatientName,
                        PatientPhone = obj.PatientPhone,
                        PatientProfileImage = PatientProfileImagePath + obj.PatientProfileImage,
                        PrescriptionDate = obj.PrescriptionDate,
                        PrescriptionId = obj.PrescriptionId,
                        PrescriptionNotes = obj.PrescriptionNotes,
                        Medications = Get_Medications_Data(null,obj.PrescriptionId),
                         DoctorId = obj.DoctorId
                    }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PrescriptionMedicationVM> Get_Medications_Data(int? prescription_Medications_Id, int? prescriptionId)
        {
            try
            {
                var result = db.SP_Prescription_Medications_GetData(prescription_Medications_Id, prescriptionId)
                    .Select(obj => new PrescriptionMedicationVM
                    {
                        MedicationId = obj.MedicationId,
                        MedicationImagePath = MedicationsImagePath + obj.MedicationImagePath,
                        CreationDate = obj.CreationDate,
                        MedicationName = obj.MedicationName,
                        Notes = obj.Notes,
                        PrescriptionDate = obj.PrescriptionDate,
                        PrescriptionId = obj.PrescriptionId,
                        Prescription_Medications_Id = obj.Prescription_Medications_Id
                    }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
