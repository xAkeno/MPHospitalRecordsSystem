using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace MPHospitalRecordsSystem
{
    internal class visit
    {
        connection con = new connection();

        public void AddVisit(int patient_id, int doctor_id, String date_of_visit, String Diagnosis, String Treatment)
        {
            bool check = true;
            String sqlInsertVisit = "INSERT INTO Visits (patient_id,doctor_id,date_of_visit,Diagnosis,Treatment) VALUES (@patient_id,@doctor_id,@date_of_visit,@Diagnosis,@Treatment)";
            String sqlCheck = "SELECT * FROM Visits WHERE patient_id=@patient_id AND doctor_id=@doctor_id AND date_of_visit=@date_of_visit";
            if (patient_id == 0 || doctor_id == 0 || date_of_visit.Equals("") || Diagnosis.Equals("") || Treatment.Equals(""))
            {
                MessageBox.Show("Please fill in all fields");
                check = false;
            }

            if (check)
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    try
                    {
                        c.Open();
                        using (MySqlCommand checkName = new MySqlCommand(sqlCheck, c))
                        {
                            checkName.Parameters.AddWithValue("@patient_id", patient_id);
                            checkName.Parameters.AddWithValue("@doctor_id", doctor_id);
                            checkName.Parameters.AddWithValue("@date_of_visit", date_of_visit);

                            object data = checkName.ExecuteScalar();

                            if (data != null)
                            {
                                MessageBox.Show("Patient is already taken");
                                return;
                            }
                        }

                        using (MySqlCommand cmd = new MySqlCommand(sqlInsertVisit, c))
                        {
                            cmd.Parameters.AddWithValue("@patient_id", patient_id);
                            cmd.Parameters.AddWithValue("@doctor_id", doctor_id);
                            cmd.Parameters.AddWithValue("@date_of_visit", date_of_visit);
                            cmd.Parameters.AddWithValue("@Diagnosis", Diagnosis);
                            cmd.Parameters.AddWithValue("@Treatment", Treatment);


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
        public List<patientDTO> getAllPatient() {
            String sqlSelectPatient = "SELECT * FROM patients";
            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSelectPatient, c))
                    {
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<patientDTO> list = new List<patientDTO>();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("patient_id");
                            String name = reader.GetString("Name");
                            DateTime dob = reader.GetDateTime("Date_of_Birth");
                            String contact = reader.GetString("Contact_Number");
                            list.Add(new patientDTO
                            {
                                PatientId = id
                                ,
                                Name = name
                                ,
                                DateOfBirth = dob
                                ,
                                ContactNumber = contact
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
        public List<doctorDTO> getAllDoctors()
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
                                DoctorId = id
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
        public List<visitsDTO> read_visits()
        {
            String sqlSelectPatient = "SELECT * FROM visits";
            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSelectPatient, c))
                    {
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<visitsDTO> list = new List<visitsDTO>();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("visit_id");
                            int patient_id = reader.GetInt32("patient_id");
                            int doctor_id = reader.GetInt32("doctor_id");
                            DateTime date_of_visit = reader.GetDateTime("date_of_visit");
                            String Diagnosis = reader.GetString("Diagnosis");
                            String Treatment = reader.GetString("Treatment");

                            list.Add(new visitsDTO
                            {
                                VisitId = id
                                ,
                                PatientId = patient_id
                                ,
                                DoctorId = doctor_id
                                ,
                                DateOfVisit = date_of_visit
                                ,
                                Diagnosis = Diagnosis
                                ,
                                Treatment = Treatment
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
        public void update_doctor(String name, int id, String special)
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
        public String get_next_id()
        {

            String sqlNextId = "SELECT MAX(visit_id) from visits ";
            using (MySqlConnection c = con.GetConnection()) 
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlNextId, c))
                {
                    c.Open();
                    object result = cmd.ExecuteScalar();

                    int nextId = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 0;

                    return Convert.ToString(nextId);
                }
            }

            return null;
        }
        //public void UpdateDoctor()
        //{
        //    try
        //    {
        //        connection db = new connection();
        //        using (MySqlConnection conn = db.GetConnection())
        //        {
        //            conn.Open();
        //            string query = "UPDATE doctors SET Name=@name, Specialty=@specialty WHERE doctor_id=@id";
        //            using (MySqlCommand cmd = new MySqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@id", DoctorId);
        //                cmd.Parameters.AddWithValue("@name", Name);
        //                cmd.Parameters.AddWithValue("@specialty", Specialty);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error while updating doctor: " + ex.Message);
        //    }
        //}

        public void DeleteDoctor(int doctor_id)
        {
            String sqlDeletePatient = "DELETE FROM doctors WHERE doctor_id=@doctor_id";
            using (MySqlConnection c = con.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlDeletePatient, c))
                {
                    cmd.Parameters.AddWithValue("@doctor_id", doctor_id);
                    try
                    {
                        c.Open();
                        int row = 0;
                        row = cmd.ExecuteNonQuery();
                        if (row > 0)
                        {
                            MessageBox.Show("Successfully deleted!");
                        }
                        else MessageBox.Show("Patient is already taken");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public List<doctorDTO> search_doctor(String search)
        {
            String sqlSearchPatient = "SELECT * FROM doctors WHERE Name LIKE @Name OR Specialty LIKE @Special OR doctor_id LIKE @id";
            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSearchPatient, c))
                    {
                        cmd.Parameters.AddWithValue("@Name", "%" + search + "%");
                        cmd.Parameters.AddWithValue("@Special", "%" + search + "%");
                        cmd.Parameters.AddWithValue("@id", "%" + search + "%");
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
                                DoctorId = id
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
    }
}
