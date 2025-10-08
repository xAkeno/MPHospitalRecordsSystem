using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
namespace MPHospitalRecordsSystem
{
    internal class DocSchedule
    {
        connection con = new connection();
        public String sqlInsertSchedule = "INSERT INTO Schedule (doctor_id,date,time) VALUES (@doctor_id,@date,@time)";
        public String sqlSearchIfAlready = "SELECT * FROM Schedule WHERE Date=@Date AND time=@time";

        public void add_Schedule(DateTime date, DateTime time, int doc_id)
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
                        using (MySqlCommand checkName = new MySqlCommand(sqlSearchIfAlready, c))
                        {
                            checkName.Parameters.AddWithValue("@date", date);
                            checkName.Parameters.AddWithValue("@time", time);
                            object data = checkName.ExecuteScalar();

                            if (data != null)
                            {
                                MessageBox.Show("Schedule is already taken");
                                return;
                            }
                        }

                        using (MySqlCommand cmd = new MySqlCommand(sqlInsertSchedule, c))
                        {
                            cmd.Parameters.AddWithValue("@doctor_id", doc_id);
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@time", time);

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
    }
}
