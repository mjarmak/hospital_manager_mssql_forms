using hospital_manager_models.Models;
using hospital_manager_ui.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class DoctorDetails : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private protected string urlOauth = ApplicationConfiguration.oauthUrl;
        private List<HospitalResponse> hospitals;
        public DoctorDetails(string doctorUsername)
        {
            InitializeComponent();
            RefreshHospitals();
            UserDetailsResponse userDetailsResponse = GetDoctorAccountInfo(doctorUsername);
            if (userDetailsResponse == null)
            {
                return;
            }
            labelEmail.Text = userDetailsResponse.Email;
            labelPhone.Text = userDetailsResponse.Phone;
            labelUsername.Text = userDetailsResponse.Username;

            DoctorResponse doctorResponse = GetDoctor(doctorUsername);
            if (doctorResponse == null)
            {
                return;
            }
            labelName.Text = doctorResponse.Name;

            listViewSpecialities.Items.Clear();
            listViewSpecialities.Items.AddRange(doctorResponse.Specialities.Select(speciality =>
            {
                return new ListViewItem(new[] { speciality.Name });
            }).ToArray());
            listViewHospitals.Items.Clear();
            listViewHospitals.Items.AddRange(doctorResponse.Consultations.Select(consultation =>
            {
                return new ListViewItem(new[] {
                    hospitals.Find(hospital => hospital.Id == consultation.HospitalId).Name,
                    consultation.Speciality.Name, consultation.Duration.ToString() });
            }).ToArray());
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private UserDetailsResponse GetDoctorAccountInfo(string doctorUsername)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthConfiguration.AccessToken);
            Task<HttpResponseMessage> response = client.GetAsync(urlOauth + "/user/username?username=" + doctorUsername);
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
                return JsonConvert.DeserializeObject<ResponseEnvelope<UserDetailsResponse>>(result).data;

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
                return;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                hospitals = JsonConvert.DeserializeObject<ResponseEnvelope<List<HospitalResponse>>>(result).data;
            }
        }
    }
}
