//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clinic.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.Prescriptions = new HashSet<Prescription>();
        }
    
        public int BookingId { get; set; }
        public int BookingNo { get; set; }
        public System.DateTime BookingDate { get; set; }
        public double BookingAmount { get; set; }
        public double BookingFees { get; set; }
        public byte BookingStatusFk { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public int DoctorFk { get; set; }
        public int PatientFk { get; set; }
        public Nullable<int> ScheduleTimingFk { get; set; }
        public Nullable<bool> IsOutService { get; set; }
    
        public virtual BookingStatus BookingStatus { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ScheduleTiming ScheduleTiming { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
