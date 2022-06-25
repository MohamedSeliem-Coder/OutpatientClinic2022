using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.BLL.VM
{
    public class PrescriptionVM
    {
        public int PrescriptionId { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string PrescriptionNotes { get; set; }
        public string DoctorReport { get; set; }
        public int DiseaseId { get; set; }
        public int BookingId { get; set; }

        public List<PrescriptionMedicationVM> Medications { get; set; }


        //======================
        public string DiseaseName { get; set; }
        public int BookingNo { get; set; }

        public System.DateTime BookingDate { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhone { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorProfileImage { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public string PatientAddress { get; set; }
        public string PatientProfileImage { get; set; }
    }
}
