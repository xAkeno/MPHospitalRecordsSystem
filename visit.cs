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
        public bool check_if_info_is_already_registred(String treatment, String diagnosis)
        {
            String sqlCheck = "SELECT * FROM visits WHERE Treatment=@treatment OR Diagnosis=@diagnosis";
            if (treatment.Equals("") && diagnosis.Equals(""))
            {
                return false;
            }
            else
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlCheck, c))
                    {
                        cmd.Parameters.AddWithValue("@treament", treatment);
                        cmd.Parameters.AddWithValue("@diagnosis", diagnosis);

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

                            visitsDTO dto = new visitsDTO();

                            // Get patient details
                            using (MySqlConnection c2 = con.GetConnection())
                            {
                                String sqlGetPatientName = "SELECT * FROM patients WHERE patient_id=@patient_id";
                                c2.Open();
                                using (MySqlCommand cmd2 = new MySqlCommand(sqlGetPatientName, c2))
                                {
                                    cmd2.Parameters.AddWithValue("@patient_id", patient_id);
                                    MySqlDataReader patientReader = cmd2.ExecuteReader();

                                    while (patientReader.Read())
                                    {

                                        if (patientReader.HasRows)
                                        {
                                            dto.PatientName = patientReader.GetString("Name");
                                            dto.dateOfBirth = patientReader.GetDateTime("Date_of_Birth").ToString("yyyy-MM-dd");
                                            dto.ContactNumber = patientReader.GetString("Contact_Number");
                                        }
                                    }
                                }
                            }

                            // Get doctor details
                            using (MySqlConnection c3 = con.GetConnection()) {
                                c3.Open();
                                String sqlGetDoctorName = "SELECT * FROM doctors WHERE doctor_id=@doctor_id";
                                using (MySqlCommand cmd3 = new MySqlCommand(sqlGetDoctorName, c3))
                                {
                                    cmd3.Parameters.AddWithValue("@doctor_id", doctor_id);
                                    MySqlDataReader doctorReader = cmd3.ExecuteReader();
                                    while (doctorReader.Read())
                                    {
                                        if (doctorReader.HasRows)
                                        {
                                            dto.DoctorName = doctorReader.GetString("Name");
                                            dto.Specialty = doctorReader.GetString("Specialty");
                                        }
                                    }
                                }
                            }

                            dto.DoctorId = doctor_id;
                            dto.PatientId = patient_id;
                            dto.VisitId = id;
                            dto.DateOfVisit = date_of_visit;
                            dto.Diagnosis = Diagnosis;
                            dto.Treatment = Treatment;

                            list.Add(dto);
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
        public void update_visits(String treatment,String diagnosis,int id)
        {
            String sqlUpdateVisit = "UPDATE visits SET Treatment=@Treatment, Diagnosis=@diagnosis WHERE visit_id=@visit_id";
            try
            {
                using (MySqlConnection c = con.GetConnection())
                {

                    using (MySqlCommand cmd = new MySqlCommand(sqlUpdateVisit, c))
                    {
                        cmd.Parameters.AddWithValue("@Treatment", treatment);
                        cmd.Parameters.AddWithValue("@diagnosis", diagnosis);
                        cmd.Parameters.AddWithValue("@visit_id", id);

                        c.Open();
                        int row = cmd.ExecuteNonQuery();

                        if (row > 0)
                        {
                            MessageBox.Show("Successfully updated!");
                        }
                        else MessageBox.Show("visit is already taken");
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

        public void DeleteVisit(int visit_id)
        {
            String sqlDeleteVisit = "DELETE FROM visits WHERE visit_id=@visit_id";
            using (MySqlConnection c = con.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlDeleteVisit, c))
                {
                    cmd.Parameters.AddWithValue("@visit_id", visit_id);
                    try
                    {
                        c.Open();
                        int row = 0;
                        row = cmd.ExecuteNonQuery();
                        if (row > 0)
                        {
                            MessageBox.Show("Successfully deleted!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public List<visitsDTO> search_visit(String search)
        {
            string sql = @"
                SELECT v.visit_id, v.patient_id, v.doctor_id, v.date_of_visit, 
                       v.Diagnosis, v.Treatment,
                       p.Name AS PatientName, p.Date_of_Birth, p.Contact_Number,
                       d.Name AS DoctorName, d.Specialty
                FROM visits v
                JOIN patients p ON v.patient_id = p.patient_id
                JOIN doctors d ON v.doctor_id = d.doctor_id
                WHERE v.visit_id LIKE @keyword
                   OR v.patient_id LIKE @keyword
                   OR v.doctor_id LIKE @keyword
                   OR v.Diagnosis LIKE @keyword
                   OR v.Treatment LIKE @keyword
                   OR p.Name LIKE @keyword
                   OR d.Name LIKE @keyword
                   OR d.Specialty LIKE @keyword";

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, c))
                    {
                        string keyword = "%" + search + "%";
                        cmd.Parameters.AddWithValue("@keyword", keyword);
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<visitsDTO> list = new List<visitsDTO>();
                        while (reader.Read())
                        {
                            visitsDTO dto = new visitsDTO
                            {
                                VisitId = reader.GetInt32("visit_id"),
                                PatientId = reader.GetInt32("patient_id"),
                                DoctorId = reader.GetInt32("doctor_id"),
                                DateOfVisit = reader.GetDateTime("date_of_visit"),
                                Diagnosis = reader.GetString("Diagnosis"),
                                Treatment = reader.GetString("Treatment"),
                                PatientName = reader.GetString("PatientName"),
                                dateOfBirth = reader.GetDateTime("Date_of_Birth").ToString("yyyy-MM-dd"),
                                ContactNumber = reader.GetString("Contact_Number"),
                                DoctorName = reader.GetString("DoctorName"),
                                Specialty = reader.GetString("Specialty")
                            };

                            list.Add(dto);
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
