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
        public String address;
        public String date_of_birth;
        public String contact_number;

        public String sqlInsertPatient = "INSERT INTO (Name,date_of_birth,contact_number) VALUES (@Name,@date_of_birth,@contact_number)";
        connection con = new connection();
        public void add_patient(String name,String address,String date_of_birth,String contact_number)
        {
            bool check = true;
            if (name.Equals("") && address.Equals("") && date_of_birth.Equals("") && contact_number.Equals("")) {
                MessageBox.Show("Please complete all the form");
                check = false;
            }

            if (name.Equals("") || address.Equals("") || date_of_birth.Equals("") || contact_number.Equals("")) {
                MessageBox.Show("Please complete all the list that say here? \n" 
                    + name.Equals("") ?? "Fill the name" + "\n" 
                    + address.Equals("") ?? "Fill the address" + "\n" 
                    + date_of_birth.Equals("") ?? "Fill the date of birth" + "\n" 
                    + contact_number.Equals("") ?? "Fill the contact number");
                check = false;
            }

            if (check)
            {
                MySqlCommand cmd = new MySqlCommand(sqlInsertPatient, con.GetConnection());
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("date_of_birth", date_of_birth);
                cmd.Parameters.AddWithValue("contact_number", contact_number);


                try
                {
                    con.GetConnection().Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    String checkUsername = "";
                    bool alreadyTake;
                    int row = 0;
                    while (reader.Read())
                    {
                        checkUsername = reader.GetString("username");
                    }
                    alreadyTake = checkUsername.Equals(name);
                    if (!alreadyTake)
                    {
                        reader.Close();
                        row = cmd.ExecuteNonQuery();
                    }
                    if (row > 0 && !alreadyTake)
                    {
                        MessageBox.Show("Successfully registered!");
                    }
                    else MessageBox.Show("Patient is already taken");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public List<patientDTO> read_patient()
        {
            String sqlSelectPatient = "SELECT * FROM patient";
            MySqlCommand cmd = new MySqlCommand(sqlSelectPatient, con.GetConnection());
            try
            {
                con.GetConnection().Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<patientDTO> list = new List<patientDTO>();
                while (reader.Read())
                {
                    String name = reader.GetString("name");
                    String address = reader.GetString("address");
                    String date_of_birth = reader.GetString("date_of_birth");
                    String contact_number = reader.GetString("contact_number");
                    list.Add(new patientDTO { 
                        Name = name
                        , DateOfBirth = Convert.ToDateTime(date_of_birth)
                        , ContactNumber = contact_number 
                    });
                }
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
