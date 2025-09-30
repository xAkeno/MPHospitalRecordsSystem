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
            getNextId();
        }

        public void loadPatients()
        {
            patient p = new patient();
            dgvPatients.DataSource = p.read_patient();
        }
        public void getNextId() {
            patient p = new patient();
            idlbl.Text = p.get_next_id();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idlbl.Text);
            String name = nameIn.Text;
            //String date_of_birth = datebirthIn.Text;
            String dtps = dtp1.Value.ToString("yyyy-MM-dd");
            String contact_number = contactnumberIn.Text;

            if (name.Equals("") || dtps.Equals("") || contact_number.Equals(""))
            {
                MessageBox.Show("Please complete all the list that say here?\n"
                    + (name.Equals("") ? "Fill the name\n" : "")
                    + (dtps.Equals("") ? "Fill the date of birth\n" : "")
                    + (contact_number.Equals("") ? "Fill the contact number" : "")
                );

            }
            else
            {
                patient p = new patient();

                if (!p.check_if_info_is_already_registred(id, name))
                {
                    p.add_patient(name, dtps, contact_number);
                }
                else
                {
                    MessageBox.Show("Patient is already taken");
                }
            }
            loadPatients();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String search = searchbox.Text;

            if(search.Equals(""))
            {
                MessageBox.Show("Please enter a name or id to search.");
                loadPatients();
            }
            else
            {
                patient p = new patient();
                dgvPatients.DataSource = p.search_patient(search);
            }
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

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPatients.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPatients.SelectedRows[0];


                string id = row.Cells["PatientId"].Value.ToString();
                string name = row.Cells["Name"].Value.ToString();
                string dob = row.Cells["DateOfBirth"].Value.ToString();
                string contact = row.Cells["ContactNumber"].Value.ToString();

                idlbl.Text = id;
                nameIn.Text = name;
                dtp1.Value = DateTime.Parse(dob);
                contactnumberIn.Text = contact;

                //MessageBox.Show(
                //    $"Patient Info:\nID: {id}\nName: {name}\nDate of Birth: {dob}\nContact: {contact}"
                //);
            }
            else
            {
                MessageBox.Show("⚠ Please select a row first.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String name = nameIn.Text;
            //String date_of_birth = datebirthIn.Text;
            int id = Convert.ToInt32(idlbl.Text);
            String dtps = dtp1.Value.ToString("yyyy-MM-dd");
            String contact_number = contactnumberIn.Text;

            if (name.Equals("") || dtps.Equals("") || contact_number.Equals(""))
            {
                MessageBox.Show("Please complete all the list that say here?\n"
                    + (name.Equals("") ? "Fill the name\n" : "")
                    + (dtps.Equals("") ? "Fill the date of birth\n" : "")
                    + (contact_number.Equals("") ? "Fill the contact number" : "")
                );
            }
            else
            {
                patient p = new patient();
                p.update_patient(name, dtps, contact_number, id);
            }
            loadPatients();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            patient p = new patient();
            idlbl.Text = p.get_next_id();
            nameIn.Text = "";
            dtp1.Value = DateTime.Now;
            contactnumberIn.Text = "";
            dgvPatients.ClearSelection();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
