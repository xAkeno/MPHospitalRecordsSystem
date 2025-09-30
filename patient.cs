using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
            if (name.Equals("") && date_of_birth.Equals("") && contact_number.Equals("")) {
                MessageBox.Show("Please complete all the form");
                check = false;
            }

            if (name.Equals("") || date_of_birth.Equals("") || contact_number.Equals("")) {
                MessageBox.Show(
                    "Please complete all the list that say here?\n"
                    + (string.IsNullOrWhiteSpace(name) ? "Fill the name\n" : "")
                    + (string.IsNullOrWhiteSpace(date_of_birth) ? "Fill the date of birth\n" : "")
                    + (string.IsNullOrWhiteSpace(contact_number) ? "Fill the contact number\n" : "")
                );
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
            MySqlCommand cmd = new MySqlCommand(sqlSelectPatient, con.GetConnection());
            try
            {
                con.GetConnection().Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<patientDTO> list = new List<patientDTO>();
                while (reader.Read())
                {
                    String name = reader.GetString("Name");
                    String date_of_birth = reader.GetString("date_of_birth");
                    String contact_number = reader.GetString("contact_numbers");
                    list.Add(new patientDTO { 
                        Name = name
                        , DateOfBirth = Convert.ToDateTime(date_of_birth)
                        , ContactNumber = contact_number 
                    });
                }
                MessageBox.Show("Rows read: " + list.Count);
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public void update_patient(String name, String address, String date_of_birth, String contact_number)
        {
            String sqlUpdatePatient = "UPDATE patient SET name=@name, address=@address, date_of_birth=@date_of_birth, contact_number=@contact_number WHERE patient_id=@patient_id";
            MySqlCommand cmd = new MySqlCommand(sqlUpdatePatient, con.GetConnection());
            cmd.Parameters.AddWithValue("Name", name);
            cmd.Parameters.AddWithValue("address", address);
            cmd.Parameters.AddWithValue("date_of_birth", date_of_birth);
            cmd.Parameters.AddWithValue("contact_number", contact_number);

            try
            {
                con.GetConnection().Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                int row = 0;
                row = cmd.ExecuteNonQuery();

                if (row > 0)
                {
                    MessageBox.Show("Successfully updated!");
                }
                else MessageBox.Show("Patient is already taken");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        public void delete_patient(String patient_id)
        {
            String sqlDeletePatient = "DELETE FROM patient WHERE patient_id=@patient_id";
            MySqlCommand cmd = new MySqlCommand(sqlDeletePatient, con.GetConnection());
            cmd.Parameters.AddWithValue("patient_id", patient_id);
            try
            {
                con.GetConnection().Open();
                MySqlDataReader reader = cmd.ExecuteReader();
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
