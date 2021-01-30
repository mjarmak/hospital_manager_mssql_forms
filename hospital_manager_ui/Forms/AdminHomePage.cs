using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using hospital_manager_models.Models;
using System.Linq;
using hospital_manager_ui.Configuration;
using System.Net.Http.Headers;

namespace hospital_manager_ui.Forms
{
    public partial class AdminHomePage : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private List<HospitalResponse> hospitals;
        private List<AppointmentResponse> appointments;
        private List<DoctorResponse> doctors;

        public AdminHomePage()
        {
            InitializeComponent();
            RefreshSpecialities();
            RefreshHospitals();
            RefreshDoctors();
            RefreshAppointments();
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
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch specialities.",
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
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch hospitals.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                hospitals = JsonConvert.DeserializeObject<ResponseEnvelope<List<HospitalResponse>>>(result).data;
                listViewHospital.Items.Clear();
                listViewHospital.Items.AddRange(hospitals.Select(hospital =>
                {
                    return new ListViewItem(new[] { hospital.Id.ToString(), hospital.Name, hospital.Address.Street, hospital.Address.City, hospital.Address.PostalCode, hospital.Address.Country });
                }).ToArray());
                hospitalComboBox.Items.Clear();
                hospitalComboBox.Items.AddRange(hospitals.Select(hospital =>
                {
                    return hospital.Name;
                }).ToArray());
            }
        }
        private void RefreshDoctors()
        {
            var client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(url + "/doctor/all");
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch doctors.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                doctors = JsonConvert.DeserializeObject<ResponseEnvelope<List<DoctorResponse>>>(result).data;
                listViewDoctor.Items.Clear();
                listViewDoctor.Items.AddRange(doctors.Select(doctor =>
                {
                    return new ListViewItem(new[] { doctor.Username, doctor.Name });
                }).ToArray());
            }
        }
        private void RefreshAppointments()
        {
            if (hospitalComboBox.SelectedItem == null)
            {
                return;
            }
            long hospitalId = hospitals.Single(hospital => hospital.Name == hospitalComboBox.SelectedItem.ToString()).Id;
            if (hospitalId == 0)
            {
                return;
            }
            DateTime current = monthCalendar1.SelectionStart;
            DateTime From = new DateTime(current.Year, current.Month, current.Day, 0, 0, 0);
            DateTime To = new DateTime(current.Year, current.Month, current.Day, 23, 59, 59);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthConfiguration.AccessToken);
            string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            string path = "/appointment/hospital/" + hospitalId + "?From=" + From.ToString(dateFormat) + "&To=" + To.ToString(dateFormat);

            Task<HttpResponseMessage> response = client.GetAsync(url + path);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch appointments.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                appointments = JsonConvert.DeserializeObject<ResponseEnvelope<List<AppointmentResponse>>>(result).data;
                listViewAppointment.Items.Clear();
                listViewAppointment.Items.AddRange(appointments.Select(appointment =>
                {
                    return new ListViewItem(new[] { appointment.Room.Name, appointment.From.ToString(),
                            (appointment.To - appointment.From).TotalMinutes.ToString(),
                            appointment.Doctor.Name, appointment.PatientUsername, appointment.Description });
                }).ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSpeciality f = new AddSpeciality();
            f.FormClosed += new FormClosedEventHandler(Form_Closed);
            void Form_Closed(object sender, FormClosedEventArgs e)
            {
                RefreshSpecialities();
            }
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddHospital f = new AddHospital();
            f.FormClosed += new FormClosedEventHandler(Form_Closed);
            void Form_Closed(object sender, FormClosedEventArgs e)
            {
                RefreshHospitals();
            }
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddDoctor f = new AddDoctor();
            f.FormClosed += new FormClosedEventHandler(Form_Closed);
            void Form_Closed(object sender, FormClosedEventArgs e)
            {
                RefreshDoctors();
            }
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddAppointment f = new AddAppointment();
            f.FormClosed += new FormClosedEventHandler(Form_Closed);
            void Form_Closed(object sender, FormClosedEventArgs e)
            {
                RefreshAppointments();
            }
            f.Show();
        }

        private void hospitalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAppointments();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            RefreshAppointments();
        }

        private void buttonDeleteAppointment_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewAppointment.SelectedIndices;
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

        private void buttonEditHospital_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewHospital.SelectedIndices;
            if (indices.Count > 0)
            {
                long hospitalId = hospitals[indices[0]].Id;
                EditHospital f = new EditHospital(hospitalId);
                f.FormClosed += new FormClosedEventHandler(Form_Closed);
                void Form_Closed(object sender, FormClosedEventArgs e)
                {
                    RefreshHospitals();
                }
                f.Show();
            }
        }

        private void buttonAddRooms_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewHospital.SelectedIndices;
            if (indices.Count > 0)
            {
                long hospitalId = hospitals[indices[0]].Id;
                EditDeleteHospitalRooms f = new EditDeleteHospitalRooms(hospitalId);
                f.Show();
            }
        }

        private void buttonDoctorDetails_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewDoctor.SelectedIndices;
            if (indices.Count > 0)
            {
                string doctorUsername = doctors[indices[0]].Username;
                DoctorDetails f = new DoctorDetails(doctorUsername);
                f.Show();
            }
        }
    }
}
