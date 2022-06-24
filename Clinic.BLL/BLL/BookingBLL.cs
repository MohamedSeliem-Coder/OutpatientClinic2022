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
                        BookingDate = obj.BookingDate,
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
                        PatientProfileImage =PatientProfileImagePath + obj.PatientProfileImage
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
                        BookingDate = obj.BookingDate,
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
                        SpecialityName= obj.SpecialityName,
                        StatusName = obj.StatusName,
                        PatientProfileImage = PatientProfileImagePath + obj.PatientProfileImage
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
    }
}
