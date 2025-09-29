using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace MPHospitalRecordsSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userValidation();
        }

        private void txtUname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpword_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void userValidation()
        {
            string user = txtUname.Text;
            connection con = new connection();
            using (MySqlConnection conn = con.GetConnection())
            {
                conn.Open();

                string see = "select * from accinfo where user_name = @banana";
                using (var cmdd = new MySqlCommand(see, conn))
                {
                    cmdd.Parameters.Add("@banana", MySqlDbType.VarChar).Value = user;
                    MySqlDataReader reader = cmdd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = reader["user_name"].ToString();
                        MessageBox.Show("Username already exists");
                    }
                    else if (user == null) { 
                        MessageBox.Show("Username field is empty");
                    }
                    else
                    {
                        passwordValidation();
                    }
                    reader.Close();
                    conn.Close();
                }
            }
        }

        private void passwordValidation()
        {
            string pass = txtpword.Text;
            string user = txtUname.Text;
            connection con = new connection();
            using (MySqlConnection conn = con.GetConnection())
            {
                conn.Open();

                var validations = new List<(bool Passed, string Message)>
                {
                    (pass.Any(Char.IsUpper), "This password has no uppercase letter"),
                    (pass.Any(Char.IsDigit), "This password has no digit"),
                    (pass.Any(c => char.IsLetterOrDigit(c)), "passowrd is less than 8 characters"),
                    (pass== null, "pasword can't be empty"),

                };

                bool hasError = false;
                foreach (var v in validations)
                {
                    if (!v.Passed)
                    {
                        MessageBox.Show(v.Message);
                        hasError = true;
                        break;

                    }
                }
                string hashedPass = GetSha256Hash(pass);

                if (!hasError)
                {
                    string query = "INSERT INTO hospital_records_db.accinfo (user_name, password) " +
                                         "VALUES (@userName,  @password)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@userName", MySqlDbType.VarChar).Value = user;
                        cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = hashedPass;
                     
                        int rows = cmd.ExecuteNonQuery();
                      
                    }


                    Form2 frm =  new Form2();
                    frm.Show();
                    this.Hide();
                }

                
            }
        }
        static string GetSha256Hash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}
