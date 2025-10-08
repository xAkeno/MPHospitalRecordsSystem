using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
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
            panel1.Visible = true;
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
        public void getNextIdDoctor() {
            doctor d = new doctor();
            idlbl.Text = d.get_next_id();
        }

        public void loadVisits()
        {
            visit v = new visit();
            dgvVisits.DataSource = v.read_visits();
        }

        public void loadAppointments()
        {
            appointment a = new appointment();
            dgvAppointments.DataSource = a.read_all_appointments();
        }
        public void loadSchedule() { 
            DocSchedule doc = new DocSchedule();
            dgvSchedule.DataSource = doc.read_schedule();
            idlbl.Text = doc.get_next_id();
        }
        public void getNextIdVisit()
        {
            visit v = new visit();
            idlbl.Text = v.get_next_id();
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
            if (validations())
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
            String search = textBox2.Text;
            if (search.Equals(""))
            {
                MessageBox.Show("Please enter a name or id to search.");
                loadVisits();
            }
            else
            {
                visit v = new visit();
                dgvVisits.DataSource = v.search_visit(search);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //bool onSelect = false;
            //label13.Visible = onSelect;
            //label14.Visible = onSelect;
            //label14.Visible = onSelect;
            //label15.Visible = onSelect;
            //label16.Visible = onSelect;
            //label17.Visible = onSelect;
            //label18.Visible = onSelect;
            //label19.Visible = onSelect;
            //label20.Visible = onSelect;
            //label21.Visible = onSelect;
            //label22.Visible = onSelect;

            //textBox4.Visible = onSelect;
            //textBox5.Visible = onSelect;
            //textBox6.Visible = onSelect;
            //textBox7.Visible = onSelect;
            //textBox8.Visible = onSelect;
            //textBox9.Visible = onSelect;
            //textBox10.Visible = onSelect;
            //textBox11.Visible = onSelect;
            //textBox12.Visible = onSelect;
            //textBox13.Visible = onSelect;

            //label8.Visible = !onSelect;
            //label9.Visible = !onSelect;
            //label10.Visible = !onSelect;
            //label11.Visible = !onSelect;
            //label12.Visible = !onSelect;
            //dtpvisit.Visible = !onSelect;
            //tb12.Visible = !onSelect;
            //tb13.Visible =  !onSelect;
            //cbDoctors.Visible = !onSelect;
            //cbPatients.Visible = !onSelect;
            panel3.Visible = false;
            panel4.Visible = true;
            loadVisits();
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
            DateTime dt3 = DateTime.Now;


            String nameD = doctorNameIn.Text;
            int idD = Convert.ToInt32(idlbl.Text);
            String specialD = cbspecial.SelectedItem.ToString();



            if (name.Equals("") || dtps.Equals("") || contact_number.Equals(""))
            {
                MessageBox.Show("Please answer all the required fields listed here\n"
                    + (name.Equals("") ? "- Enter in a name\n" : "")
                    + (dtps.Equals("") ? "- Enter in a birthday \n" : "")
                    + (contact_number.Equals("") ? "- Enter in a valid contact number" : "")
                );
                return false;
            }
            else if (!contact_number.Any(Char.IsDigit) || name.Any(Char.IsDigit) || dt1 < dt2 || dt1 > dt3|| contact_number.Length != 11 || contact_number[0] != '0' || contact_number[1] != '9' || dt1 == null)
            {
                MessageBox.Show("Please answer all the required fields listed here\n"
                     + (name.Any(Char.IsDigit) ? "- Enter in a name\n" : " ")
                     + (dt1 < dt2 ? "- Please enter a valid date \n" : " ")
                     + (dt1 == null ? "- Please enter a valid date \n" : " ")
                     + (dt1 > dt3 ? "- Please enter a valid date \n" : " ")
                     + (!contact_number.Any(Char.IsDigit) ? "- Enter in a valid contact number \n" : " ")
                     + (contact_number[0] != '0' || contact_number[1] != '9' ? "- Contact number must begin with 09 \n" : " ")
                     + (contact_number.Length != 11 ? "- Numbers length must be exacty 11 digits" : "")
                 );
                return false;
            }
            return true;
        }

        private void tabControl1_Selected_1(object sender, TabControlEventArgs e)
        {
            //MessageBox.Show(e.TabPage.Text);
            bool showPatients = e.TabPage.Text.Equals("patient");
            //label1.Visible = showPatients;
            //label2.Visible = showPatients;
            //nameIn.Visible = showPatients;
            //label3.Visible = showPatients;
            //dtp1.Visible = showPatients;
            //label4.Visible = showPatients;
            //contactnumberIn.Visible = showPatients;
            if (showPatients)
            {
                loadPatients();
                getNextId();
                //showFill(false);
                //showFill2(false);
                panel1.Visible = true;

                panel1.Location = new Point(4, 112);
            }
            else {
                panel1.Visible = false;
            }

            bool showDoctors = e.TabPage.Text.Equals("doctor");
            //cbspecial.Visible = showDoctors;
            //doctorNameIn.Visible = showDoctors;
            //label7.Visible = showDoctors;
            //label6.Visible = showDoctors;
            if (showDoctors)
            {
                loadDoctors();
                getNextIdDoctor();
                //showFill(false);
                //showFill2(false); 4, 112
                panel5.Visible = true;
                panel5.Location = new Point(4, 112);
            }
            else {
                panel5.Visible = false;
            }

             bool showVisits = e.TabPage.Text.Equals("visitors");

            if (showVisits)
            {
                //showFill(false);
                //showFill2(true);
                visit v = new visit();
                List<patientDTO> patients = v.getAllPatient();
                List<doctorDTO> doctors = v.getAllDoctors();

                foreach (patientDTO p in patients)
                {
                    string display = string.Format("{0,-30} | {1}", "Name: " + p.Name, "ID: " + p.PatientId);
                    cbPatients.Items.Add(new KeyValuePair<int, string>(p.PatientId, display));
                }
                cbPatients.DisplayMember = "Value"; // what user sees
                cbPatients.ValueMember = "Key";     // actual PatientId
                if (cbPatients.Items.Count > 0)
                    cbPatients.SelectedIndex = 0;

                // Populate Doctors ComboBox
                foreach (doctorDTO d in doctors)
                {
                    string display = string.Format("{0,-30} | {1}", "Name: " + d.DoctorName, "Specialty: " + d.Specialty);
                    cbDoctors.Items.Add(new KeyValuePair<int, string>(d.DoctorId, display));
                }
                cbDoctors.DisplayMember = "Value"; 
                cbDoctors.ValueMember = "Key";

                if (cbDoctors.Items.Count > 0)
                    cbDoctors.SelectedIndex = 0;
                panel4.Visible = true;
                panel4.Location = new Point(4, 112);
                loadVisits();
                getNextIdVisit();
            }
            else { 
                panel4.Visible = false;
                panel3.Visible = false;
            }

            bool showSched = e.TabPage.Text.Equals("Doctor schedule");

            if (showSched)
            {
                panel6.Visible = true;
                panel6.Location = new Point(4, 112);

                visit v = new visit();
                List<doctorDTO> doctors = v.getAllDoctors();

                foreach (doctorDTO d in doctors)
                {
                    string display = string.Format("{0,-30} | {1}", "Name: " + d.DoctorName, "Specialty: " + d.Specialty);
                    cbDoctorSched.Items.Add(new KeyValuePair<int, string>(d.DoctorId, display));
                }
                cbDoctorSched.DisplayMember = "Value";
                cbDoctorSched.ValueMember = "Key";

                if (cbDoctorSched.Items.Count > 0)
                    cbDoctorSched.SelectedIndex = 0;
                loadSchedule();
            }
            else { 
                panel6.Visible = false;
            }

            bool showAppointments = e.TabPage.Text.Equals("Appointment");
            if (showAppointments) { 
            
                panel7.Visible = true;
                panel7.Location = new Point(4, 112);

                visit v = new visit();
                List<patientDTO> patients = v.getAllPatient();
                List<doctorDTO> doctors = v.getAllDoctors();

                foreach (patientDTO p in patients)
                {
                    string display = string.Format("{0,-30} | {1}", "Name: " + p.Name, "ID: " + p.PatientId);
                    hideExCb.Items.Add(new KeyValuePair<int, string>(p.PatientId, display));
                }
                hideExCb.DisplayMember = "Value"; // what user sees
                hideExCb.ValueMember = "Key";     // actual PatientId
                if (hideExCb.Items.Count > 0)
                    hideExCb.SelectedIndex = 0;

                // Populate Doctors ComboBox
                foreach (doctorDTO d in doctors)
                {
                    string display = string.Format("{0,-30} | {1}", "Name: " + d.DoctorName, "Specialty: " + d.Specialty);
                    hideExCbDoc.Items.Add(new KeyValuePair<int, string>(d.DoctorId, display));
                }
                hideExCbDoc.DisplayMember = "Value";
                hideExCbDoc.ValueMember = "Key";

                if (hideExCbDoc.Items.Count > 0)
                    hideExCbDoc.SelectedIndex = 0;
                loadAppointments();
            }
            else
            {
                panel7.Visible = false;
            }

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
            int id = Convert.ToInt32(idlbl.Text);
            if (specialty.Equals(""))
            {
                MessageBox.Show("Please select a specialty");
                return;
            }
            if (name.Equals(""))
            {
                MessageBox.Show("Please enter a name");
                return;
            }
            doctor d = new doctor();
            if (!d.check_if_info_is_already_registred(id,name))
            {
                d.AddDoctor(name, specialty);
            }
            else
            {
                MessageBox.Show("Doctor is already taken");
            }
            loadDoctors();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            String name = doctorNameIn.Text;
            int id = Convert.ToInt32(idlbl.Text);
            String special = cbspecial.Text;

            doctor d = new doctor();
            if (name.Equals(""))
            {
                MessageBox.Show("Please answer all the required fields listed here\n"
                    + (name.Equals("") ? "- Enter a name\n" : "")
                );
                return;
            }
            d.update_doctor(name, id,special);
            loadDoctors();
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (!idlbl.Text.Equals(""))
            {
                int id = Convert.ToInt32(idlbl.Text);
                doctor d = new doctor();
                d.DeleteDoctor(id);
            }
            else
            {
                MessageBox.Show("Please select a patient first");
            }
            loadDoctors();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            doctor d = new doctor();
            doctorNameIn.Text = "";
            idlbl.Text = d.get_next_id();
            cbspecial.Items.Clear();
            dgvPatients.ClearSelection();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            String search = textBox6.Text;
            

            if (search.Equals(""))
            {
                MessageBox.Show("Please enter a name or id to search.");
                loadDoctors();
            }
            else
            {
                doctor d = new doctor();
                dgvDoctors.DataSource = d.search_doctor(search);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int selectedDoctorId = ((KeyValuePair<int, string>)cbDoctors.SelectedItem).Key;
            int selectedPatientId = ((KeyValuePair<int, string>)cbPatients.SelectedItem).Key;
            String visitDate = dtpvisit.Value.ToString("yyyy-MM-dd");
            String diagnosis = tb12.Text;
            String treatment = tb13.Text;

            if (selectedDoctorId == 0 || selectedPatientId == 0 || diagnosis.Equals("") || treatment.Equals(""))
            {
                MessageBox.Show("Please answer all the required fields listed here\n"
                    + (selectedPatientId == 0 ? "- Select a doctor\n" : "")
                    + (selectedDoctorId == 0 ? "- Select a patient \n" : "")
                    + (diagnosis.Equals("") ? "- Enter in a diagnosis\n" : "")
                    + (treatment.Equals("") ? "- Enter in a treatment\n" : "")
                );
                return;
            }
            visit v = new visit();
            v.AddVisit(selectedPatientId, selectedDoctorId, visitDate, diagnosis, treatment);
            loadVisits();
        }

        public void showFill(bool x) {
            bool onSelect = x;
            label13.Visible = onSelect;
            label14.Visible = onSelect;
            label14.Visible = onSelect;
            label15.Visible = onSelect;
            label16.Visible = onSelect;
            label17.Visible = onSelect;
            label18.Visible = onSelect;
            label19.Visible = onSelect;
            label20.Visible = onSelect;
            label21.Visible = onSelect;
            label22.Visible = onSelect;

            textBox3.Visible = onSelect;
            textBox4.Visible = onSelect;
            textBox5.Visible = onSelect;
            textBox7.Visible = onSelect;
            textBox8.Visible = onSelect;
            textBox9.Visible = onSelect;
            textBox10.Visible = onSelect;
            textBox11.Visible = onSelect;
            textBox12.Visible = onSelect;
            textBox13.Visible = onSelect;

        }
        public void showFill2(bool onSelect) {
            label8.Visible = onSelect;
            label9.Visible = onSelect;
            label10.Visible = onSelect;
            label11.Visible = onSelect;
            label12.Visible = onSelect;
            dtpvisit.Visible = onSelect;
            tb12.Visible = onSelect;
            tb13.Visible = onSelect;
            cbDoctors.Visible = onSelect;
            cbPatients.Visible = onSelect;


            cbDoctors.Visible = onSelect;
            cbPatients.Visible = onSelect;
            dtpvisit.Visible = onSelect;
            label11.Visible = onSelect;
            tb13.Visible = onSelect;
            cbDoctors.Visible = onSelect;
            cbPatients.Visible = onSelect;
            dtpvisit.Visible = onSelect;
        }

        private void dgvVisits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //showFill(true);
            //showFill2(false);
            if (dgvVisits.SelectedRows.Count > 0)
            {
                panel4.Visible = false;
                panel3.Visible = true;
                panel3.Location = new Point(4, 112);
                DataGridViewRow row = dgvVisits.SelectedRows[0];
                string visitId = row.Cells["VisitId"].Value.ToString();
                string patientId = row.Cells["PatientId"].Value.ToString();
                string patientName = row.Cells["PatientName"].Value.ToString();
                string dateOfBirth = row.Cells["DateOfBirth"].Value.ToString();
                string contactNo = row.Cells["ContactNumber"].Value.ToString();
                string doctorId = row.Cells["DoctorId"].Value.ToString();
                string doctorName = row.Cells["DoctorName"].Value.ToString();
                string specialty = row.Cells["Specialty"].Value.ToString();
                string dateOfVisit = row.Cells["DateOfVisit"].Value.ToString();
                string diagnosis = row.Cells["Diagnosis"].Value.ToString();
                string treatment = row.Cells["Treatment"].Value.ToString();


                idlbl.Text = visitId;
                textBox3.Text = patientId;
                textBox4.Text = patientName;
                textBox5.Text = dateOfBirth;
                textBox7.Text = contactNo;
                textBox8.Text = doctorId;
                textBox9.Text = doctorName;
                textBox10.Text = specialty;
                textBox11.Text = dateOfVisit;
                textBox12.Text = diagnosis;
                textBox13.Text = treatment;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            String diagnosis = textBox12.Text;
            String treatment = textBox13.Text;
            String id = idlbl.Text;

            if (diagnosis.Equals("") || treatment.Equals(""))
            {
                MessageBox.Show("Please answer all the required fields listed here\n"
                    + (diagnosis.Equals("") ? "- Enter in a diagnosis\n" : "")
                    + (treatment.Equals("") ? "- Enter in a treatment\n" : "")
                );
                return;
            }
            visit v = new visit();
            v.update_visits(diagnosis, treatment, Convert.ToInt32(id));
            loadVisits();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            String id = idlbl.Text;
            if (!id.Equals(""))
            {
                visit v = new visit();
                v.DeleteVisit(Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Please select a visit first");
            }
            loadVisits();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvVisits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnScheduleAdd_Click(object sender, EventArgs e)
        {
            String doc = cbDoctorSched.SelectedItem.ToString();
            String SchedDate = dtpScheduleDate.Value.ToString("yyyy-MM-dd");
            String SchedTime = timePicker.Value.ToString("HH:mm");

            if (!doc.Equals("") || !SchedDate.Equals("") || !SchedDate.Equals("")) { 
                DocSchedule docSchedule = new DocSchedule();
                docSchedule.add_Schedule(DateTime.Parse(SchedDate), DateTime.Parse(SchedTime), ((KeyValuePair<int, string>)cbDoctorSched.SelectedItem).Key);
            }
            loadSchedule();
        }

        private void btnScheduleUpdate_Click(object sender, EventArgs e)
        {
            String doc = cbDoctorSched.SelectedItem.ToString();
            String SchedDate = dtpScheduleDate.Value.ToString("yyyy-MM-dd");
            String SchedTime = timePicker.Value.ToString("HH:mm");
            int id = Convert.ToInt32(idlbl.Text);

            if (!doc.Equals("") || !SchedDate.Equals("") || !SchedDate.Equals(""))
            {
                DocSchedule docSchedule = new DocSchedule();
                docSchedule.update_Schedule(DateTime.Parse(SchedDate), DateTime.Parse(SchedTime), ((KeyValuePair<int, string>)cbDoctorSched.SelectedItem).Key,id);
            }
            loadSchedule();
        }

        private void btnScheduleUnselect_Click(object sender, EventArgs e)
        {
            DocSchedule doc = new DocSchedule();
            idlbl.Text = doc.get_next_id();
            dtpScheduleDate.Value = DateTime.Now;
            timePicker.Value = DateTime.Now;
            dgvSchedule.ClearSelection();

            cbDoctorSched.Items.Clear();

            visit v = new visit();
            List<doctorDTO> doctors = v.getAllDoctors();

            foreach (doctorDTO d in doctors)
            {
                string display = string.Format("{0,-30} | {1}", "Name: " + d.DoctorName, "Specialty: " + d.Specialty);
                cbDoctorSched.Items.Add(new KeyValuePair<int, string>(d.DoctorId, display));
            }

            cbDoctorSched.DisplayMember = "Value";
            cbDoctorSched.ValueMember = "Key";

            if (cbDoctorSched.Items.Count > 0)
                cbDoctorSched.SelectedIndex = 0;
        }

        private void dgvSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSchedule.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvSchedule.SelectedRows[0];
                string ScheduleId = row.Cells["ScheduleId"].Value.ToString();
                int DoctorId = Convert.ToInt32(row.Cells["DoctorId"].Value.ToString());
                string DoctorName = row.Cells["DoctorName"].Value.ToString();
                string Specialty = row.Cells["Specialty"].Value.ToString();
                string AvailableDate = row.Cells["AvailableDate"].Value.ToString();
                string AvailableTime = row.Cells["AvailableTime"].Value.ToString();

                idlbl.Text = ScheduleId;
                dtpScheduleDate.Value = DateTime.Parse(AvailableDate);
                timePicker.Value = DateTime.Parse(AvailableTime);


                //string display = $"Name: {DoctorName,-30} | Specialty: {Specialty}";

                //cbDoctorSched.Items.Clear();

                //cbDoctorSched.DisplayMember = "Value";
                //cbDoctorSched.ValueMember = "Key";

                //var doctorItem = new KeyValuePair<int, string>(DoctorId, display);
                //cbDoctorSched.Items.Add(doctorItem);
                cbDoctorSched.SelectedItem = cbDoctorSched.Items.Cast<KeyValuePair<int, string>>().FirstOrDefault(item => item.Key == DoctorId); ;
            }
        }

        private void btnScheduleDelete_Click(object sender, EventArgs e)
        {
            String id = idlbl.Text;
            if (!id.Equals(""))
            {
                DocSchedule v = new DocSchedule();
                v.delete_Schedule(Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Please select a visit first");
            }
            loadSchedule();
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void rbAppointment_Click(object sender, EventArgs e)
        {

        }

        private void rbAppointment2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAppointment2.Checked)
            {
                hideExLbl.Location = new Point(10, 72);
                hideExCb.Location = new Point(7, 93);
                hideExCb.Visible = true;
                hideExLbl.Visible = true;
                textBox17.Visible = false;
                textBox18.Visible = false;
                label28.Visible = false;
                label29.Visible = false;
                label27.Visible = false;    
                dateTimePicker2.Visible = false;

                label31.Location = new Point(10, 132);
                label32.Location = new Point(11, 201);
                dateTimePicker3.Location = new Point(8, 158);
                dateTimePicker4.Location = new Point(8, 224);
            }
        }

        private void rbAppointment_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAppointment.Checked)
            {
                hideExCb.Visible = false;
                hideExLbl.Visible = false;
                textBox17.Visible = true;
                textBox18.Visible = true;
                label28.Visible = true;
                label29.Visible = true;
                label27.Visible = true;
                dateTimePicker2.Visible = true;

                label31.Location = new Point(11, 336);
                label32.Location = new Point(11, 401);
                dateTimePicker3.Location = new Point(8,356);
                dateTimePicker4.Location = new Point(7,422);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

            String dateAppoint = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            String startTime = dateTimePicker4.Value.ToString("HH:mm");
            if (rbAppointment.Checked)
            {
                String name = textBox17.Text;
                String contact = textBox18.Text;
                String dateOfBirth = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                appointment a = new appointment();
                patient p = new patient();
                p.add_patient(name, dateOfBirth, contact);

                a.add_appoint(dateTimePicker3.Value, dateTimePicker4.Value, ((KeyValuePair<int, string>)hideExCbDoc.SelectedItem).Key, p.get_patient_id_by_name(name));
            }
            else if (rbAppointment2.Checked)
            { 
                String id = ((KeyValuePair<int, string>)hideExCb.SelectedItem).Key.ToString();
                appointment a = new appointment();
                a.add_appoint(dateTimePicker3.Value, dateTimePicker4.Value, ((KeyValuePair<int, string>)hideExCbDoc.SelectedItem).Key, Convert.ToInt32(id));
            }


        }

        private void button19_Click(object sender, EventArgs e)
        {
            String dateAppoint = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            String startTime = dateTimePicker4.Value.ToString("HH:mm");
            int App_id = Convert.ToInt32(idlbl.Text);
            if (rbAppointment.Checked)
            {
                String name = textBox17.Text;
                String contact = textBox18.Text;
                String dateOfBirth = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                appointment a = new appointment();
                patient p = new patient();

                a.update_appointment(App_id, ((KeyValuePair<int, string>)hideExCbDoc.SelectedItem).Key, dateTimePicker3.Value, dateTimePicker4.Value,"");
            }
            else if (rbAppointment2.Checked)
            {
                String id = ((KeyValuePair<int, string>)hideExCb.SelectedItem).Key.ToString();
                appointment a = new appointment();
                a.add_appoint(dateTimePicker3.Value, dateTimePicker4.Value, ((KeyValuePair<int, string>)hideExCbDoc.SelectedItem).Key, Convert.ToInt32(id));
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if(!idlbl.Text.Equals(""))
            {
                int id = Convert.ToInt32(idlbl.Text);
                appointment a = new appointment();
                a.delete_appointment(id);
            }
            else
            {
                MessageBox.Show("Please select an appointment first");
            }
        }

        private void dgvAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                hideExCb.Visible = false;
                hideExLbl.Visible = false;
                textBox17.Visible = true;
                textBox18.Visible = true;
                label28.Visible = true;
                label29.Visible = true;
                label27.Visible = true;
                dateTimePicker2.Visible = true;

                label31.Location = new Point(11, 336);
                label32.Location = new Point(11, 401);
                dateTimePicker3.Location = new Point(8, 356);
                dateTimePicker4.Location = new Point(7, 422);


                DataGridViewRow row = dgvAppointments.SelectedRows[0];
                int appointmentId = Convert.ToInt32(row.Cells["AppointmentId"].Value);
                int patientId = Convert.ToInt32(row.Cells["PatientId"].Value);
                string patientName = row.Cells["PatientName"].Value.ToString();
                string contactNumber = row.Cells["ContactNumber"].Value.ToString();
                DateTime dateOfBirth = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
                int doctorId = Convert.ToInt32(row.Cells["DoctorId"].Value);
                DateTime appointmentDate = Convert.ToDateTime(row.Cells["AppointmentDate"].Value);
                DateTime appointmentTime = Convert.ToDateTime(row.Cells["AppointmentTime"].Value);
                string status = row.Cells["Status"].Value.ToString();

                idlbl.Text = Convert.ToString(appointmentId);

                textBox17.Text = patientName;
                textBox18.Text = contactNumber;
                dateTimePicker2.Value = dateOfBirth;
                dateTimePicker3.Value = appointmentDate;
                dateTimePicker4.Value = appointmentTime;
                hideExCbDoc.SelectedItem = hideExCbDoc.Items.Cast<KeyValuePair<int, string>>().FirstOrDefault(item => item.Key == doctorId);


            }
        }
    }
}
