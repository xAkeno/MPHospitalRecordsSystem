using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
        public String role;
        public int id;
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
        public void updateAcc(int id, string username, string password, string role)
        {
            String updAcc = "UPDATE accinfo SET user_name = @uname, password = @pass, role = @role WHERE id = @id";

            using (MySqlConnection c = con.GetConnection())
            {
                using (var cd = new MySqlCommand(updAcc, c))
                {
                    cd.Parameters.Add("@uname", MySqlDbType.VarChar).Value = username;
                    cd.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
                    cd.Parameters.Add("@role", MySqlDbType.VarChar).Value = role;
                    cd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                    c.Open();
                    int row = cd.ExecuteNonQuery();

                    if (row > 0)
                    {
                        MessageBox.Show("Successfully updated!");
                    }
                    else
                    {
                        MessageBox.Show("No record found with that ID.");
                    }
                }
            }
        }


        public String get_next_id()
        {

            String sqlNextId = "SELECT MAX(id) from accinfo ";
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
        }


        public List<UserInfoDTO> Read_acc()
        {
            String read = "SELECT * from accinfo";
            List<UserInfoDTO> list = new List<UserInfoDTO>();
            using (MySqlConnection c = con.GetConnection())
            {
                using (var cmdd = new MySqlCommand(read, c))
                {
                    c.Open();
                    MySqlDataReader reader = cmdd.ExecuteReader();
                    while (reader.Read())
                    {
                        String name = reader.GetString("user_name");
                        String role = reader.GetString("role");
                        int id = reader.GetInt32("id");
                        list.Add(new UserInfoDTO
                        {
                           
                            username = name,
                            role = role
                        });
                    }
                    return list;
                }
            }

        }
        public List<UserInfoDTO> search_Acc(string search)
        {
            String see = " SELECT FROM accinfo WHERE user_name LIKE @search OR user_name  Like @search ";

            using (MySqlConnection c = con.GetConnection())
            {
                using (var cmdd = new MySqlCommand(see, c))
                {
                    c.Open();
                    cmdd.Parameters.AddWithValue("@search", "%" + search + "%");
                    MySqlDataReader reader = cmdd.ExecuteReader();
                    List<UserInfoDTO> lists = new List<UserInfoDTO>();
                    while (reader.Read())
                    {
                        String name = reader.GetString("user_name");
                        String role = reader.GetString("role");
                        lists.Add(new UserInfoDTO { username = name, role = role });
                    }
                    return lists;
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
