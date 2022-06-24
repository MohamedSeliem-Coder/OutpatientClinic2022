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
    }
}
