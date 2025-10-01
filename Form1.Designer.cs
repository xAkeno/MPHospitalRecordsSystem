using System.Drawing;
using System.Windows.Forms;

namespace MPHospitalRecordsSystem
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtUName = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkSignup = new System.Windows.Forms.LinkLabel();
            this.logoside = new MPHospitalRecordsSystem.RoundedLabel();
            this.topbanner = new MPHospitalRecordsSystem.RoundedLabel();
            this.btnLogin = new MPHospitalRecordsSystem.RoundedButton();
            this.SuspendLayout();
            // 
            // txtUName
            // 
            this.txtUName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUName.Location = new System.Drawing.Point(40, 138);
            this.txtUName.Multiline = true;
            this.txtUName.Name = "txtUName";
            this.txtUName.Size = new System.Drawing.Size(284, 33);
            this.txtUName.TabIndex = 0;
            this.txtUName.TextChanged += new System.EventHandler(this.txtUName_TextChanged);
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(40, 206);
            this.txtPass.Multiline = true;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(284, 33);
            this.txtPass.TabIndex = 1;
            this.txtPass.TextChanged += new System.EventHandler(this.txtPass_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dont have an account?";
            // 
            // lnkSignup
            // 
            this.lnkSignup.AutoSize = true;
            this.lnkSignup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSignup.Location = new System.Drawing.Point(218, 303);
            this.lnkSignup.Name = "lnkSignup";
            this.lnkSignup.Size = new System.Drawing.Size(54, 15);
            this.lnkSignup.TabIndex = 5;
            this.lnkSignup.TabStop = true;
            this.lnkSignup.Text = "Sign Up!";
            this.lnkSignup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // logoside
            // 
            this.logoside.BorderColor = System.Drawing.Color.Black;
            this.logoside.BorderRadius = 50;
            this.logoside.BorderSize = 0;
            this.logoside.Image = ((System.Drawing.Image)(resources.GetObject("logoside.Image")));
            this.logoside.Location = new System.Drawing.Point(366, 25);
            this.logoside.Name = "logoside";
            this.logoside.Size = new System.Drawing.Size(341, 328);
            this.logoside.TabIndex = 8;
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
            this.topbanner.Location = new System.Drawing.Point(74, 46);
            this.topbanner.Name = "topbanner";
            this.topbanner.Padding = new System.Windows.Forms.Padding(45, 5, 45, 5);
            this.topbanner.Size = new System.Drawing.Size(207, 49);
            this.topbanner.TabIndex = 7;
            this.topbanner.Text = "Log In";
            this.topbanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLogin.BorderColor = System.Drawing.Color.Transparent;
            this.btnLogin.BorderRadius = 20;
            this.btnLogin.BorderSize = 0;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(111, 254);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(122, 32);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Log In";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(719, 383);
            this.Controls.Add(this.logoside);
            this.Controls.Add(this.topbanner);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lnkSignup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUName);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUName;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lnkSignup;
        private MPHospitalRecordsSystem.RoundedButton btnLogin;
        private MPHospitalRecordsSystem.RoundedLabel topbanner;
        private MPHospitalRecordsSystem.RoundedLabel logoside;
    }
}

