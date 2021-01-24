using hospital_manager_models.Models;
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
    public partial class EditAppointment : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private int duration;
        private long appointmentId;
        private AppointmentRequest appointmentRequest;
        public EditAppointment(long appointmentId)
        {
            this.appointmentId = appointmentId;
            InitializeComponent();
            AppointmentResponse appointmentResponse = GetAppointment(appointmentId);
            appointmentRequest = new AppointmentRequest();
            appointmentRequest.Id = appointmentResponse.Id;
            appointmentRequest.Description = appointmentResponse.Description;
            appointmentRequest.From = appointmentResponse.From;
            appointmentRequest.RoomId = appointmentResponse.RoomId;
            appointmentRequest.DoctorUsername = appointmentResponse.DoctorUsername;
            appointmentRequest.PatientUsername = appointmentResponse.PatientUsername;

            textBoxDescription.Text = appointmentRequest.Description;
            textBoxDoctor.Text = appointmentRequest.Description;
            textBoxPatientUsername.Text = appointmentRequest.Description;
            textBoxRoom.Text = appointmentRequest.RoomId.ToString();

            textBoxDuration.Text = (appointmentRequest.To - appointmentRequest.From).TotalMinutes.ToString();
            duration = (int)(appointmentRequest.To - appointmentRequest.From).TotalMinutes;
            dateTimePickerAt.Value = appointmentRequest.From;

        }
        private AppointmentResponse GetAppointment(long appointmentId)
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/appointment/" + appointmentId);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch appointment with ID " + appointmentId,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ResponseEnvelope<AppointmentResponse>>(result).data;

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            appointmentRequest.PatientUsername = textBoxPatientUsername.Text;
            appointmentRequest.Description = textBoxDescription.Text;

            if (String.IsNullOrWhiteSpace(appointmentRequest.PatientUsername))
            {
                MessageBox.Show("Patient missing", "Please add the patient username",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrWhiteSpace(appointmentRequest.Description))
            {
                MessageBox.Show("Description missing", "Please give a description for the appointment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(appointmentRequest);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(url + "/appointment", httpContent);
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

        private void dateTimePickerAt_ValueChanged(object sender, EventArgs e)
        {
            if (duration != 0)
            {
                appointmentRequest.From = dateTimePickerAt.Value;
                appointmentRequest.To = dateTimePickerAt.Value.AddMinutes(duration);
                ProcessAppointmentAvailability();
            }
        }
        private void ProcessAppointmentAvailability()
        {
            bool taken = GetAppointmentTaken(appointmentRequest.RoomId, appointmentRequest.From, appointmentRequest.To);
            if (taken)
            {
                buttonSave.Enabled = false;
                labelTimeSlotBooked.Visible = true;
            }
            else
            {
                buttonSave.Enabled = true;
                labelTimeSlotBooked.Visible = false;
            }
        }
        private bool GetAppointmentTaken(long roomId, DateTime from, DateTime to)
        {
            var client = new HttpClient();
            string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            string path = "/appointment/room/" + roomId + "/taken?from=" + from.ToString(dateFormat) + "&to=" + to.ToString(dateFormat);
            Task<HttpResponseMessage> response = client.GetAsync(url + path);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch appointment suggestions",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ResponseEnvelope<bool>>(result).data;

            }
        }
    }
}
