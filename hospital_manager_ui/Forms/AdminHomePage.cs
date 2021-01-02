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
using hospital_manager_models.Models;
using System.Linq;
using hospital_manager_models.Response_Models;

namespace hospital_manager_ui.Forms
{
    public partial class AdminHomePage : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;

        public AdminHomePage()
        {
            InitializeComponent();
            RefreshSpecialities();
            RefreshHospitals();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == SpecialitiesTab)
            {
                RefreshSpecialities();
            }
            else if (tabControl1.SelectedTab == HospitalsTab)
            {
                RefreshHospitals();
            }
            else if (tabControl1.SelectedTab == DoctorsTab)
            {

            }
            else if (tabControl1.SelectedTab == AppointmentsTab)
            {

            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Specialities_Click(object sender, EventArgs e)
        {

        }

        private void listViewHospital_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewAppointment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshSpecialities()
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/speciality/all");
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch specialities",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                List<SpecialityResponse> specialities = JsonConvert.DeserializeObject<ResponseEnvelope<List<SpecialityResponse>>>(result).data;

                listViewSpeciality.Items.Clear();
                listViewSpeciality.Items.AddRange(specialities.Select(speciality =>
                {
                    return new ListViewItem(new[] { speciality.Id.ToString(), speciality.Name });
                }).ToArray());
            }
        }
        private void RefreshHospitals()
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/hospital/all");
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch hospitals",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                List<HospitalResponse> hospitals = JsonConvert.DeserializeObject<ResponseEnvelope<List<HospitalResponse>>>(result).data;

                listViewHospital.Items.Clear();
                listViewHospital.Items.AddRange(hospitals.Select(hospital =>
                {
                    return new ListViewItem(new[] { hospital.Id.ToString(), hospital.Name, hospital.Address.Street, hospital.Address.City, hospital.Address.PostalCode, hospital.Address.Country });
                }).ToArray());
            }
        }
        private void RefreshDoctors()
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/speciality/all");
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch specialities",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                List<SpecialityResponse> specialities = JsonConvert.DeserializeObject<ResponseEnvelope<List<SpecialityResponse>>>(result).data;

                listViewSpeciality.Items.Clear();
                listViewSpeciality.Items.AddRange(specialities.Select(speciality =>
                {
                    return new ListViewItem(new[] { speciality.Id.ToString(), speciality.Name });
                }).ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSpeciality f = new AddSpeciality();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddHospital f = new AddHospital();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddDoctor f = new AddDoctor();
            f.Show();
        }
    }
}
