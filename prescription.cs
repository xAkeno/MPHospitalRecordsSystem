using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;
using System.Windows.Forms;

namespace MPHospitalRecordsSystem
{
    internal class Prescription
    {
        connection con = new connection();

        // Generate a new Prescription ID based on date and sequence
        private string GeneratePrescriptionId()
        {
            return GetNextPrescriptionId();
        }

        // Get next prescription ID
        public string GetNextPrescriptionId()
        {
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string sql = "SELECT MAX(PrescriptionID) FROM prescription WHERE PrescriptionID LIKE @datePrefix";

            using (MySqlConnection c = con.GetConnection())
            {
                c.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("@datePrefix", $"PRE-{datePart}-%");
                    object result = cmd.ExecuteScalar();

                    int nextSequence = 1;
                    if (result != DBNull.Value && result != null)
                    {
                        string lastId = result.ToString(); // format: PRE-YYYYMMDD-XXX
                        string[] parts = lastId.Split('-');
                        if (parts.Length == 3 && int.TryParse(parts[2], out int lastSeq))
                        {
                            nextSequence = lastSeq + 1;
                        }
                    }

                    return $"PRE-{datePart}-{nextSequence:D3}";
                }
            }
        }


        // Add prescription with multiple medicines
        public void AddPrescription(int validUntil, List<PrescriptionDTO> medicines)
        {
            if (medicines == null || medicines.Count == 0)
            {
                MessageBox.Show("Add at least one medicine.");
                return;
            }

            string prescriptionId = GeneratePrescriptionId();

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    c.Open();
                    using (var transaction = c.BeginTransaction())
                    {
                        // Insert into prescription table
                        string sqlPrescription = "INSERT INTO prescription (PrescriptionID, ValidUntil) VALUES (@PrescriptionID, @ValidUntil)";
                        using (MySqlCommand cmd = new MySqlCommand(sqlPrescription, c, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                            cmd.Parameters.AddWithValue("@ValidUntil", validUntil);
                            cmd.ExecuteNonQuery();
                        }

                        // Insert into prescription_medicine table
                        string sqlMedicine = @"INSERT INTO prescription_medicine 
                                               (PrescriptionID,InventoryID, Dosage, Frequency, Duration, Quantity, Instructions, doctor_id, patient_id)
                                               VALUES (@PrescriptionID, @InventoryID, @Dosage, @Frequency, @Duration, @Quantity, @Instructions, @doctor_id, @patient_id)";
                        foreach (var med in medicines)
                        {

                            string getInventoryIdSql = "SELECT id FROM inventory WHERE MedicineName = @MedicineName LIMIT 1";
                            int inventoryId = 0;

                            using (MySqlCommand cmdGet = new MySqlCommand(getInventoryIdSql, c, transaction))
                            {
                                cmdGet.Parameters.AddWithValue("@MedicineName", med.MedicineName);
                                object result = cmdGet.ExecuteScalar();
                                if (result != null)
                                {
                                    inventoryId = Convert.ToInt32(result);
                                }
                                else
                                {
                                    MessageBox.Show($"Medicine '{med.MedicineName}' not found in inventory.");
                                    continue;
                                }
                            }

                            patient p = new patient();
                            doctor d = new doctor();

                            int patientId = p.findByName(med.patient);
                            int doctorId = d.findByName(med.doctor);

                            using (MySqlCommand cmd = new MySqlCommand(sqlMedicine, c, transaction))
                            {
                                cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                                cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                                cmd.Parameters.AddWithValue("@Dosage", med.Dosage);
                                cmd.Parameters.AddWithValue("@Frequency", med.Frequency);
                                cmd.Parameters.AddWithValue("@Duration", med.Duration);
                                cmd.Parameters.AddWithValue("@Quantity", med.Quantity);
                                cmd.Parameters.AddWithValue("@Instructions", med.Instructions);
                                cmd.Parameters.AddWithValue("@patient_id", patientId);
                                cmd.Parameters.AddWithValue("@doctor_id", doctorId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }

                    MessageBox.Show($"Prescription {prescriptionId} added successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding prescription: " + ex.Message);
            }
        }
        public List<PrescriptionDTO> ReadPrescriptions()
        {
            string sql = @"SELECT 
                                p.PrescriptionID, 
                                p.ValidUntil, 
                                pm.InventoryID AS MedicineID, 
                                pm.Dosage, 
                                pm.Frequency, 
                                pm.Duration, 
                                pm.Quantity, 
                                pm.Instructions, 
                                pm.patient_id,
                                pm.doctor_id,
                                i.MedicineName
                            FROM prescription p
                            JOIN prescription_medicine pm ON p.PrescriptionID = pm.PrescriptionID
                            JOIN inventory i ON pm.InventoryID = i.id
                            ORDER BY p.PrescriptionID DESC;
                            ";

            List<PrescriptionDTO> list = new List<PrescriptionDTO>();

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    c.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, c))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patient p = new patient().findNameById(reader.GetInt32("doctor_id"));
                                doctorDTO d = new doctor().findNameById(reader.GetInt32("patient_id"));
                                list.Add(new PrescriptionDTO
                                {
                                    PrescriptionID = reader.GetString("PrescriptionID"),
                                    ValidUntil = reader.GetInt32("ValidUntil"),
                                    MedicineID = reader.GetInt32("MedicineID"),
                                    MedicineName = reader.GetString("MedicineName"),
                                    Dosage = reader.GetString("Dosage"),
                                    Frequency = reader.GetInt32("Frequency"),
                                    Duration = reader.GetInt32("Duration"),
                                    Quantity = reader.GetInt32("Quantity"),
                                    Instructions = reader.GetString("Instructions"),
                                    patient = p != null ? p.name : "Unknown",     // safe null check
                                    doctor = d != null ? d.DoctorName : "Unknown", // safe null check
                                });

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading prescriptions: " + ex.Message);
            }

            return list;
        }

