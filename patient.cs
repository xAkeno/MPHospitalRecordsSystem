using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace MPHospitalRecordsSystem
{
    internal class patient
    {
        public String name;
        public String date_of_birth;
        public String contact_number;

        public String sqlInsertPatient = "INSERT INTO patients (Name,date_of_birth,contact_number) VALUES (@Name,@date_of_birth,@contact_number)";
        public String sqlSearchIfAlready = "SELECT * FROM patients WHERE Name=@Name";
        connection con = new connection();


        public void add_patient(String name,String date_of_birth,String contact_number)
        {
            bool check = true;
            if (name.Equals("") && date_of_birth.Equals("") && contact_number.Equals(""))
            {
                MessageBox.Show("Please answer all the forms");
                check = false;
            }

            if (name.Equals("") || date_of_birth.Equals("") || contact_number.Equals(""))
            {
                //MessageBox.Show(
                //    "Please answer all the required form listed here ?\n"
                //    + (string.IsNullOrWhiteSpace(name) ? "enter a name\n" : "")
                //    + (string.IsNullOrWhiteSpace(date_of_birth) ? "enter a birthday\n" : "")
                //);
                check = false;
            }

            if (check)
            {
                using (MySqlConnection c = con.GetConnection()) {
                    try
                    {
                        c.Open();
                        using (MySqlCommand checkName = new MySqlCommand(sqlSearchIfAlready, c))
                        {
                            checkName.Parameters.AddWithValue("@Name", name);

                            object data = checkName.ExecuteScalar();

                            if (data != null) {
                                MessageBox.Show("Patient is already taken");
                                return;
                            }
                        }

                        using (MySqlCommand cmd = new MySqlCommand(sqlInsertPatient, c))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@date_of_birth", date_of_birth);
                            cmd.Parameters.AddWithValue("@contact_number", contact_number);

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

        public List<patientDTO> read_patient()
        {
            String sqlSelectPatient = "SELECT * FROM patients";
            try
            {
                using (MySqlConnection c = con.GetConnection()) {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSelectPatient, c))
                    {
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<patientDTO> list = new List<patientDTO>();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("patient_id");
                            String name = reader.GetString("Name");
                            DateTime date_of_birth = reader.GetDateTime("date_of_birth");
                            String contact_number = reader.GetString("contact_number");
                            list.Add(new patientDTO
                            {   
                                PatientId = id
                                ,
                                Name = name
                                ,
                                DateOfBirth = date_of_birth
                                ,
                                ContactNumber = contact_number
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
        public List<patientDTO> search_patient(String search)
        {
            String sqlSearchPatient = "SELECT * FROM patients WHERE Name LIKE @search OR patient_id LIKE @search";
            try
            {
                using (MySqlConnection c = con.GetConnection()) {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSearchPatient, c))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<patientDTO> list = new List<patientDTO>();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("patient_id");
                            String name = reader.GetString("Name");
                            DateTime date_of_birth = reader.GetDateTime("date_of_birth");
                            String contact_number = reader.GetString("contact_number");
                            list.Add(new patientDTO
                            {
                                PatientId = id
                                ,
                                Name = name
                                ,
                                DateOfBirth = date_of_birth
                                ,
                                ContactNumber = contact_number
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
        public bool check_if_info_is_already_registred(int id,String name) {
            String sqlCheck = "SELECT * FROM patients WHERE patient_id=@id OR Name=@Name";
            if (id == 0 && name.Equals(""))
            {
                return false;
            }
            else {
                using (MySqlConnection c = con.GetConnection()) {
                    using (MySqlCommand cmd = new MySqlCommand(sqlCheck, c)) {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Name", name);

                        c.Open();

                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows) {
                            return true;
                        }
                    }
                
                }
            }

            return false;
        }

        public String get_next_id() {

            String sqlNextId = "SELECT MAX(patient_id) from patients ";
            using (MySqlConnection c = con.GetConnection()) {
                using (MySqlCommand cmd = new MySqlCommand(sqlNextId, c)) {
                    c.Open();
                    object result = cmd.ExecuteScalar();

                    int nextId = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 0;

                    return Convert.ToString(nextId);
                }
            }

            return null;
        }
        public void update_patient(String name, String date_of_birth, String contact_number, int id)
        {
            String sqlUpdatePatient = "UPDATE patients SET Name=@Name, date_of_birth=@date_of_birth, contact_number=@contact_number WHERE patient_id=@patient_id";
            try
            {
                using (MySqlConnection c = con.GetConnection()) {

                    using (MySqlCommand cmd = new MySqlCommand(sqlUpdatePatient, c))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@date_of_birth", date_of_birth);
                        cmd.Parameters.AddWithValue("@patient_id", id);
                        cmd.Parameters.AddWithValue("@contact_number", contact_number);

                        c.Open();
                        int row = cmd.ExecuteNonQuery();

                        if (row > 0)
                        {
                            MessageBox.Show("Successfully updated!");
                        }
                       // else MessageBox.Show("Patient is already taken");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        public int get_patient_id_by_name(String name)
        {
            String sqlGetId = "SELECT patient_id FROM patients WHERE Name=@Name";
            using (MySqlConnection c = con.GetConnection()) {
                using (MySqlCommand cmd = new MySqlCommand(sqlGetId, c))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    c.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0;
        }
        public void delete_patient(int patient_id)
        {
            String sqlDeletePatient = "DELETE FROM patients WHERE patient_id=@patient_id";
            String checkVisit = "SELECT * FROM visits WHERE patient_id = @patient_id";

            using (MySqlConnection c = con.GetConnection()) {
                using (MySqlCommand cmd = new MySqlCommand(sqlDeletePatient, c))
                {
                    using(MySqlCommand cd =  new MySqlCommand(checkVisit, c))
                    {
                        try
                        {
                            cd.Parameters.AddWithValue("@patient_id", patient_id);
                            c.Open();
                            MySqlDataReader reader = cd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                MessageBox.Show("cannot delete patient with visit record");
                                return;
                            }
                            reader.Close();
                            c.Close();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }


                    cmd.Parameters.AddWithValue("@patient_id", patient_id);
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
    }
}
