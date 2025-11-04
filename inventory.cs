using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MPHospitalRecordsSystem
{
    internal class inventory
    {
        public string MedicineName;
        public string GenericName;
        public string DosageForm;
        public int StockQuantity;

        private string sqlInsertInventory = "INSERT INTO inventory (MedicineName, GenericName, DosageForm, StockQuantity) VALUES (@MedicineName, @GenericName, @DosageForm, @StockQuantity)";
        private string sqlSearchIfAlready = "SELECT * FROM inventory WHERE MedicineName=@MedicineName";
        connection con = new connection();

        public void add_inventory(string MedicineName, string GenericName, string DosageForm, int StockQuantity)
        {
            bool check = true;

            if (string.IsNullOrWhiteSpace(MedicineName) || string.IsNullOrWhiteSpace(GenericName) || string.IsNullOrWhiteSpace(DosageForm))
            {
                MessageBox.Show("Please fill out all required fields.");
                check = false;
            }

            if (StockQuantity < 0)
            {
                MessageBox.Show("Stock quantity cannot be negative.");
                check = false;
            }

            if (check)
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    try
                    {
                        c.Open();

                        // Check if medicine already exists
                        using (MySqlCommand checkMed = new MySqlCommand(sqlSearchIfAlready, c))
                        {
                            checkMed.Parameters.AddWithValue("@MedicineName", MedicineName);
                            object data = checkMed.ExecuteScalar();

                            if (data != null)
                            {
                                MessageBox.Show("Medicine already exists in inventory.");
                                return;
                            }
                        }

                        // Insert medicine
                        using (MySqlCommand cmd = new MySqlCommand(sqlInsertInventory, c))
                        {
                            cmd.Parameters.AddWithValue("@MedicineName", MedicineName);
                            cmd.Parameters.AddWithValue("@GenericName", GenericName);
                            cmd.Parameters.AddWithValue("@DosageForm", DosageForm);
                            cmd.Parameters.AddWithValue("@StockQuantity", StockQuantity);

                            int row = cmd.ExecuteNonQuery();

                            if (row > 0)
                            {
                                MessageBox.Show("Medicine successfully added to inventory!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to add medicine.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public List<inventoryDTO> read_inventory()
        {
            string sqlSelect = "SELECT * FROM inventory";
            List<inventoryDTO> list = new List<inventoryDTO>();

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSelect, c))
                    {
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            list.Add(new inventoryDTO
                            {
                                Id = reader.GetInt32("id"),
                                MedicineName = reader.GetString("MedicineName"),
                                GenericName = reader.GetString("GenericName"),
                                DosageForm = reader.GetString("DosageForm"),
                                StockQuantity = reader.GetInt32("StockQuantity")
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<inventoryDTO> search_inventory(string search)
        {
            string sqlSearch = "SELECT * FROM inventory WHERE MedicineName LIKE @search OR GenericName LIKE @search";
            List<inventoryDTO> list = new List<inventoryDTO>();

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlSearch, c))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                        c.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            list.Add(new inventoryDTO
                            {
                                Id = reader.GetInt32("id"),
                                MedicineName = reader.GetString("MedicineName"),
                                GenericName = reader.GetString("GenericName"),
                                DosageForm = reader.GetString("DosageForm"),
                                StockQuantity = reader.GetInt32("StockQuantity")
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void update_inventory(int id, string MedicineName, string GenericName, string DosageForm, int StockQuantity)
        {
            string sqlUpdate = "UPDATE inventory SET MedicineName=@MedicineName, GenericName=@GenericName, DosageForm=@DosageForm, StockQuantity=@StockQuantity WHERE id=@id";

            try
            {
                using (MySqlConnection c = con.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlUpdate, c))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@MedicineName", MedicineName);
                        cmd.Parameters.AddWithValue("@GenericName", GenericName);
                        cmd.Parameters.AddWithValue("@DosageForm", DosageForm);
                        cmd.Parameters.AddWithValue("@StockQuantity", StockQuantity);

                        c.Open();
                        int row = cmd.ExecuteNonQuery();

                        if (row > 0)
                        {
                            MessageBox.Show("Inventory successfully updated!");
                        }
                        else
                        {
                            MessageBox.Show("No record updated.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void delete_inventory(int id)
        {
            string sqlDelete = "DELETE FROM inventory WHERE id=@id";

            using (MySqlConnection c = con.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlDelete, c))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        c.Open();
                        int row = cmd.ExecuteNonQuery();

                        if (row > 0)
                        {
                            MessageBox.Show("Medicine successfully deleted!");
                        }
                        else
                        {
                            MessageBox.Show("No record found to delete.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public int get_next_id()
        {
            string sqlNextId = "SELECT MAX(id) FROM inventory";
            using (MySqlConnection c = con.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlNextId, c))
                {
                    c.Open();
                    object result = cmd.ExecuteScalar();
                    int nextId = (result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;
                    return nextId;
                }
            }
        }
    }
}
