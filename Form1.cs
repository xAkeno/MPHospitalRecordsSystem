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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            juice();  
        }


        public void juice()
        {
            Form3 uhh = new Form3();
            uhh.Show();
        }

        public void aur() { 
        Form2 yesir = new Form2();

            yesir.Show();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            aur();
        }
    }
}