        // Delete prescription (all medicines included due to cascade)
        public void DeletePrescription(string prescriptionId)
        {
            string sql = "DELETE FROM prescription WHERE PrescriptionID=@PrescriptionID";

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    c.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, c))
                    {
                        cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            MessageBox.Show("Prescription deleted successfully!");
                        else
                            MessageBox.Show("No record found for the given ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting prescription: " + ex.Message);
            }
        }
        public List<PrescriptionDTO> SearchPrescriptions(string search)
        {
            string sql = @"SELECT 
                        p.PrescriptionID, 
                        p.ValidUntil, 
                        pm.InventoryID AS MedicineID, 
                        pm.Dosage, 
                        pm.Frequency, 
                        pm.Duration, 
                        pm.Quantity, 
                        pm.Instructions, 
                        pm.patient_id,
                        pm.doctor_id,
                        i.MedicineName,
                        pat.Name AS PatientName,
                        doc.Name AS Name
                    FROM prescription p
                    JOIN prescription_medicine pm ON p.PrescriptionID = pm.PrescriptionID
                    JOIN inventory i ON pm.InventoryID = i.id
                    LEFT JOIN patients pat ON pm.patient_id = pat.patient_id
                    LEFT JOIN doctors doc ON pm.doctor_id = doc.doctor_id
                    WHERE p.PrescriptionID LIKE @search
                       OR i.MedicineName LIKE @search
                       OR pat.Name LIKE @search
                       OR doc.Name LIKE @search
                    ORDER BY p.PrescriptionID DESC;";

            List<PrescriptionDTO> list = new List<PrescriptionDTO>();

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    c.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, c))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new PrescriptionDTO
                                {
                                    PrescriptionID = reader.GetString("PrescriptionID"),
                                    ValidUntil = reader.GetInt32("ValidUntil"),
                                    MedicineID = reader.GetInt32("MedicineID"),
                                    MedicineName = reader.GetString("MedicineName"),
                                    Dosage = reader.GetString("Dosage"),
                                    Frequency = reader.GetInt32("Frequency"),
                                    Duration = reader.GetInt32("Duration"),
                                    Quantity = reader.GetInt32("Quantity"),
                                    Instructions = reader.GetString("Instructions"),
                                    patient = reader["PatientName"] != DBNull.Value ? reader.GetString("Name") : "Unknown",
                                    doctor = reader["Name"] != DBNull.Value ? reader.GetString("Name") : "Unknown"
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching prescriptions: " + ex.Message);
            }

            return list;
        }

        public void UpdatePrescription(string prescriptionId, List<PrescriptionDTO> medicines)
        {
            if (medicines == null || medicines.Count == 0)
            {
                MessageBox.Show("Add at least one medicine.");
                return;
            }

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    c.Open();
                    using (var transaction = c.BeginTransaction())
                    {
                        foreach (var med in medicines)
                        {
                            // Get InventoryID from MedicineName
                            string getInventoryIdSql = "SELECT id FROM inventory WHERE MedicineName = @MedicineName LIMIT 1";
                            int inventoryId = 0;

                            using (MySqlCommand cmdGet = new MySqlCommand(getInventoryIdSql, c, transaction))
                            {
                                cmdGet.Parameters.AddWithValue("@MedicineName", med.MedicineName);
                                object result = cmdGet.ExecuteScalar();
                                if (result != null)
                                    inventoryId = Convert.ToInt32(result);
                                else
                                {
                                    MessageBox.Show($"Medicine '{med.MedicineName}' not found in inventory.");
                                    continue; // skip invalid entry
                                }
                            }

                            // Check if record already exists for this prescription + medicine
                            string checkSql = @"SELECT id FROM prescription_medicine 
                                        WHERE PrescriptionID = @PrescriptionID AND InventoryID = @InventoryID LIMIT 1";
                            object existingId = null;
                            using (MySqlCommand cmdCheck = new MySqlCommand(checkSql, c, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                                cmdCheck.Parameters.AddWithValue("@InventoryID", inventoryId);
                                existingId = cmdCheck.ExecuteScalar();
                            }

                            if (existingId != null)
                            {
                                // ✅ Update existing record
                                string updateSql = @"UPDATE prescription_medicine 
                                             SET Dosage=@Dosage, Frequency=@Frequency, Duration=@Duration, Quantity=@Quantity, Instructions=@Instructions
                                             WHERE id=@Id";
                                using (MySqlCommand cmdUpdate = new MySqlCommand(updateSql, c, transaction))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@Dosage", med.Dosage);
                                    cmdUpdate.Parameters.AddWithValue("@Frequency", med.Frequency);
                                    cmdUpdate.Parameters.AddWithValue("@Duration", med.Duration);
                                    cmdUpdate.Parameters.AddWithValue("@Quantity", med.Quantity);
                                    cmdUpdate.Parameters.AddWithValue("@Instructions", med.Instructions);
                                    cmdUpdate.Parameters.AddWithValue("@Id", Convert.ToInt32(existingId));
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // ➕ Insert new record if not found
                                string insertSql = @"INSERT INTO prescription_medicine 
                                             (PrescriptionID, InventoryID, Dosage, Frequency, Duration, Quantity, Instructions)
                                             VALUES (@PrescriptionID, @InventoryID, @Dosage, @Frequency, @Duration, @Quantity, @Instructions)";
                                using (MySqlCommand cmdInsert = new MySqlCommand(insertSql, c, transaction))
                                {
                                    cmdInsert.Parameters.AddWithValue("@PrescriptionID", prescriptionId);
                                    cmdInsert.Parameters.AddWithValue("@InventoryID", inventoryId);
                                    cmdInsert.Parameters.AddWithValue("@Dosage", med.Dosage);
                                    cmdInsert.Parameters.AddWithValue("@Frequency", med.Frequency);
                                    cmdInsert.Parameters.AddWithValue("@Duration", med.Duration);
                                    cmdInsert.Parameters.AddWithValue("@Quantity", med.Quantity);
                                    cmdInsert.Parameters.AddWithValue("@Instructions", med.Instructions);
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }

                    MessageBox.Show($"Prescription {prescriptionId} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating prescription: " + ex.Message);
            }
        }
    }


        public class PrescriptionMedicineDTO
    {
        public int MedicineID { get; set; }
        public string Dosage { get; set; }
        public int Frequency { get; set; }
        public int Duration { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
    }

    public class PrescriptionDTO
    {
        public string PrescriptionID { get; set; }
        public int ValidUntil { get; set; }
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public int Frequency { get; set; }
        public int Duration { get; set; }
        public int Quantity { get; set; }
        public String patient { get; set; }
        public String doctor { get; set; }
        public string Instructions { get; set; }
    }
}
