using hospital_manager_models.Models;
using hospital_manager_models.Response_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private long hospitalId;

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
            specialityId = specialities.Single(speciality => speciality.Name == specialityComboBox.SelectedItem.ToString()).Id;
            RefreshHospitals(specialityId);
            hospitalComboBox.Enabled = true;
        }

        private void hospitalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hospitalId = hospitals.Single(hospital => hospital.Name == hospitalComboBox.SelectedItem.ToString()).Id;
            colorDays();
        }

        private void colorDays()
        {
            if (specialityId != 0 && hospitalId != 0)
            {
                DateTime current = monthCalendar1.SelectionStart;
                DateTime From = new DateTime(current.Year, current.Month, 1, 0, 0, 0);
                DateTime To = new DateTime(current.Year, current.Month, DateTime.DaysInMonth(current.Year, current.Month), 0, 0, 0);
                var client = new HttpClient();
                string dateFormat = "yyyy-MM-ddTHH:mm:ss";
                string path = "/appointment/hospital/" + hospitalId + "/speciality/" + specialityId + "/freedays?From=" + From.ToString(dateFormat) + "&To=" + To.ToString(dateFormat);
                Task<HttpResponseMessage> response = client.GetAsync(url + path);
                response.Wait();
                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch available days",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    string result = response.Result.Content.ReadAsStringAsync().Result;
                    List<DateTime> freeDays = JsonConvert.DeserializeObject<ResponseEnvelope<List<DateTime>>>(result).data;
                    monthCalendar1.BoldedDates = freeDays.ToArray();
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            colorDays();
        }
    }
}
