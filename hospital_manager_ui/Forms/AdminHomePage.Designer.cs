namespace hospital_manager_ui.Forms
{
    partial class AdminHomePage
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SpecialitiesTab = new System.Windows.Forms.TabPage();
            this.listViewSpeciality = new System.Windows.Forms.ListView();
            this.SpecialityId = new System.Windows.Forms.ColumnHeader();
            this.SpecialityName = new System.Windows.Forms.ColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.HospitalsTab = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.listViewHospital = new System.Windows.Forms.ListView();
            this.HospitalId = new System.Windows.Forms.ColumnHeader();
            this.HospitalName = new System.Windows.Forms.ColumnHeader();
            this.HospitalStreet = new System.Windows.Forms.ColumnHeader();
            this.HospitalCity = new System.Windows.Forms.ColumnHeader();
            this.HospitalPostCode = new System.Windows.Forms.ColumnHeader();
            this.HospitalCountry = new System.Windows.Forms.ColumnHeader();
            this.DoctorsTab = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.listViewDoctor = new System.Windows.Forms.ListView();
            this.DoctorUsername = new System.Windows.Forms.ColumnHeader();
            this.DoctorName = new System.Windows.Forms.ColumnHeader();
            this.DoctorEmail = new System.Windows.Forms.ColumnHeader();
            this.DoctorPhone = new System.Windows.Forms.ColumnHeader();
            this.AppointmentsTab = new System.Windows.Forms.TabPage();
            this.listViewAppointment = new System.Windows.Forms.ListView();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.SpecialitiesTab.SuspendLayout();
            this.HospitalsTab.SuspendLayout();
            this.DoctorsTab.SuspendLayout();
            this.AppointmentsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.SpecialitiesTab);
            this.tabControl1.Controls.Add(this.HospitalsTab);
            this.tabControl1.Controls.Add(this.DoctorsTab);
            this.tabControl1.Controls.Add(this.AppointmentsTab);
            this.tabControl1.Location = new System.Drawing.Point(10, 9);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(731, 371);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // SpecialitiesTab
            // 
            this.SpecialitiesTab.Controls.Add(this.listViewSpeciality);
            this.SpecialitiesTab.Controls.Add(this.button1);
            this.SpecialitiesTab.Location = new System.Drawing.Point(4, 24);
            this.SpecialitiesTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpecialitiesTab.Name = "SpecialitiesTab";
            this.SpecialitiesTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpecialitiesTab.Size = new System.Drawing.Size(723, 343);
            this.SpecialitiesTab.TabIndex = 0;
            this.SpecialitiesTab.Text = "Specialities";
            this.SpecialitiesTab.UseVisualStyleBackColor = true;
            this.SpecialitiesTab.Click += new System.EventHandler(this.Specialities_Click);
            // 
            // listViewSpeciality
            // 
            this.listViewSpeciality.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSpeciality.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SpecialityId,
            this.SpecialityName});
            this.listViewSpeciality.HideSelection = false;
            this.listViewSpeciality.Location = new System.Drawing.Point(3, 2);
            this.listViewSpeciality.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewSpeciality.Name = "listViewSpeciality";
            this.listViewSpeciality.Size = new System.Drawing.Size(714, 310);
            this.listViewSpeciality.TabIndex = 1;
            this.listViewSpeciality.UseCompatibleStateImageBehavior = false;
            this.listViewSpeciality.View = System.Windows.Forms.View.Details;
            this.listViewSpeciality.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // SpecialityId
            // 
            this.SpecialityId.Text = "ID";
            // 
            // SpecialityName
            // 
            this.SpecialityName.Text = "Name";
            this.SpecialityName.Width = 200;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(634, 316);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HospitalsTab
            // 
            this.HospitalsTab.Controls.Add(this.button2);
            this.HospitalsTab.Controls.Add(this.listViewHospital);
            this.HospitalsTab.Location = new System.Drawing.Point(4, 24);
            this.HospitalsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HospitalsTab.Name = "HospitalsTab";
            this.HospitalsTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HospitalsTab.Size = new System.Drawing.Size(723, 343);
            this.HospitalsTab.TabIndex = 1;
            this.HospitalsTab.Text = "Hospitals";
            this.HospitalsTab.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(634, 316);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listViewHospital
            // 
            this.listViewHospital.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHospital.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HospitalId,
            this.HospitalName,
            this.HospitalStreet,
            this.HospitalCity,
            this.HospitalPostCode,
            this.HospitalCountry});
            this.listViewHospital.HideSelection = false;
            this.listViewHospital.Location = new System.Drawing.Point(3, 2);
            this.listViewHospital.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewHospital.Name = "listViewHospital";
            this.listViewHospital.Size = new System.Drawing.Size(714, 310);
            this.listViewHospital.TabIndex = 0;
            this.listViewHospital.UseCompatibleStateImageBehavior = false;
            this.listViewHospital.View = System.Windows.Forms.View.Details;
            this.listViewHospital.SelectedIndexChanged += new System.EventHandler(this.listViewHospital_SelectedIndexChanged);
            // 
            // HospitalId
            // 
            this.HospitalId.Text = "ID";
            // 
            // HospitalName
            // 
            this.HospitalName.Text = "Name";
            // 
            // HospitalStreet
            // 
            this.HospitalStreet.Text = "Street";
            // 
            // HospitalCity
            // 
            this.HospitalCity.Text = "City";
            // 
            // HospitalPostCode
            // 
            this.HospitalPostCode.Text = "Postal Code";
            // 
            // HospitalCountry
            // 
            this.HospitalCountry.Text = "Country";
            // 
            // DoctorsTab
            // 
            this.DoctorsTab.Controls.Add(this.button3);
            this.DoctorsTab.Controls.Add(this.listViewDoctor);
            this.DoctorsTab.Location = new System.Drawing.Point(4, 24);
            this.DoctorsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DoctorsTab.Name = "DoctorsTab";
            this.DoctorsTab.Size = new System.Drawing.Size(723, 343);
            this.DoctorsTab.TabIndex = 2;
            this.DoctorsTab.Text = "Doctors";
            this.DoctorsTab.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(634, 316);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 22);
            this.button3.TabIndex = 1;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listViewDoctor
            // 
            this.listViewDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDoctor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DoctorUsername,
            this.DoctorName,
            this.DoctorEmail,
            this.DoctorPhone});
            this.listViewDoctor.HideSelection = false;
            this.listViewDoctor.Location = new System.Drawing.Point(4, 3);
            this.listViewDoctor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewDoctor.Name = "listViewDoctor";
            this.listViewDoctor.Size = new System.Drawing.Size(713, 309);
            this.listViewDoctor.TabIndex = 0;
            this.listViewDoctor.UseCompatibleStateImageBehavior = false;
            this.listViewDoctor.View = System.Windows.Forms.View.Details;
            this.listViewDoctor.SelectedIndexChanged += new System.EventHandler(this.listViewDoctor_SelectedIndexChanged);
            // 
            // DoctorUsername
            // 
            this.DoctorUsername.Text = "Username";
            this.DoctorUsername.Width = 200;
            // 
            // DoctorName
            // 
            this.DoctorName.Text = "Name";
            this.DoctorName.Width = 200;
            // 
            // DoctorEmail
            // 
            this.DoctorEmail.Text = "Email";
            this.DoctorEmail.Width = 150;
            // 
            // DoctorPhone
            // 
            this.DoctorPhone.Text = "Phone";
            this.DoctorPhone.Width = 100;
            // 
            // AppointmentsTab
            // 
            this.AppointmentsTab.Controls.Add(this.button5);
            this.AppointmentsTab.Controls.Add(this.listViewAppointment);
            this.AppointmentsTab.Location = new System.Drawing.Point(4, 24);
            this.AppointmentsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AppointmentsTab.Name = "AppointmentsTab";
            this.AppointmentsTab.Size = new System.Drawing.Size(723, 343);
            this.AppointmentsTab.TabIndex = 3;
            this.AppointmentsTab.Text = "Appointments";
            this.AppointmentsTab.UseVisualStyleBackColor = true;
            // 
            // listViewAppointment
            // 
            this.listViewAppointment.HideSelection = false;
            this.listViewAppointment.Location = new System.Drawing.Point(3, 2);
            this.listViewAppointment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewAppointment.Name = "listViewAppointment";
            this.listViewAppointment.Size = new System.Drawing.Size(714, 225);
            this.listViewAppointment.TabIndex = 0;
            this.listViewAppointment.UseCompatibleStateImageBehavior = false;
            this.listViewAppointment.View = System.Windows.Forms.View.Details;
            this.listViewAppointment.SelectedIndexChanged += new System.EventHandler(this.listViewAppointment_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(634, 316);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 22);
            this.button5.TabIndex = 1;
            this.button5.Text = "Add";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // AdminHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 389);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AdminHomePage";
            this.Text = "AdminHomePage";
            this.tabControl1.ResumeLayout(false);
            this.SpecialitiesTab.ResumeLayout(false);
            this.HospitalsTab.ResumeLayout(false);
            this.DoctorsTab.ResumeLayout(false);
            this.AppointmentsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SpecialitiesTab;
        private System.Windows.Forms.TabPage HospitalsTab;
        private System.Windows.Forms.TabPage DoctorsTab;
        private System.Windows.Forms.ListView listViewSpeciality;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage Appointments;
        private System.Windows.Forms.ColumnHeader SpecialityId;
        private System.Windows.Forms.ColumnHeader SpecialityName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listViewHospital;
        private System.Windows.Forms.ColumnHeader HospitalId;
        private System.Windows.Forms.ColumnHeader HospitalName;
        private System.Windows.Forms.ColumnHeader HospitalCity;
        private System.Windows.Forms.ColumnHeader HospitalCountry;
        private System.Windows.Forms.ColumnHeader HospitalPostCode;
        private System.Windows.Forms.ColumnHeader HospitalStreet;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView listViewDoctor;
        private System.Windows.Forms.ColumnHeader DoctorUsername;
        private System.Windows.Forms.ColumnHeader DoctorName;
        private System.Windows.Forms.ColumnHeader DoctorEmail;
        private System.Windows.Forms.ColumnHeader DoctorPhone;
        private System.Windows.Forms.ListView listViewAppointment;
        private System.Windows.Forms.TabPage AppointmentsTab;
        private System.Windows.Forms.Button button5;
    }
}