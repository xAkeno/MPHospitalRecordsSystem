using System.Drawing;
using System.Windows.Forms;
namespace MPHospitalRecordsSystem
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new MPHospitalRecordsSystem.RoundedButton();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtUname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.topbanner = new MPHospitalRecordsSystem.RoundedLabel();
            this.logoside = new MPHospitalRecordsSystem.RoundedLabel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.BorderColor = System.Drawing.Color.Transparent;
            this.button1.BorderRadius = 20;
            this.button1.BorderSize = 0;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(125, 256);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Register";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(236, 299);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(40, 16);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Login";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtUname
            // 
            this.txtUname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUname.Location = new System.Drawing.Point(41, 138);
            this.txtUname.Margin = new System.Windows.Forms.Padding(2);
            this.txtUname.Multiline = true;
            this.txtUname.Name = "txtUname";
            this.txtUname.Size = new System.Drawing.Size(284, 33);
            this.txtUname.TabIndex = 3;
            this.txtUname.TextChanged += new System.EventHandler(this.txtUname_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 182);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password:";
            // 
            // txtpword
            // 
            this.txtpword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpword.Location = new System.Drawing.Point(41, 204);
            this.txtpword.Margin = new System.Windows.Forms.Padding(2);
            this.txtpword.Multiline = true;
            this.txtpword.Name = "txtpword";
            this.txtpword.Size = new System.Drawing.Size(284, 33);
            this.txtpword.TabIndex = 6;
            this.txtpword.TextChanged += new System.EventHandler(this.txtpword_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(77, 300);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Already have an account?";
            // 
            // topbanner
            // 
            this.topbanner.AutoSize = true;
            this.topbanner.BackColor = System.Drawing.Color.Gray;
            this.topbanner.BorderColor = System.Drawing.Color.Black;
            this.topbanner.BorderRadius = 50;
            this.topbanner.BorderSize = 0;
            this.topbanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topbanner.ForeColor = System.Drawing.Color.White;
            this.topbanner.Location = new System.Drawing.Point(73, 44);
            this.topbanner.Name = "topbanner";
            this.topbanner.Padding = new System.Windows.Forms.Padding(45, 5, 45, 5);
            this.topbanner.Size = new System.Drawing.Size(220, 49);
            this.topbanner.TabIndex = 8;
            this.topbanner.Text = "Sign In";
            this.topbanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.topbanner.Click += new System.EventHandler(this.topbanner_Click);
            // 
            // logoside
            // 
            this.logoside.BorderColor = System.Drawing.Color.Black;
            this.logoside.BorderRadius = 50;
            this.logoside.BorderSize = 0;
            this.logoside.Image = ((System.Drawing.Image)(resources.GetObject("logoside.Image")));
            this.logoside.Location = new System.Drawing.Point(367, 23);
            this.logoside.Name = "logoside";
            this.logoside.Size = new System.Drawing.Size(341, 328);
            this.logoside.TabIndex = 9;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(720, 375);
            this.Controls.Add(this.logoside);
            this.Controls.Add(this.topbanner);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtpword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUname);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MPHospitalRecordsSystem.RoundedButton button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtUname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpword;
        private System.Windows.Forms.Label label4;
        private RoundedLabel topbanner;
        private RoundedLabel logoside;
    }
}