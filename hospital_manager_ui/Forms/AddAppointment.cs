using hospital_manager_models.Models;
using hospital_manager_models.Response_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class AddAppointment : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private List<SpecialityResponse> specialities;
        private List<HospitalResponse> hospitals;

        private AppointmentRequest appointmentRequest = new AppointmentRequest();

        private long specialityId;

        public AddAppointment()
        {
            InitializeComponent();
            RefreshSpecialities();
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
                specialityComboBox.Items.Clear();
                specialityComboBox.Items.AddRange(specialitiesResponse.Select(speciality =>
                {
                    return speciality.Name;
                }).ToArray());
            }
        }
        private void RefreshHospitals(long specialityId)
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/hospital/speciality/" + specialityId);
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

        private void specialityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            specialityId = specialities.Single(speciality => speciality.Name == hospitalComboBox.SelectedItem.ToString()).Id;
            RefreshHospitals(specialityId);
        }

        private void hospitalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
