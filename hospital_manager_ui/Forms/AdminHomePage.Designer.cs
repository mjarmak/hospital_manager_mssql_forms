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
            this.buttonEditAppointment = new System.Windows.Forms.Button();
            this.buttonDeleteAppointment = new System.Windows.Forms.Button();
            this.hospitalComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.button5 = new System.Windows.Forms.Button();
            this.listViewAppointment = new System.Windows.Forms.ListView();
            this.Room = new System.Windows.Forms.ColumnHeader();
            this.At = new System.Windows.Forms.ColumnHeader();
            this.Duration = new System.Windows.Forms.ColumnHeader();
            this.Doctor = new System.Windows.Forms.ColumnHeader();
            this.Patient = new System.Windows.Forms.ColumnHeader();
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.buttonEditHospital = new System.Windows.Forms.Button();
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
            this.tabControl1.Location = new System.Drawing.Point(12, 11);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(890, 494);
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
            this.SpecialitiesTab.Size = new System.Drawing.Size(882, 466);
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
            this.listViewSpeciality.Size = new System.Drawing.Size(876, 433);
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
            this.button1.Location = new System.Drawing.Point(804, 440);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HospitalsTab
            // 
            this.HospitalsTab.Controls.Add(this.buttonEditHospital);
            this.HospitalsTab.Controls.Add(this.button2);
            this.HospitalsTab.Controls.Add(this.listViewHospital);
            this.HospitalsTab.Location = new System.Drawing.Point(4, 24);
            this.HospitalsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HospitalsTab.Name = "HospitalsTab";
            this.HospitalsTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HospitalsTab.Size = new System.Drawing.Size(882, 466);
            this.HospitalsTab.TabIndex = 1;
            this.HospitalsTab.Text = "Hospitals";
            this.HospitalsTab.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(804, 440);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
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
            this.listViewHospital.Size = new System.Drawing.Size(876, 433);
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
            this.DoctorsTab.Size = new System.Drawing.Size(882, 466);
            this.DoctorsTab.TabIndex = 2;
            this.DoctorsTab.Text = "Doctors";
            this.DoctorsTab.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(804, 440);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
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
            this.listViewDoctor.Location = new System.Drawing.Point(3, 2);
            this.listViewDoctor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewDoctor.Name = "listViewDoctor";
            this.listViewDoctor.Size = new System.Drawing.Size(876, 433);
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
            this.AppointmentsTab.Controls.Add(this.buttonEditAppointment);
            this.AppointmentsTab.Controls.Add(this.buttonDeleteAppointment);
            this.AppointmentsTab.Controls.Add(this.hospitalComboBox);
            this.AppointmentsTab.Controls.Add(this.label1);
            this.AppointmentsTab.Controls.Add(this.monthCalendar1);
            this.AppointmentsTab.Controls.Add(this.button5);
            this.AppointmentsTab.Controls.Add(this.listViewAppointment);
            this.AppointmentsTab.Location = new System.Drawing.Point(4, 24);
            this.AppointmentsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AppointmentsTab.Name = "AppointmentsTab";
            this.AppointmentsTab.Size = new System.Drawing.Size(882, 466);
            this.AppointmentsTab.TabIndex = 3;
            this.AppointmentsTab.Text = "Appointments";
            this.AppointmentsTab.UseVisualStyleBackColor = true;
            // 
            // buttonEditAppointment
            // 
            this.buttonEditAppointment.Location = new System.Drawing.Point(642, 440);
            this.buttonEditAppointment.Name = "buttonEditAppointment";
            this.buttonEditAppointment.Size = new System.Drawing.Size(75, 23);
            this.buttonEditAppointment.TabIndex = 4;
            this.buttonEditAppointment.Text = "Edit";
            this.buttonEditAppointment.UseVisualStyleBackColor = true;
            this.buttonEditAppointment.Click += new System.EventHandler(this.buttonEditAppointment_Click);
            // 
            // buttonDeleteAppointment
            // 
            this.buttonDeleteAppointment.Location = new System.Drawing.Point(723, 440);
            this.buttonDeleteAppointment.Name = "buttonDeleteAppointment";
            this.buttonDeleteAppointment.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteAppointment.TabIndex = 3;
            this.buttonDeleteAppointment.Text = "Delete";
            this.buttonDeleteAppointment.UseVisualStyleBackColor = true;
            this.buttonDeleteAppointment.Click += new System.EventHandler(this.buttonDeleteAppointment_Click);
            // 
            // hospitalComboBox
            // 
            this.hospitalComboBox.FormattingEnabled = true;
            this.hospitalComboBox.Location = new System.Drawing.Point(63, 9);
            this.hospitalComboBox.Name = "hospitalComboBox";
            this.hospitalComboBox.Size = new System.Drawing.Size(167, 23);
            this.hospitalComboBox.TabIndex = 0;
            this.hospitalComboBox.SelectedIndexChanged += new System.EventHandler(this.hospitalComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hospital:";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(3, 36);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 2;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(804, 440);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "Add";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listViewAppointment
            // 
            this.listViewAppointment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAppointment.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Room,
            this.At,
            this.Duration,
            this.Doctor,
            this.Patient,
            this.Description});
            this.listViewAppointment.HideSelection = false;
            this.listViewAppointment.Location = new System.Drawing.Point(239, 0);
            this.listViewAppointment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewAppointment.Name = "listViewAppointment";
            this.listViewAppointment.Size = new System.Drawing.Size(640, 435);
            this.listViewAppointment.TabIndex = 0;
            this.listViewAppointment.UseCompatibleStateImageBehavior = false;
            this.listViewAppointment.View = System.Windows.Forms.View.Details;
            this.listViewAppointment.SelectedIndexChanged += new System.EventHandler(this.listViewAppointment_SelectedIndexChanged);
            // 
            // Room
            // 
            this.Room.Text = "Room";
            // 
            // At
            // 
            this.At.Text = "At";
            this.At.Width = 100;
            // 
            // Duration
            // 
            this.Duration.Text = "Duration";
            // 
            // Doctor
            // 
            this.Doctor.Text = "Doctor";
            this.Doctor.Width = 120;
            // 
            // Patient
            // 
            this.Patient.Text = "Patient";
            this.Patient.Width = 120;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 150;
            // 
            // buttonEditHospital
            // 
            this.buttonEditHospital.Location = new System.Drawing.Point(723, 440);
            this.buttonEditHospital.Name = "buttonEditHospital";
            this.buttonEditHospital.Size = new System.Drawing.Size(75, 23);
            this.buttonEditHospital.TabIndex = 2;
            this.buttonEditHospital.Text = "Edit";
            this.buttonEditHospital.UseVisualStyleBackColor = true;
            this.buttonEditHospital.Click += new System.EventHandler(this.buttonEditHospital_Click);
            // 
            // AdminHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 516);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AdminHomePage";
            this.Text = "AdminHomePage";
            this.tabControl1.ResumeLayout(false);
            this.SpecialitiesTab.ResumeLayout(false);
            this.HospitalsTab.ResumeLayout(false);
            this.DoctorsTab.ResumeLayout(false);
            this.AppointmentsTab.ResumeLayout(false);
            this.AppointmentsTab.PerformLayout();
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
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ComboBox hospitalComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader Room;
        private System.Windows.Forms.ColumnHeader At;
        private System.Windows.Forms.ColumnHeader Duration;
        private System.Windows.Forms.ColumnHeader Doctor;
        private System.Windows.Forms.ColumnHeader Patient;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.Button buttonEditAppointment;
        private System.Windows.Forms.Button buttonDeleteAppointment;
        private System.Windows.Forms.Button buttonEditHospital;
    }
}