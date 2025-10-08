using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPHospitalRecordsSystem
{
    internal class appointmentDTO
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public String PatientName { get; set; }
        public String ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public String Status { get; set; }
    }
}
