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
    
    public partial class Prescription
    {
        public int PrescriptionId { get; set; }
        public System.DateTime PrescriptionDate { get; set; }
        public string PrescriptionNotes { get; set; }
        public string DoctorReport { get; set; }
        public int DiseaseFk { get; set; }
        public int BookingFk { get; set; }
    
        public virtual Booking Booking { get; set; }
        public virtual Disease Disease { get; set; }
    }
}
