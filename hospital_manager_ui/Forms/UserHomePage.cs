using hospital_manager_models.Models;
using hospital_manager_ui.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class UserHomePage : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private List<AppointmentResponse> appointments;
        private string username;
        private List<HospitalResponse> hospitals;
        public UserHomePage()
        {
            InitializeComponent();

            this.username = AuthConfiguration.Username;

            labelUsername.Text = String.IsNullOrEmpty(AuthConfiguration.Username) ? "" : AuthConfiguration.Username;
            labelEmail.Text = String.IsNullOrEmpty(AuthConfiguration.Email) ? "" : AuthConfiguration.Email;
            labelFirstName.Text = String.IsNullOrEmpty(AuthConfiguration.Name) ? "" : AuthConfiguration.Name;
            labelLastName.Text = String.IsNullOrEmpty(AuthConfiguration.LastName) ? "" : AuthConfiguration.LastName;
            labelPhone.Text = String.IsNullOrEmpty(AuthConfiguration.Phone) ? "" : AuthConfiguration.Phone;
            labelBirthday.Text = String.IsNullOrEmpty(AuthConfiguration.Birthdate) ? "" : AuthConfiguration.Birthdate;
            labelGender.Text = String.IsNullOrEmpty(AuthConfiguration.Gender) ? "" : AuthConfiguration.Gender;

            RefreshHospitals();
            RefreshAppointments();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void RefreshAppointments()
        {
            DateTime current = DateTime.Now;
            DateTime From = new DateTime(current.Year, current.Month, current.Day - 7, 0, 0, 0);
            DateTime To = new DateTime(current.AddYears(10).Year, current.Month, current.Day, 0, 0, 0);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthConfiguration.AccessToken);
            string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            string path = "";
            if (AuthConfiguration.Role != null && AuthConfiguration.Role.Contains("PATIENT"))
            {
                path = "/appointment/patient/" + username + "?from=" + From.ToString(dateFormat) + "&to=" + To.ToString(dateFormat);
            }
            else if (AuthConfiguration.Role != null && AuthConfiguration.Role.Contains("DOCTOR"))
            {
                path = "/appointment/doctor/" + username + "?from=" + From.ToString(dateFormat) + "&to=" + To.ToString(dateFormat);
            }

            Task<HttpResponseMessage> response = client.GetAsync(url + path);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch appointments",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                appointments = JsonConvert.DeserializeObject<ResponseEnvelope<List<AppointmentResponse>>>(result).data;
                listViewAppointmentUser.Items.Clear();
                listViewAppointmentUser.Items.AddRange(appointments.Select(appointment =>
                {
                    string withWho = "";
                    if (AuthConfiguration.Role != null && AuthConfiguration.Role.Contains("PATIENT"))
                    {
                        withWho = appointment.Doctor.Name;
                    }
                    else if (AuthConfiguration.Role != null && AuthConfiguration.Role.Contains("DOCTOR"))
                    {
                        withWho = appointment.PatientUsername;
                    }
                    return new ListViewItem(new[] {
                        hospitals.SingleOrDefault(hospital => hospital.Id == appointment.Room.HospitalId).Name,
                        appointment.Room.Name,
                        appointment.From.ToString(),
                        (appointment.To - appointment.From).TotalMinutes.ToString(),
                        withWho,
                        appointment.Description });
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
                return;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                hospitals = JsonConvert.DeserializeObject<ResponseEnvelope<List<HospitalResponse>>>(result).data;
            }
        }

        private void buttonDeleteAppointment_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewAppointmentUser.SelectedIndices;
            if (indices.Count > 0)
            {
                long appointmentId = appointments[indices[0]].Id;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthConfiguration.AccessToken);
                Task<HttpResponseMessage> response = client.DeleteAsync(url + "/appointment/" + appointmentId);
                response.Wait();
                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to delete appointment with ID " + appointmentId,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                RefreshAppointments();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAppointment f = new AddAppointment();
            f.FormClosed += new FormClosedEventHandler(Form_Closed);
            void Form_Closed(object sender, FormClosedEventArgs e)
            {
                RefreshAppointments();
            }
            f.Show();
        }
    }
}
