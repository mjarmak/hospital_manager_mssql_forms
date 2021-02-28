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
            this.buttonAddSpeciality = new System.Windows.Forms.Button();
            this.HospitalsTab = new System.Windows.Forms.TabPage();
            this.buttonAddRooms = new System.Windows.Forms.Button();
            this.buttonEditHospital = new System.Windows.Forms.Button();
            this.buttonAddHospital = new System.Windows.Forms.Button();
            this.listViewHospital = new System.Windows.Forms.ListView();
            this.HospitalId = new System.Windows.Forms.ColumnHeader();
            this.HospitalName = new System.Windows.Forms.ColumnHeader();
            this.HospitalStreet = new System.Windows.Forms.ColumnHeader();
            this.HospitalCity = new System.Windows.Forms.ColumnHeader();
            this.HospitalPostCode = new System.Windows.Forms.ColumnHeader();
            this.HospitalCountry = new System.Windows.Forms.ColumnHeader();
            this.DoctorsTab = new System.Windows.Forms.TabPage();
            this.buttonEditConsultations = new System.Windows.Forms.Button();
            this.buttonDoctorDetails = new System.Windows.Forms.Button();
            this.AddDoctor = new System.Windows.Forms.Button();
            this.listViewDoctor = new System.Windows.Forms.ListView();
            this.DoctorUsername = new System.Windows.Forms.ColumnHeader();
            this.DoctorName = new System.Windows.Forms.ColumnHeader();
            this.AppointmentsTab = new System.Windows.Forms.TabPage();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.buttonDeleteAppointment = new System.Windows.Forms.Button();
            this.hospitalComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.buttonAddAppointment = new System.Windows.Forms.Button();
            this.listViewAppointment = new System.Windows.Forms.ListView();
            this.Room = new System.Windows.Forms.ColumnHeader();
            this.At = new System.Windows.Forms.ColumnHeader();
            this.Duration = new System.Windows.Forms.ColumnHeader();
            this.Doctor = new System.Windows.Forms.ColumnHeader();
            this.Patient = new System.Windows.Forms.ColumnHeader();
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.accountMenuItemAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutMenuItemAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.SpecialitiesTab.SuspendLayout();
            this.HospitalsTab.SuspendLayout();
            this.DoctorsTab.SuspendLayout();
            this.AppointmentsTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(12, 26);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(893, 491);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // SpecialitiesTab
            // 
            this.SpecialitiesTab.Controls.Add(this.listViewSpeciality);
            this.SpecialitiesTab.Controls.Add(this.buttonAddSpeciality);
            this.SpecialitiesTab.Location = new System.Drawing.Point(4, 24);
            this.SpecialitiesTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpecialitiesTab.Name = "SpecialitiesTab";
            this.SpecialitiesTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpecialitiesTab.Size = new System.Drawing.Size(885, 463);
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
            this.listViewSpeciality.Size = new System.Drawing.Size(876, 430);
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
            // buttonAddSpeciality
            // 
            this.buttonAddSpeciality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddSpeciality.Location = new System.Drawing.Point(807, 437);
            this.buttonAddSpeciality.Name = "buttonAddSpeciality";
            this.buttonAddSpeciality.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSpeciality.TabIndex = 0;
            this.buttonAddSpeciality.Text = "Add";
            this.buttonAddSpeciality.UseVisualStyleBackColor = true;
            this.buttonAddSpeciality.Click += new System.EventHandler(this.button1_Click);
            // 
            // HospitalsTab
            // 
            this.HospitalsTab.Controls.Add(this.buttonAddRooms);
            this.HospitalsTab.Controls.Add(this.buttonEditHospital);
            this.HospitalsTab.Controls.Add(this.buttonAddHospital);
            this.HospitalsTab.Controls.Add(this.listViewHospital);
            this.HospitalsTab.Location = new System.Drawing.Point(4, 24);
            this.HospitalsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HospitalsTab.Name = "HospitalsTab";
            this.HospitalsTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HospitalsTab.Size = new System.Drawing.Size(885, 463);
            this.HospitalsTab.TabIndex = 1;
            this.HospitalsTab.Text = "Hospitals";
            this.HospitalsTab.UseVisualStyleBackColor = true;
            // 
            // buttonAddRooms
            // 
            this.buttonAddRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddRooms.Location = new System.Drawing.Point(626, 437);
            this.buttonAddRooms.Name = "buttonAddRooms";
            this.buttonAddRooms.Size = new System.Drawing.Size(94, 23);
            this.buttonAddRooms.TabIndex = 3;
            this.buttonAddRooms.Text = "Edit Rooms";
            this.buttonAddRooms.UseVisualStyleBackColor = true;
            this.buttonAddRooms.Click += new System.EventHandler(this.buttonAddRooms_Click);
            // 
            // buttonEditHospital
            // 
            this.buttonEditHospital.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditHospital.Location = new System.Drawing.Point(726, 437);
            this.buttonEditHospital.Name = "buttonEditHospital";
            this.buttonEditHospital.Size = new System.Drawing.Size(75, 23);
            this.buttonEditHospital.TabIndex = 2;
            this.buttonEditHospital.Text = "Edit";
            this.buttonEditHospital.UseVisualStyleBackColor = true;
            this.buttonEditHospital.Click += new System.EventHandler(this.buttonEditHospital_Click);
            // 
            // buttonAddHospital
            // 
            this.buttonAddHospital.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddHospital.Location = new System.Drawing.Point(807, 437);
            this.buttonAddHospital.Name = "buttonAddHospital";
            this.buttonAddHospital.Size = new System.Drawing.Size(75, 23);
            this.buttonAddHospital.TabIndex = 1;
            this.buttonAddHospital.Text = "Add";
            this.buttonAddHospital.UseVisualStyleBackColor = true;
            this.buttonAddHospital.Click += new System.EventHandler(this.button2_Click);
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
            this.listViewHospital.Size = new System.Drawing.Size(876, 430);
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
            this.DoctorsTab.Controls.Add(this.buttonEditConsultations);
            this.DoctorsTab.Controls.Add(this.buttonDoctorDetails);
            this.DoctorsTab.Controls.Add(this.AddDoctor);
            this.DoctorsTab.Controls.Add(this.listViewDoctor);
            this.DoctorsTab.Location = new System.Drawing.Point(4, 24);
            this.DoctorsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DoctorsTab.Name = "DoctorsTab";
            this.DoctorsTab.Size = new System.Drawing.Size(885, 463);
            this.DoctorsTab.TabIndex = 2;
            this.DoctorsTab.Text = "Doctors";
            this.DoctorsTab.UseVisualStyleBackColor = true;
            // 
            // buttonEditConsultations
            // 
            this.buttonEditConsultations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditConsultations.Location = new System.Drawing.Point(597, 437);
            this.buttonEditConsultations.Name = "buttonEditConsultations";
            this.buttonEditConsultations.Size = new System.Drawing.Size(123, 23);
            this.buttonEditConsultations.TabIndex = 3;
            this.buttonEditConsultations.Text = "Edit Consultations";
            this.buttonEditConsultations.UseVisualStyleBackColor = true;
            this.buttonEditConsultations.Click += new System.EventHandler(this.buttonEditConsultations_Click);
            // 
            // buttonDoctorDetails
            // 
            this.buttonDoctorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDoctorDetails.Location = new System.Drawing.Point(726, 437);
            this.buttonDoctorDetails.Name = "buttonDoctorDetails";
            this.buttonDoctorDetails.Size = new System.Drawing.Size(75, 23);
            this.buttonDoctorDetails.TabIndex = 2;
            this.buttonDoctorDetails.Text = "Details";
            this.buttonDoctorDetails.UseVisualStyleBackColor = true;
            this.buttonDoctorDetails.Click += new System.EventHandler(this.buttonDoctorDetails_Click);
            // 
            // AddDoctor
            // 
            this.AddDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddDoctor.Location = new System.Drawing.Point(807, 437);
            this.AddDoctor.Name = "AddDoctor";
            this.AddDoctor.Size = new System.Drawing.Size(75, 23);
            this.AddDoctor.TabIndex = 1;
            this.AddDoctor.Text = "Add";
            this.AddDoctor.UseVisualStyleBackColor = true;
            this.AddDoctor.Click += new System.EventHandler(this.button3_Click);
            // 
            // listViewDoctor
            // 
            this.listViewDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDoctor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DoctorUsername,
            this.DoctorName});
            this.listViewDoctor.HideSelection = false;
            this.listViewDoctor.Location = new System.Drawing.Point(3, 2);
            this.listViewDoctor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewDoctor.Name = "listViewDoctor";
            this.listViewDoctor.Size = new System.Drawing.Size(876, 430);
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
            // AppointmentsTab
            // 
            this.AppointmentsTab.Controls.Add(this.btnConfirm);
            this.AppointmentsTab.Controls.Add(this.buttonDeleteAppointment);
            this.AppointmentsTab.Controls.Add(this.hospitalComboBox);
            this.AppointmentsTab.Controls.Add(this.label1);
            this.AppointmentsTab.Controls.Add(this.monthCalendar1);
            this.AppointmentsTab.Controls.Add(this.buttonAddAppointment);
            this.AppointmentsTab.Controls.Add(this.listViewAppointment);
            this.AppointmentsTab.Location = new System.Drawing.Point(4, 24);
            this.AppointmentsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AppointmentsTab.Name = "AppointmentsTab";
            this.AppointmentsTab.Size = new System.Drawing.Size(885, 463);
            this.AppointmentsTab.TabIndex = 3;
            this.AppointmentsTab.Text = "Appointments";
            this.AppointmentsTab.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(645, 437);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonDeleteAppointment
            // 
            this.buttonDeleteAppointment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteAppointment.Location = new System.Drawing.Point(726, 437);
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
            // buttonAddAppointment
            // 
            this.buttonAddAppointment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddAppointment.Location = new System.Drawing.Point(807, 437);
            this.buttonAddAppointment.Name = "buttonAddAppointment";
            this.buttonAddAppointment.Size = new System.Drawing.Size(75, 23);
            this.buttonAddAppointment.TabIndex = 1;
            this.buttonAddAppointment.Text = "Add";
            this.buttonAddAppointment.UseVisualStyleBackColor = true;
            this.buttonAddAppointment.Click += new System.EventHandler(this.button5_Click);
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
            this.listViewAppointment.Size = new System.Drawing.Size(643, 432);
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleName = "accountMenuStrip";
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(64, 20);
            this.toolStripMenuItem1.Text = "Account";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.AccessibleName = "logoutMenuStrip";
            this.toolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem2.Text = "Log Out";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountMenuItemAdmin});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(917, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // accountMenuItemAdmin
            // 
            this.accountMenuItemAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutMenuItemAdmin});
            this.accountMenuItemAdmin.Name = "accountMenuItemAdmin";
            this.accountMenuItemAdmin.Size = new System.Drawing.Size(64, 20);
            this.accountMenuItemAdmin.Text = "Account";
            // 
            // logoutMenuItemAdmin
            // 
            this.logoutMenuItemAdmin.AccessibleName = "logoutMenuItemAdmin";
            this.logoutMenuItemAdmin.Name = "logoutMenuItemAdmin";
            this.logoutMenuItemAdmin.Size = new System.Drawing.Size(117, 22);
            this.logoutMenuItemAdmin.Text = "Log Out";
            this.logoutMenuItemAdmin.Click += new System.EventHandler(this.logoutMenuStrip_Clicked);
            // 
            // AdminHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 528);
            this.Controls.Add(this.menuStrip1);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SpecialitiesTab;
        private System.Windows.Forms.TabPage HospitalsTab;
        private System.Windows.Forms.TabPage DoctorsTab;
        private System.Windows.Forms.ListView listViewSpeciality;
        private System.Windows.Forms.Button buttonAddSpeciality;
        private System.Windows.Forms.TabPage Appointments;
        private System.Windows.Forms.ColumnHeader SpecialityId;
        private System.Windows.Forms.ColumnHeader SpecialityName;
        private System.Windows.Forms.Button buttonAddHospital;
        private System.Windows.Forms.ListView listViewHospital;
        private System.Windows.Forms.ColumnHeader HospitalId;
        private System.Windows.Forms.ColumnHeader HospitalName;
        private System.Windows.Forms.ColumnHeader HospitalCity;
        private System.Windows.Forms.ColumnHeader HospitalCountry;
        private System.Windows.Forms.ColumnHeader HospitalPostCode;
        private System.Windows.Forms.ColumnHeader HospitalStreet;
        private System.Windows.Forms.Button AddDoctor;
        private System.Windows.Forms.ListView listViewDoctor;
        private System.Windows.Forms.ColumnHeader DoctorUsername;
        private System.Windows.Forms.ColumnHeader DoctorName;
        private System.Windows.Forms.ListView listViewAppointment;
        private System.Windows.Forms.TabPage AppointmentsTab;
        private System.Windows.Forms.Button buttonAddAppointment;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ComboBox hospitalComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader Room;
        private System.Windows.Forms.ColumnHeader At;
        private System.Windows.Forms.ColumnHeader Duration;
        private System.Windows.Forms.ColumnHeader Doctor;
        private System.Windows.Forms.ColumnHeader Patient;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.Button buttonDeleteAppointment;
        private System.Windows.Forms.Button buttonEditHospital;
        private System.Windows.Forms.Button buttonAddRooms;
        private System.Windows.Forms.Button buttonDoctorDetails;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem accountMenuItemAdmin;
        private System.Windows.Forms.ToolStripMenuItem logoutMenuItemAdmin;
        private System.Windows.Forms.Button buttonEditConsultations;
    }
}