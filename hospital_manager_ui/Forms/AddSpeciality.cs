﻿using hospital_manager_models.Models;
using hospital_manager_models.Response_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class AddSpeciality : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;

        public AddSpeciality()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();

            string json = JsonConvert.SerializeObject(new SpecialityRequest(textBox1.Text));

            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync(url + "/speciality", httpContent);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to add speciality",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }
    }
}
