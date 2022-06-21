using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.VM
{
    public class BookingVM
    {
        public int BookingId { get; set; }
        public int BookingNo { get; set; }
        public DateTime BookingDate { get; set; }
        public double BookingAmount { get; set; }
        public double BookingFees { get; set; }
        public byte BookingStatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int? ScheduleTimingId { get; set; }
        public bool? IsOutService { get; set; }

        //=============
        public string DoctorName { get; set; }
        public string DoctorProfileImage { get; set; }

        public string CountryName { get; set; }

        //=============
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public string PatientEmail { get; set; }
        public string PatientAddress { get; set; }

    }
}
