namespace hospital_manager_ui.Forms
{
    partial class AddAppointment
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
            System.Windows.Forms.ColumnHeader At;
            this.hospitalComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.specialityComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.listViewSuggestions = new System.Windows.Forms.ListView();
            this.Room = new System.Windows.Forms.ColumnHeader();
            this.Duration = new System.Windows.Forms.ColumnHeader();
            this.Doctor = new System.Windows.Forms.ColumnHeader();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDoctor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDuration = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxRoom = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePickerAppointmenFrom = new System.Windows.Forms.DateTimePicker();
            this.labelTimeSlotBooked = new System.Windows.Forms.Label();
            this.comboBoxPatient = new System.Windows.Forms.ComboBox();
            At = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // At
            // 
            At.Text = "At";
            At.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            At.Width = 150;
            // 
            // hospitalComboBox
            // 
            this.hospitalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hospitalComboBox.Enabled = false;
            this.hospitalComboBox.FormattingEnabled = true;
            this.hospitalComboBox.Location = new System.Drawing.Point(471, 12);
            this.hospitalComboBox.Name = "hospitalComboBox";
            this.hospitalComboBox.Size = new System.Drawing.Size(240, 23);
            this.hospitalComboBox.TabIndex = 0;
            this.hospitalComboBox.SelectedIndexChanged += new System.EventHandler(this.hospitalComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(411, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hospital:";
            // 
            // specialityComboBox
            // 
            this.specialityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.specialityComboBox.FormattingEnabled = true;
            this.specialityComboBox.Location = new System.Drawing.Point(78, 12);
            this.specialityComboBox.Name = "specialityComboBox";
            this.specialityComboBox.Size = new System.Drawing.Size(240, 23);
            this.specialityComboBox.TabIndex = 4;
            this.specialityComboBox.SelectedIndexChanged += new System.EventHandler(this.specialityComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Speciality:";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.BackColor = System.Drawing.SystemColors.Control;
            this.monthCalendar1.Location = new System.Drawing.Point(18, 47);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 6;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // listViewSuggestions
            // 
            this.listViewSuggestions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Room,
            At,
            this.Duration,
            this.Doctor});
            this.listViewSuggestions.HideSelection = false;
            this.listViewSuggestions.Location = new System.Drawing.Point(257, 47);
            this.listViewSuggestions.Name = "listViewSuggestions";
            this.listViewSuggestions.Size = new System.Drawing.Size(531, 162);
            this.listViewSuggestions.TabIndex = 7;
            this.listViewSuggestions.UseCompatibleStateImageBehavior = false;
            this.listViewSuggestions.View = System.Windows.Forms.View.Details;
            this.listViewSuggestions.SelectedIndexChanged += new System.EventHandler(this.listViewSuggestions_SelectedIndexChanged);
            // 
            // Room
            // 
            this.Room.Text = "Room";
            // 
            // Duration
            // 
            this.Duration.Text = "Duration (minutes)";
            this.Duration.Width = 120;
            // 
            // Doctor
            // 
            this.Doctor.Text = "Doctor";
            this.Doctor.Width = 180;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(127, 250);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(212, 23);
            this.textBoxDescription.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Patient Username:\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 253);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Description:";
            // 
            // textBoxDoctor
            // 
            this.textBoxDoctor.Enabled = false;
            this.textBoxDoctor.Location = new System.Drawing.Point(127, 308);
            this.textBoxDoctor.Name = "textBoxDoctor";
            this.textBoxDoctor.Size = new System.Drawing.Size(212, 23);
            this.textBoxDoctor.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "At:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 311);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Doctor:";
            // 
            // textBoxDuration
            // 
            this.textBoxDuration.Enabled = false;
            this.textBoxDuration.Location = new System.Drawing.Point(127, 337);
            this.textBoxDuration.Name = "textBoxDuration";
            this.textBoxDuration.Size = new System.Drawing.Size(212, 23);
            this.textBoxDuration.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 340);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Duration:";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(423, 415);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(325, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxRoom
            // 
            this.textBoxRoom.Enabled = false;
            this.textBoxRoom.Location = new System.Drawing.Point(127, 366);
            this.textBoxRoom.Name = "textBoxRoom";
            this.textBoxRoom.Size = new System.Drawing.Size(212, 23);
            this.textBoxRoom.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 369);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Room:";
            // 
            // dateTimePickerAppointmenFrom
            // 
            this.dateTimePickerAppointmenFrom.Enabled = false;
            this.dateTimePickerAppointmenFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerAppointmenFrom.Location = new System.Drawing.Point(127, 279);
            this.dateTimePickerAppointmenFrom.Name = "dateTimePickerAppointmenFrom";
            this.dateTimePickerAppointmenFrom.Size = new System.Drawing.Size(212, 23);
            this.dateTimePickerAppointmenFrom.TabIndex = 14;
            this.dateTimePickerAppointmenFrom.ValueChanged += new System.EventHandler(this.dateTimePickerAppointmenFrom_ValueChanged);
            // 
            // labelTimeSlotBooked
            // 
            this.labelTimeSlotBooked.AutoSize = true;
            this.labelTimeSlotBooked.Location = new System.Drawing.Point(345, 282);
            this.labelTimeSlotBooked.Name = "labelTimeSlotBooked";
            this.labelTimeSlotBooked.Size = new System.Drawing.Size(375, 15);
            this.labelTimeSlotBooked.TabIndex = 15;
            this.labelTimeSlotBooked.Text = "*Time slot already booked or not in the same half day for the hospital.";
            this.labelTimeSlotBooked.Visible = false;
            // 
            // comboBoxPatient
            // 
            this.comboBoxPatient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxPatient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPatient.FormattingEnabled = true;
            this.comboBoxPatient.Location = new System.Drawing.Point(127, 221);
            this.comboBoxPatient.Name = "comboBoxPatient";
            this.comboBoxPatient.Size = new System.Drawing.Size(212, 23);
            this.comboBoxPatient.TabIndex = 16;
            // 
            // AddAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxPatient);
            this.Controls.Add(this.labelTimeSlotBooked);
            this.Controls.Add(this.dateTimePickerAppointmenFrom);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDuration);
            this.Controls.Add(this.textBoxDoctor);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxRoom);
            this.Controls.Add(this.listViewSuggestions);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.specialityComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hospitalComboBox);
            this.Name = "AddAppointment";
            this.Text = "AddAppointment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox hospitalComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox specialityComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ListView listViewSuggestions;
        private System.Windows.Forms.ColumnHeader Room;
        private System.Windows.Forms.ColumnHeader Duration;
        private System.Windows.Forms.ColumnHeader Doctor;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDoctor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDuration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxRoom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePickerAppointmenFrom;
        private System.Windows.Forms.Label labelTimeSlotBooked;
        private System.Windows.Forms.ComboBox comboBoxPatient;
    }
}