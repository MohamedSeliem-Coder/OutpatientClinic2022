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
    
    public partial class SP_Prescription_Medications_GetData_Result
    {
        public int Prescription_Medications_Id { get; set; }
        public string Notes { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int PrescriptionId { get; set; }
        public System.DateTime PrescriptionDate { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public string MedicationImagePath { get; set; }
    }
}
