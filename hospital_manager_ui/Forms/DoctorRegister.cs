using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void DoctorRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
