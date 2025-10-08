using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPHospitalRecordsSystem
{
    internal class AccInfo
    {
        public String username;
        public String password;
        connection con = new connection();


        public void addUserInfo(string username, string password, string role)
        {

            using (MySqlConnection c = con.GetConnection())
            {
                c.Open();             
                    string query = "INSERT INTO hospital_records_db.accinfo (user_name, password) " +
                                         "VALUES (@userName,  @password)";

                    using (var cmd = new MySqlCommand(query, c))
                    {

                        cmd.Parameters.Add("@userName", MySqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Successfully registered!");                           
                        }
                        else
                        {
                            MessageBox.Show("Insert failed.");
                        }
                    }               
            }
        }
        public void deleteAcc(string username, string password)
        {
            String delete = "DELETE * from accinfo WHERE user_name = @user";
            using(MySqlConnection c = con.GetConnection())
            {
                using(var cd = new MySqlCommand(delete, c))
                {
                    cd.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
                }
            }
        }           
        public bool repetitionCheck(string username)
        {
            
            string see = "SELECT * from accinfo WHERE user_name = @banana";
            using (MySqlConnection c = con.GetConnection()) {
                using (var cmdd = new MySqlCommand(see, c))
                {             
                        c.Open();

                        cmdd.Parameters.Add("@banana", MySqlDbType.VarChar).Value = username;
                        MySqlDataReader reader = cmdd.ExecuteReader();

                        if (reader.Read())
                        {
                            username = reader["user_name"].ToString();
                            MessageBox.Show("Username already exists");
                            reader.Close();
                            c.Close();   
                            return false;
                        }
                        return true;                                  
                }
            }
        }
    }
}
