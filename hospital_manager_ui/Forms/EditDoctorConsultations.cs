using hospital_manager_models.Models;
using hospital_manager_ui.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class EditDoctorConsultations : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private string doctorUsername;
        private List<SpecialityResponse> specialities;
        private List<HospitalResponse> hospitals;
        private List<ConsultationResponse> consultations = new List<ConsultationResponse>();

        public EditDoctorConsultations(string doctorUsername)
        {
            InitializeComponent();
            DoctorResponse doctor = GetDoctor(doctorUsername);
            if (doctor == null)
            {
                return;
            }
            this.doctorUsername = doctorUsername;
            RefreshSpecialities();
            RefreshHospitals();
            RefreshConsultationsList();
        }

        private DoctorResponse GetDoctor(string doctorUsername)
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/doctor/" + doctorUsername);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch doctor with username " + doctorUsername,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ResponseEnvelope<DoctorResponse>>(result).data;

            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRemoveConsultation_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewConsultations.SelectedIndices;
            if (indices.Count > 0)
            {
                long consultationId = consultations[indices[0]].Id;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthConfiguration.AccessToken);
                Task<HttpResponseMessage> response = client.DeleteAsync(url + "/doctor/" + this.doctorUsername + "/consultations/" + consultationId);
                response.Wait();
                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to remove consultation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                consultations.Remove(consultations[indices[0]]);
                RefreshConsultationsList();
            }
        }

        private void RefreshConsultationsList()
        {
            this.consultations = GetDoctor(this.doctorUsername).Consultations;
            listViewConsultations.Items.Clear();
            listViewConsultations.Items.AddRange(consultations.Select(consultation =>
            {
                return new ListViewItem(new[] { hospitals.Find(hospital => hospital.Id == consultation.HospitalId).Name, specialities.Find(speciality => speciality.Id == consultation.Speciality.Id).Name, consultation.Duration.ToString() });
            }).ToArray());
        }

        private void AddConsultation_Click(object sender, EventArgs e)
        {
            int hospitalIdIndex = comboBoxHospital.SelectedIndex;
            if (hospitalIdIndex < 0)
            {
                MessageBox.Show("Please select a hospital", "Can't to add consultation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            long hospitalId = hospitals[hospitalIdIndex].Id;

            int specialityIdIndex = comboBoxSpeciality.SelectedIndex;
            if (specialityIdIndex < 0)
            {
                MessageBox.Show("Please select a speciality", "Can't to add consultation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            long specialityId = specialities[specialityIdIndex].Id;

            if (comboBoxDuration.SelectedItem == null)
            {
                MessageBox.Show("Please select a duration", "Can't to add consultation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            int duration = Int32.Parse(comboBoxDuration.SelectedItem.ToString());

            ConsultationRequest consultationRequest = new ConsultationRequest(hospitalId, duration, specialityId);

            string json = JsonConvert.SerializeObject(new List<ConsultationRequest> { consultationRequest });
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthConfiguration.AccessToken);
            Task<HttpResponseMessage> response = client.PutAsync(url + "/doctor/" + this.doctorUsername + "/consultations", httpContent);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to add consultation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            RefreshConsultationsList();
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
                comboBoxSpeciality.Items.Clear();
                comboBoxSpeciality.Items.AddRange(specialitiesResponse.Select(speciality =>
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
                comboBoxHospital.Items.Clear();
                comboBoxHospital.Items.AddRange(hospitalsResponse.Select(hospital =>
                {
                    return hospital.Name;
                }).ToArray());
            }
        }
    }
}
