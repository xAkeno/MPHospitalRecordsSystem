using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPHospitalRecordsSystem
{
    internal class visitsDTO
    {
        public int VisitId { get; set; }           
        public int? PatientId { get; set; }        
        public String PatientName { get; set; }
        public String dateOfBirth { get; set; }
        public String ContactNumber { get; set; }
        public int? DoctorId { get; set; }    
        public String DoctorName { get; set; }
        public String Specialty { get; set; }
        public DateTime DateOfVisit { get; set; }  
        public string Diagnosis { get; set; }      
        public string Treatment { get; set; }
    }
}
