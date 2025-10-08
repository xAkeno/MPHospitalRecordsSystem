using Google.Protobuf.WellKnownTypes;
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
using System.Xml.Linq;

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
           passwordValidation();         
        }

        private void txtUname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpword_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 login = new Form1();  
            login.Show();
            this.Hide();
        }
      
        private void passwordValidation()
        {
            string pass = txtpword.Text;
            string user = txtUname.Text;
            string role = "clerk";
            AccInfo acc = new AccInfo();
            string HashedPass = GetSha256Hash(pass);               
            if (user.Length < 5)
            {
                MessageBox.Show("Username must be at least 5 characters long");
                return;
            }
            connection con = new connection();
            using (MySqlConnection conn = con.GetConnection())
            {
                conn.Open();
                bool hasError = false;
                if (user.Equals("") || pass.Equals("") || !pass.Any(Char.IsDigit) || !pass.Any(Char.IsUpper) || !pass.Any(c => !char.IsLetterOrDigit(c)) || pass.Length <8)
                {
                    MessageBox.Show("Please follow the instructions given below\n"
                        + (user.Equals("") ? "- Enter in a username\n" : "")
                        + (pass.Equals("") ? "- Enter in a Password \n" : "")
                        + (!pass.Any(Char.IsDigit) ? "- The password must contain at least 1 digit\n" : "")
                        + (!pass.Any(Char.IsUpper) ? "- The password must contain at least 1 Uppercase letter\n" : "")
                        + (!pass.Any(c => !char.IsLetterOrDigit(c)) ? "- The password must contain at least 1 special character\n" : "")
                        + (pass.Length <8 ? "- The password must be at least 8 characters long" : "")
                    );
                   hasError = true;
                }
                          
                if (!hasError && acc.repetitionCheck(user))
                {
                    acc.addUserInfo(user, HashedPass, role);
                    Form1 frm = new Form1();
                    frm.Show();
                    this.Hide();                  
                }             
            }
        }
        private void topbanner_Click(object sender, EventArgs e)
        {

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
