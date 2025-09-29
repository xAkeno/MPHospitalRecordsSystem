using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPHospitalRecordsSystem
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        public Form1()
        {
            InitializeComponent();
            Instance = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            accountValidation();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 signup = new Form2();
            signup.Show();
            this.Hide();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUName_TextChanged(object sender, EventArgs e)
        {

        }
        private void accountValidation()
        {
            string user = txtUName.Text;
            string pass = txtPass.Text;
            string hashedPass = GetSha256Hash(pass);
            connection con = new connection();
            using (MySqlConnection conn = con.GetConnection())
            {
                conn.Open();
                string see = "select * from accinfo where user_name = @banana and password = @apple";
                using (var cmdd = new MySqlCommand(see, conn))
                {
                    cmdd.Parameters.Add("@banana", MySqlDbType.VarChar).Value = user;
                    cmdd.Parameters.Add("@apple", MySqlDbType.VarChar).Value = hashedPass;
                    MySqlDataReader reader = cmdd.ExecuteReader();

                    

                   if (reader.Read())
                     {
                        user = reader["user_name"].ToString();
                         Form3 fr = new Form3();
                         fr.Show();
                         this.Hide();
                     }
                     else if((user == null) || (pass == null))
                     {
                        MessageBox.Show("Username or Password field is empty");
                      }
                     else
                     {
                         MessageBox.Show("Invalid Username or Password");
                     }
                    reader.Close();
                    conn.Close();
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
