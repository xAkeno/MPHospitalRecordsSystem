using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MPHospitalRecordsSystem
{
    internal class doctor
    {
        public string DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }

       
        public doctor(string doctorId, string name, string specialty)
        {
            DoctorId = doctorId;
            Name = name;
            Specialty = specialty;
        }

        
        public void AddDoctor()
        {
            try
            {
                connection db = new connection();
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO doctors (doctor_id, Name, Specialty) VALUES (@id, @name, @specialty)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", DoctorId);
                        cmd.Parameters.AddWithValue("@name", Name);
                        cmd.Parameters.AddWithValue("@specialty", Specialty);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding doctor: " + ex.Message);
            }
        }

        /*public static DataTable GetAllDoctors()
        {
            try
            {
                connection db = new connection();
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM doctors";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching doctors: " + ex.Message);
            }
        }*/

        public void UpdateDoctor()
        {
            try
            {
                connection db = new connection();
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE doctors SET Name=@name, Specialty=@specialty WHERE doctor_id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", DoctorId);
                        cmd.Parameters.AddWithValue("@name", Name);
                        cmd.Parameters.AddWithValue("@specialty", Specialty);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating doctor: " + ex.Message);
            }
        }

        public void DeleteDoctor()
        {
            try
            {
                connection db = new connection();
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM doctors WHERE doctor_id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", DoctorId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting doctor: " + ex.Message);
            }
        }

    }
}
