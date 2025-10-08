using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPHospitalRecordsSystem
{
    internal class DocScheduleDTO
    {
        public int ScheduleId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeSpan AvailableTime { get; set; }
    }
}
