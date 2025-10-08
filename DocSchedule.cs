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
        public List<DocScheduleDTO> get_doctor_all_schedule(int id)
        {
            string sql = "SELECT id AS schedule_id, doctor_id, date AS available_date, time AS available_time FROM Schedule WHERE doctor_id = @doctor_id";

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, c))
                    {
                        cmd.Parameters.AddWithValue("@doctor_id", id);
                        c.Open();

                        List<DocScheduleDTO> list = new List<DocScheduleDTO>();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DocScheduleDTO schedule = new DocScheduleDTO
                                {
                                    ScheduleId = reader.GetInt32("schedule_id"),
                                    DoctorId = reader["doctor_id"].ToString(),
                                    AvailableDate = reader.GetDateTime("available_date"),
                                    AvailableTime = reader.GetTimeSpan("available_time")
                                };

                                list.Add(schedule);
                            }
                        }

                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return new List<DocScheduleDTO>();
            }
        }

        public void update_Schedule(DateTime date, DateTime time, int doc_id, int sched_id)
        {
            //@doctor_id,@date,@time
            String sqlUpdateSchedule = "UPDATE Schedule SET doctor_id=@doctor_id, date=@date, time=@time WHERE id=@sched_id";
            try
            {
                using (MySqlConnection c = con.GetConnection())
                {

                    using (MySqlCommand cmd = new MySqlCommand(sqlUpdateSchedule, c))
                    {
                        cmd.Parameters.AddWithValue("@doctor_id", doc_id);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@sched_id", sched_id);
                        cmd.Parameters.AddWithValue("@time", time);

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
        public List<DocScheduleDTO> read_schedule() {
            string sqlSelectSchedule =
                "SELECT s.id AS schedule_id, s.doctor_id, s.date AS available_date, s.time AS available_time, " +
                "d.Name AS doctor_name, d.Specialty " +
                "FROM Schedule s " +
                "JOIN doctors d ON s.doctor_id = d.doctor_id";


            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSelectSchedule, c))
                    {
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<DocScheduleDTO> list = new List<DocScheduleDTO>();
                        while (reader.Read())
                        {
                            DocScheduleDTO schedule = new DocScheduleDTO
                            {
                                ScheduleId = reader.GetInt32("schedule_id"),
                                DoctorId = reader["doctor_id"].ToString(),
                                DoctorName = reader["doctor_name"].ToString(),
                                Specialty = reader["Specialty"].ToString(),
                                AvailableDate = reader.GetDateTime("available_date"),
                                AvailableTime = reader.GetTimeSpan("available_time")
                            };
                            list.Add(schedule);
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
        public String get_next_id()
        {

            String sqlNextId = "SELECT MAX(id) from Schedule ";
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
        public void delete_Schedule(int id)
        {
            String sqlDeleteSchedule = "DELETE FROM Schedule WHERE id=@id";
            using (MySqlConnection c = con.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlDeleteSchedule, c))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
    }
}
