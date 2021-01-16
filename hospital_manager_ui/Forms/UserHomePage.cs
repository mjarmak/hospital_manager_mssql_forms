using hospital_manager_ui.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class UserHomePage : Form
    {
        public UserHomePage()
        {
            InitializeComponent();

            labelUsername.Text = String.IsNullOrEmpty(AuthConfiguration.Username) ? "" : AuthConfiguration.Username;
            labelEmail.Text = String.IsNullOrEmpty(AuthConfiguration.Email) ? "" : AuthConfiguration.Email;
            labelFirstName.Text = String.IsNullOrEmpty(AuthConfiguration.Name) ? "" : AuthConfiguration.Name;
            labelLastName.Text = String.IsNullOrEmpty(AuthConfiguration.LastName) ? "" : AuthConfiguration.LastName;
            labelPhone.Text = String.IsNullOrEmpty(AuthConfiguration.Phone) ? "" : AuthConfiguration.Phone;
            labelBirthday.Text = String.IsNullOrEmpty(AuthConfiguration.Birthdate) ? "" : AuthConfiguration.Birthdate;
            labelGender.Text = String.IsNullOrEmpty(AuthConfiguration.Gender) ? "" : AuthConfiguration.Gender;

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
