using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
        public void loadDoctors()
        {
            doctor d = new doctor();
            dgvDoctors.DataSource = d.read_doctors();
        }   
        public void getNextId()
        {
            patient p = new patient();
            idlbl.Text = p.get_next_id();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idlbl.Text);
            String name = nameIn.Text;
            String dtps = dtp1.Value.ToString("yyyy-MM-dd");
            String contact_number = contactnumberIn.Text;

            patient p = new patient();

            if (!p.check_if_info_is_already_registred(id, name) && validations())
            {
                p.add_patient(name, dtps, contact_number);
            }
            else
            {
                MessageBox.Show("Patient is already taken");
            }
            loadPatients();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String search = searchbox.Text;

            if (search.Equals(""))
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
            int id = Convert.ToInt32(idlbl.Text);
            String dtps = dtp1.Value.ToString("yyyy-MM-dd");
            String contact_number = contactnumberIn.Text;

            patient p = new patient();
            if (!validations())
            {
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
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        private bool validations()
        {
            int id = Convert.ToInt32(idlbl.Text);
            String name = nameIn.Text;
            String dtps = dtp1.Value.ToString("d");
            String contact_number = contactnumberIn.Text;
            DateTime dt1 = DateTime.Parse(dtps);
            DateTime dt2 = DateTime.Parse("01/01/1920");



            if (name.Equals("") || dtps.Equals("") || contact_number.Equals(""))
            {
                MessageBox.Show("Please answer all the required fields listed here\n"
                    + (name.Equals("") ? "- Enter in a name\n" : "")
                    + (dtps.Equals("") ? "- Enter in a birthday \n" : "")
                    + (contact_number.Equals("") ? "- Enter in a valid contact number" : "")
                );
                return true;
            }
            else if (!contact_number.Any(Char.IsDigit) || name.Any(Char.IsDigit) || dt1 < dt2 || contact_number.Length != 11 || contact_number[0] != '0' || contact_number[1] != '9' || dt1 == null)
            {
                MessageBox.Show("Please answer all the required fields listed here\n"
                     + (name.Any(Char.IsDigit) ? "- Enter in a name\n" : " ")
                     + (dt1 < dt2 ? "- Please enter a valid date \n" : " ")
                     + (dt1 == null ? "- Please enter a valid date \n" : " ")
                     + (!contact_number.Any(Char.IsDigit) ? "- Enter in a valid contact number \n" : " ")
                     + (contact_number[0] != '0' || contact_number[1] != '9' ? "- Contact number must begin with 09 \n" : " ")
                     + (contact_number.Length != 11 ? "- Numbers length must be exacty 11 digits" : "")
                 );
                return true;
            }
            return false;
        }

        private void tabControl1_Selected_1(object sender, TabControlEventArgs e)
        {
            //MessageBox.Show(e.TabPage.Text);
            bool showPatients = e.TabPage.Text.Equals("patient");
            label1.Visible = showPatients;
            label2.Visible = showPatients;
            nameIn.Visible = showPatients;
            label3.Visible = showPatients;
            dtp1.Visible = showPatients;
            label4.Visible = showPatients;
            contactnumberIn.Visible = showPatients;
            if (showPatients) {
                loadPatients();
                getNextId();
            }

            bool showDoctors = e.TabPage.Text.Equals("doctor");
            cbspecial.Visible = showDoctors;
            doctorNameIn.Visible = showDoctors;
            label7.Visible = showDoctors;
            label6.Visible = showDoctors;
            if (showDoctors) {
                loadDoctors();
            }

            bool showVisits = e.TabPage.Text.Equals("visitors");
            label8.Visible = showVisits;
            label9.Visible = showVisits;
            label10.Visible = showVisits;
            label11.Visible = showVisits;
            label12.Visible = showVisits;
            tb10.Visible = showVisits;
            textBox1.Visible = showVisits;
            textBox3.Visible = showVisits;
            textBox4.Visible = showVisits;
            textBox5.Visible = showVisits;

            if (showPatients)
                label5.Text = "Patient Id:";
            else if (showDoctors)
                label5.Text = "Doctor Id:";
            else if (showVisits)
                label5.Text = "Visitor Id:";
            else
                label5.Text = "Id:";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!idlbl.Text.Equals(""))
            {
                int id = Convert.ToInt32(idlbl.Text);
                patient p = new patient();
                p.delete_patient(id);
            }
            else
            {
                MessageBox.Show("Please select a patient first");
            }
            loadPatients();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            String name = doctorNameIn.Text;
            String specialty = cbspecial.SelectedItem.ToString();
            if (specialty == null)
            {
                MessageBox.Show("Please select a specialty");
                return;
            }
            if (name == null)
            {
                MessageBox.Show("Please enter a name");
                return;
            }
            doctor d = new doctor();
            if (d.check_if_info_is_already_registred(0,name))
            {
                d.AddDoctor(name, specialty);
            }
            else
            {
                MessageBox.Show("Doctor is already taken");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void dgvDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDoctors.SelectedRows[0];


                string id = row.Cells["DoctorId"].Value.ToString();
                string name = row.Cells["DoctorName"].Value.ToString();
                string special = row.Cells["Specialty"].Value.ToString();


                idlbl.Text = id;
                doctorNameIn.Text = name;
                cbspecial.SelectedItem = special;

                //MessageBox.Show(
                //    $"Patient Info:\nID: {id}\nName: {name}\nDate of Birth: {dob}\nContact: {contact}"
                //);
            }
            else
            {
                MessageBox.Show("⚠ Please select a row first.");
            }
        }
    }
}
