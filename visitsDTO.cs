using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPHospitalRecordsSystem
{
    internal class visitsDTO
    {
        public int VisitId { get; set; }           // Primary key, auto-increment
        public int? PatientId { get; set; }        // Nullable, indexed
        public int? DoctorId { get; set; }         // Nullable, indexed
        public DateTime DateOfVisit { get; set; }  // Not nullable
        public string Diagnosis { get; set; }      // Not nullable
        public string Treatment { get; set; }
    }
}
