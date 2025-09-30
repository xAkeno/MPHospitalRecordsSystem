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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            loadPatients();
        }

        public void loadPatients()
        {
            patient p = new patient();
            dgvPatients.DataSource = p.read_patient();
            MessageBox.Show("Loaded patients");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = nameIn.Text;
            String date_of_birth = datebirthIn.Text;
            String contact_number = contactnumberIn.Text;

            if(name.Equals("") || date_of_birth.Equals("") || contact_number.Equals(""))
            {
                MessageBox.Show("Please complete all the list that say here?\n"
                    + (name.Equals("") ? "Fill the name\n" : "")
                    + (date_of_birth.Equals("") ? "Fill the date of birth\n" : "")
                    + (contact_number.Equals("") ? "Fill the contact number" : "")
                );

            }
            else
            {
                patient p = new patient();
                p.add_patient(name, date_of_birth, contact_number);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
