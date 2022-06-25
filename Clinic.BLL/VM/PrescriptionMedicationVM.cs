using System;

namespace Clinic.BLL.VM
{
    public class PrescriptionMedicationVM
    {
        public int Prescription_Medications_Id { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public string MedicationImagePath { get; set; }
        public string Notes { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int PrescriptionId { get; set; }
        public System.DateTime PrescriptionDate { get; set; }

    }
}
