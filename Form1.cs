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
            userValidation();
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

        private void userValidation()
        {
            string user = txtUName.Text;
            connection con = new connection();
            using (MySqlConnection conn = con.GetConnection())
            {
                conn.Open();

                string see = "select * from accinfo where userName = @banana";
                using (var cmdd = new MySqlCommand(see, conn))
                {
                    cmdd.Parameters.Add("@banana", MySqlDbType.VarChar).Value = user;
                    MySqlDataReader reader = cmdd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = reader["user_name"].ToString();
                        MessageBox.Show("Username already exists");
                    }
                    //else
                    //{
                        
                    //}

                }
            }

        }
    }
}
