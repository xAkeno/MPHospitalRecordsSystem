using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPHospitalRecordsSystem
{
    internal class inventoryDTO
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string GenericName { get; set; }
        public string DosageForm { get; set; }
        public int StockQuantity { get; set; }
    }
}
