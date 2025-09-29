using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MPHospitalRecordsSystem
{
    internal class connection
    {
        protected static String connect = "Server=localhost; Database=hospital_records_db; Uid=root; Pwd=;";
        public   MySqlConnection GetConnection()
        {
            return new MySqlConnection(connect);
        }
    }
}
