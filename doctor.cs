using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MPHospitalRecordsSystem
{
    internal class doctor
    {
        public string DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }

        public doctor() { 
        
        }

       
        public doctor(string doctorId, string name, string specialty)
        {
            DoctorId = doctorId;
            Name = name;
            Specialty = specialty;
        }

        connection con = new connection();

        public void AddDoctor(String name,String sp)
        {
            bool check = true;
            String sqlInsertPatient = "INSERT INTO doctors (Name, Specialty) VALUES (@Name, @Specialty)";
            if (name.Equals("") && sp.Equals(""))
            {
                MessageBox.Show("Please answer all the forms");
                check = false;
            }

            if (check)
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    try
                    {
                        c.Open();
                        using (MySqlCommand checkName = new MySqlCommand(sqlInsertPatient, c))
                        {
                            checkName.Parameters.AddWithValue("@Name", name);
                            checkName.Parameters.AddWithValue("@Specialty", sp);
                            object data = checkName.ExecuteScalar();

                            if (data != null)
                            {
                                MessageBox.Show("Patient is already taken");
                                return;
                            }
                        }

                        using (MySqlCommand cmd = new MySqlCommand(sqlInsertPatient, c))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Specialty", sp);

                            int row = cmd.ExecuteNonQuery();
                            if (row > 0)
                            {
                                MessageBox.Show("Successfully registered!");
                            }
                            else
                            {
                                MessageBox.Show("Insert failed.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public bool check_if_info_is_already_registred(int id, String name)
        {
            String sqlCheck = "SELECT * FROM doctors WHERE doctor_id=@id OR Name=@Name";
            if (id == 0 && name.Equals(""))
            {
                return false;
            }
            else
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlCheck, c))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Name", name);

                        c.Open();

                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            return true;
                        }
                    }

                }
            }

            return false;
        }
        public List<doctorDTO> read_doctors()
        {
            String sqlSelectPatient = "SELECT * FROM doctors";
            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSelectPatient, c))
                    {
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<doctorDTO> list = new List<doctorDTO>();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("doctor_id");
                            String name = reader.GetString("Name");
                            String specialty = reader.GetString("Specialty");
                            list.Add(new doctorDTO
                            {
                                DoctorId = id.ToString()
                                ,
                                DoctorName = name
                                ,
                                Specialty = specialty
                            });
                        }
                        return list;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public void update_doctor(String name, int id,String special)
        {
            String sqlUpdatePatient = "UPDATE doctors SET Name=@Name, Specialty=@Specialty WHERE doctor_id=@doctor_id";
            try
            {
                using (MySqlConnection c = con.GetConnection())
                {

                    using (MySqlCommand cmd = new MySqlCommand(sqlUpdatePatient, c))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@doctor_id", id);
                        cmd.Parameters.AddWithValue("@Specialty", special);

                        c.Open();
                        int row = cmd.ExecuteNonQuery();

                        if (row > 0)
                        {
                            MessageBox.Show("Successfully updated!");
                        }
                        else MessageBox.Show("Patient is already taken");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
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
