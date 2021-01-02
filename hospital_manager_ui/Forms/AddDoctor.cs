using hospital_manager_data_access.Entities;
using hospital_manager_models.Models;
using hospital_manager_models.Response_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class AddDoctor : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private List<SpecialityResponse> specialities;
        private List<HospitalResponse> hospitals;
        private List<ConsultationRequest> consultationRequests = new List<ConsultationRequest>();

        public AddDoctor()
        {
            InitializeComponent();
            RefreshSpecialities();
            RefreshHospitals();
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
                List<SpecialityResponse> specialitiesResponse = JsonConvert.DeserializeObject<ResponseEnvelope<List<SpecialityResponse>>>(result).data;
                specialities = specialitiesResponse;
                specialititesList.Items.Clear();
                specialititesList.Items.AddRange(specialitiesResponse.Select(speciality =>
                {
                    return speciality.Name;
                }).ToArray());
                consulationSpecialityComboBox.Items.Clear();
                consulationSpecialityComboBox.Items.AddRange(specialitiesResponse.Select(speciality =>
                {
                    return speciality.Name;
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
                List<HospitalResponse> hospitalsResponse = JsonConvert.DeserializeObject<ResponseEnvelope<List<HospitalResponse>>>(result).data;
                hospitals = hospitalsResponse;
                hospitalComboBox.Items.Clear();
                hospitalComboBox.Items.AddRange(hospitalsResponse.Select(hospital =>
                {
                    return hospital.Name;
                }).ToArray());
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();

            DoctorRequest doctorRequest = new DoctorRequest();

            List<string> checkedSpecialities = specialititesList.CheckedItems.Cast<string>().ToList();
            doctorRequest.SpecialityIds = checkedSpecialities.Select(name => specialities.Find(speciality => speciality.Name == name).Id).ToList();

            string json = null;
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync(url + "/hospital", httpContent);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddConsultation_Click(object sender, EventArgs e)
        {
            if (hospitalComboBox.SelectedItem != null && consultationDurationComboBox.SelectedItem != null)
            {
                int duration = Int32.Parse(consultationDurationComboBox.SelectedItem.ToString());
                long hospitalId = hospitals.Find(hospital => hospital.Name == hospitalComboBox.SelectedItem.ToString()).Id;
                long specialityId = specialities.Find(speciality => speciality.Name == consulationSpecialityComboBox.SelectedItem.ToString()).Id;

                if (consultationRequests.Find(consultation => consultation.HospitalId == hospitalId && consultation.SpecialityId == specialityId) != null)
                {
                    MessageBox.Show("Choose a different hospital or speciality", "Consultation already exists",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                consultationRequests.Add(new ConsultationRequest(hospitalId, duration, specialityId));
                RefreshConsultationsList();          
            } else
            {
                MessageBox.Show("Please select a hospital and consultation duration", "No hospital or consultation duration selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void RefreshConsultationsList()
        {
            consultationsLlistView.Items.Clear();
            consultationsLlistView.Items.AddRange(consultationRequests.Select(consultation =>
            {
                return new ListViewItem(new[] { hospitals.Find(hospital => hospital.Id == consultation.HospitalId).Name, specialities.Find(speciality => speciality.Id == consultation.SpecialityId).Name, consultation.Duration.ToString() });
            }).ToArray());
        }
    }
}
