using System;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class DoctorRegister : Form
    {
        public DoctorRegister()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Click_Back(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Close();
        }
    }
}
