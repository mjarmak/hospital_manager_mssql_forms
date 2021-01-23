using hospital_manager_models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class AddAppointment : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private List<SpecialityResponse> specialities;
        private List<HospitalResponse> hospitals;
        private List<RoomResponse> rooms;
        private List<DoctorResponse> doctors;
        private List<AppointmentRequest> appointmentSuggestions;
        private AppointmentRequest appointmentSuggestion;

        private long specialityId;
        private long hospitalId;

        public AddAppointment()
        {
            InitializeComponent();
            RefreshSpecialities();
            RefreshDoctors();
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
        private void RefreshRooms()
        {
            if (specialityId != 0 && hospitalId != 0)
            {
                var client = new HttpClient();
                Task<HttpResponseMessage> response = client.GetAsync(url + "/room/hospital/" + hospitalId + "/speciality/" + specialityId);
                response.Wait();
                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch rooms",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    string result = response.Result.Content.ReadAsStringAsync().Result;
                    List<RoomResponse> roomResponse = JsonConvert.DeserializeObject<ResponseEnvelope<List<RoomResponse>>>(result).data;
                    rooms = roomResponse;
                }
            }
        }
        private void RefreshDoctors()
        {
            var client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(url + "/doctor/all");
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch doctors",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                List<DoctorResponse> doctorResponse = JsonConvert.DeserializeObject<ResponseEnvelope<List<DoctorResponse>>>(result).data;
                doctors = doctorResponse;
            }
        }

        private void specialityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            specialityId = specialities.Single(speciality => speciality.Name == specialityComboBox.SelectedItem.ToString()).Id;
            RefreshHospitals(specialityId);
            hospitalComboBox.Enabled = true;
            colorDays();
            RefreshRooms();
        }

        private void hospitalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hospitalId = hospitals.Single(hospital => hospital.Name == hospitalComboBox.SelectedItem.ToString()).Id;
            colorDays();
            RefreshRooms();
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
                    return;
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
            if (specialityId != 0 && hospitalId != 0)
            {
                colorDays();
                RefreshRooms();
                DateTime current = monthCalendar1.SelectionStart;
                DateTime From = new DateTime(current.Year, current.Month, current.Day, 0, 0, 0);
                DateTime To = new DateTime(current.Year, current.Month, current.Day, 23, 59, 59);
                var client = new HttpClient();
                string dateFormat = "yyyy-MM-ddTHH:mm:ss";
                string path = "/appointment/hospital/" + hospitalId + "/speciality/" + specialityId + "/suggestions?From=" + From.ToString(dateFormat) + "&To=" + To.ToString(dateFormat);
                Task<HttpResponseMessage> response = client.GetAsync(url + path);
                response.Wait();
                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch appointment suggestions",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string result = response.Result.Content.ReadAsStringAsync().Result;
                    appointmentSuggestions = JsonConvert.DeserializeObject<ResponseEnvelope<List<AppointmentRequest>>>(result).data;

                    listViewSuggestions.Items.Clear();
                    listViewSuggestions.Items.AddRange(appointmentSuggestions.Select(appointmentSuggestion =>
                    {
                        return new ListViewItem(new[] {
                        rooms.SingleOrDefault(room => room.Id == appointmentSuggestion.RoomId).Name,
                        appointmentSuggestion.From.ToString("HH:mm"),
                        (appointmentSuggestion.To - appointmentSuggestion.From).TotalMinutes.ToString(),
                        doctors.SingleOrDefault(doctor => doctor.Username == appointmentSuggestion.DoctorUsername).Name
                        });
                    }).ToArray());
                }
            }
        }

        private void listViewSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewSuggestions.SelectedIndices;
            if (indices.Count > 0)
            {
                appointmentSuggestion = appointmentSuggestions[indices[0]];
                textBoxAt.Text = appointmentSuggestion.From.ToString("HH:mm");
                textBoxDescription.Text = appointmentSuggestion.Description;
                textBoxDoctor.Text = doctors.SingleOrDefault(doctor => doctor.Username == appointmentSuggestion.DoctorUsername).Name;
                textBoxDuration.Text = (appointmentSuggestion.To - appointmentSuggestion.From).TotalMinutes.ToString();
                textBoxRoom.Text = rooms.SingleOrDefault(room => room.Id == appointmentSuggestion.RoomId).Name;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            appointmentSuggestion.PatientUsername = textBoxPatient.Text;
            appointmentSuggestion.Description = textBoxDescription.Text;

            if (String.IsNullOrWhiteSpace(appointmentSuggestion.PatientUsername))
            {
                MessageBox.Show("Patient missing", "Please add the patient username",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrWhiteSpace(appointmentSuggestion.Description))
            {
                MessageBox.Show("Description missing", "Please give a description for the appointment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(appointmentSuggestion);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync(url + "/appointment", httpContent);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to add appointment",
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
