using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace MPHospitalRecordsSystem
{
    internal class appointment
    {
        connection con = new connection();
        public String sqlInsertAppoint = "INSERT INTO Appointment (patient_id,doctor_id,date,time) VALUES (@patient_id,@doctor_id,@date,@time)";
        public String sqlSearchIfAlready = "SELECT * FROM Appointment WHERE patient_id=@patient_id AND doctor_id=@doctor_id AND date=@date AND time=@time";

        public void add_appoint(DateTime date, DateTime time, int doc_id,int patient_id)
        {
            bool check = true;
            if (date.Equals("") && time.Equals("") && doc_id.Equals(""))
            {
                MessageBox.Show("Please answer all the forms");
                check = false;
            }

            if (date.Equals("") && time.Equals("") && doc_id.Equals(""))
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

                using (MySqlConnection c = con.GetConnection())
                {
                    try
                    {
                        c.Open();
                        using (MySqlCommand checkIfExists = new MySqlCommand(sqlSearchIfAlready, c))
                        {
                            checkIfExists.Parameters.AddWithValue("@patient_id", patient_id);
                            checkIfExists.Parameters.AddWithValue("@doctor_id", doc_id);
                            checkIfExists.Parameters.AddWithValue("@date", date.Date);
                            checkIfExists.Parameters.AddWithValue("@time", time.TimeOfDay);

                            using (MySqlDataReader reader = checkIfExists.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    MessageBox.Show("This appointment already exists.");
                                    return;
                                }
                            }
                        }

                        using (MySqlCommand cmd = new MySqlCommand(sqlInsertAppoint, c))
                        {
                            cmd.Parameters.AddWithValue("@patient_id", patient_id);
                            cmd.Parameters.AddWithValue("@doctor_id", doc_id);
                            cmd.Parameters.AddWithValue("@date", date.Date);
                            cmd.Parameters.AddWithValue("@time", time.TimeOfDay);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Appointment successfully scheduled!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to schedule appointment.");
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
    }
}
