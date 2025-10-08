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
        public string sqlInsertAppoint = "INSERT INTO Appointment (patient_id, doctor_id, date, time, status) VALUES (@patient_id, @doctor_id, @date, @time, @status)";
        public string sqlSearchIfAlready = "SELECT * FROM Appointment WHERE patient_id=@patient_id AND doctor_id=@doctor_id AND date=@date AND time=@time";
        public string sqlUpdateAppoint = "UPDATE Appointment SET doctor_id=@doctor_id, date=@date, time=@time, status=@status WHERE id=@id";
        public string sqlDeleteAppoint = "DELETE FROM Appointment WHERE id=@id";
        public string sqlSelectAll = "SELECT * FROM Appointment";

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
                            cmd.Parameters.AddWithValue("@status", Status.PENDING.ToString());


                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                using (MySqlCommand updateSchedule = new MySqlCommand(
                                                    "UPDATE Schedule SET status = 1 WHERE doctor_id = @doctor_id AND date = @date AND time = @time", c))
                                {
                                    updateSchedule.Parameters.AddWithValue("@doctor_id", doc_id);
                                    updateSchedule.Parameters.AddWithValue("@date", date);
                                    updateSchedule.Parameters.AddWithValue("@time", time);

                                    updateSchedule.ExecuteNonQuery();
                                }
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
        public void update_appointment(int appointment_id, int doc_id, DateTime date, DateTime time, string status)
        {
            using (MySqlConnection c = con.GetConnection())
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlUpdateAppoint, c))
                    {
                        cmd.Parameters.AddWithValue("@id", appointment_id);
                        cmd.Parameters.AddWithValue("@doctor_id", doc_id);
                        cmd.Parameters.AddWithValue("@date", date.Date);
                        cmd.Parameters.AddWithValue("@time", time.TimeOfDay);
                        cmd.Parameters.AddWithValue("@status", status);

                        c.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Appointment updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Update failed.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        public void delete_appointment(int appointment_id)
        {
            using (MySqlConnection c = con.GetConnection())
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlDeleteAppoint, c))
                    {
                        cmd.Parameters.AddWithValue("@id", appointment_id);

                        c.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Appointment deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Delete failed.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        public List<appointmentDTO> search_appointments(string patientName = "", int? doctorId = null, DateTime? date = null, string status = "")
        {
            List<appointmentDTO> list = new List<appointmentDTO>();

            StringBuilder sql = new StringBuilder(
                "SELECT a.id, a.patient_id, a.doctor_id, a.date, a.time, a.status " +
                "FROM Appointment a " +
                "JOIN patients p ON a.patient_id = p.patient_id " +
                "WHERE 1=1"
            );

            if (!string.IsNullOrWhiteSpace(patientName))
                sql.Append(" AND p.Name LIKE @patientName");

            if (doctorId.HasValue)
                sql.Append(" AND a.doctor_id = @doctorId");

            if (date.HasValue)
                sql.Append(" AND a.date = @date");

            if (!string.IsNullOrWhiteSpace(status))
                sql.Append(" AND a.status = @status");

            using (MySqlConnection c = con.GetConnection())
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), c))
                    {
                        if (!string.IsNullOrWhiteSpace(patientName))
                            cmd.Parameters.AddWithValue("@patientName", "%" + patientName + "%");

                        if (doctorId.HasValue)
                            cmd.Parameters.AddWithValue("@doctorId", doctorId.Value);

                        if (date.HasValue)
                            cmd.Parameters.AddWithValue("@date", date.Value.Date);

                        if (!string.IsNullOrWhiteSpace(status))
                            cmd.Parameters.AddWithValue("@status", status);

                        c.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patient p = new patient();
                                patientDTO patientInfo = p.find_patient_by_id(reader.GetInt32("patient_id"));

                                appointmentDTO dto = new appointmentDTO
                                {
                                    AppointmentId = reader.GetInt32("id"),
                                    PatientId = reader.GetInt32("patient_id"),
                                    PatientName = patientInfo.Name,
                                    ContactNumber = patientInfo.ContactNumber,
                                    DateOfBirth = patientInfo.DateOfBirth,
                                    DoctorId = reader.GetInt32("doctor_id"),
                                    AppointmentDate = reader.GetDateTime("date"),
                                    AppointmentTime = reader.GetTimeSpan("time"),
                                    Status = reader.GetString("status")
                                };
                                list.Add(dto);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return list;
        }

        public List<appointmentDTO> read_all_appointments()
        {
            List<appointmentDTO> list = new List<appointmentDTO>();

            using (MySqlConnection c = con.GetConnection())
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSelectAll, c))
                    {
                        c.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patient p = new patient();

                                patientDTO patientInfo = p.find_patient_by_id(reader.GetInt32("patient_id"));
                                appointmentDTO dto = new appointmentDTO
                                {
                                    AppointmentId = reader.GetInt32("id"),
                                    PatientId = reader.GetInt32("patient_id"),
                                    PatientName = patientInfo.Name,
                                    ContactNumber = patientInfo.ContactNumber,
                                    DateOfBirth = patientInfo.DateOfBirth,
                                    DoctorId = reader.GetInt32("doctor_id"),
                                    AppointmentDate = reader.GetDateTime("date"),
                                    AppointmentTime = reader.GetTimeSpan("time"),
                                    Status = reader.GetString("status")
                                };
                                list.Add(dto);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return list;
        }
    }
}
