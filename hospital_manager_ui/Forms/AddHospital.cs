using hospital_manager_models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Net.Http.Headers;
using hospital_manager_ui.Configuration;

namespace hospital_manager_ui.Forms
{
    public partial class AddHospital : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private List<SpecialityResponse> specialities;
        private List<RoomRequest> roomRequests = new List<RoomRequest>();
        public AddHospital()
        {
            InitializeComponent();
            RefreshSpecialities();
            hourFromComboBox1.SelectedIndex = 9;
            hourFromComboBox2.SelectedIndex = 9;
            hourFromComboBox3.SelectedIndex = 9;
            hourFromComboBox4.SelectedIndex = 9;
            hourFromComboBox5.SelectedIndex = 9;
            hourFromComboBox6.SelectedIndex = 9;
            hourFromComboBox7.SelectedIndex = 9;
            hourToComboBox1.SelectedIndex = 19;
            hourToComboBox2.SelectedIndex = 19;
            hourToComboBox3.SelectedIndex = 19;
            hourToComboBox4.SelectedIndex = 19;
            hourToComboBox5.SelectedIndex = 19;
            hourToComboBox6.SelectedIndex = 19;
            hourToComboBox7.SelectedIndex = 19;
            minuteFromComboBox1.SelectedIndex = 0;
            minuteFromComboBox2.SelectedIndex = 0;
            minuteFromComboBox3.SelectedIndex = 0;
            minuteFromComboBox4.SelectedIndex = 0;
            minuteFromComboBox5.SelectedIndex = 0;
            minuteFromComboBox6.SelectedIndex = 0;
            minuteFromComboBox7.SelectedIndex = 0;
            minuteToComboBox1.SelectedIndex = 0;
            minuteToComboBox2.SelectedIndex = 0;
            minuteToComboBox3.SelectedIndex = 0;
            minuteToComboBox4.SelectedIndex = 0;
            minuteToComboBox5.SelectedIndex = 0;
            minuteToComboBox6.SelectedIndex = 0;
            minuteToComboBox7.SelectedIndex = 0;
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
                specialititesCheckList.Items.Clear();
                specialititesCheckList.Items.AddRange(specialitiesResponse.Select(speciality =>
                {
                    return speciality.Name;
                }).ToArray());
            }
        }

        private void AddRoom_Click(object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(roomTextBox.Text) || specialititesCheckList.CheckedItems.Count < 1)
            {
                MessageBox.Show("Please choose a name and atleast 1 speciality", "No name of specialities selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                List<long> specialityIds = specialities.Where(speciality => specialititesCheckList.CheckedItems.Contains(speciality.Name))?.Select(speciality => speciality.Id).ToList();

                if (roomRequests.Find(room => room.Name == roomTextBox.Text) != null)
                {
                    MessageBox.Show("Choose a different name", "Room name already taken for this hospital",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                roomRequests.Add(new RoomRequest(roomTextBox.Text, specialityIds));
                RefreshRoomList();
            }
        }
        private void RefreshRoomList()
        {
            roomsListView.Items.Clear();
            roomsListView.Items.AddRange(roomRequests.Select(room =>
            {
                return new ListViewItem(new[] { room.Name, String.Join(", ", specialities.Where(speciality => room.SpecialityIds.Contains(speciality.Id))?.Select(speciality => speciality.Name).ToList()) });
            }).ToArray());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthConfiguration.AccessToken);

            HospitalRequest hospitalRequest = new HospitalRequest();
            hospitalRequest.Rooms = roomRequests;
            hospitalRequest.Address = new AddressRequest();

            hospitalRequest.Name = nameTextBox.Text;
            hospitalRequest.Address.Street = streetTextBox.Text;
            hospitalRequest.Address.City = cityTextBox.Text;
            hospitalRequest.Address.Country = countryTextBox.Text;
            hospitalRequest.Address.PostalCode = postcodeTextBox.Text;
            hospitalRequest.Address.BoxNumber = boxTextBox.Text;

            hospitalRequest.OpeningHours = new List<OpeningHoursRequest>
            {
                new OpeningHoursRequest(0, "MONDAY", ToInt(hourFromComboBox1), ToInt(hourToComboBox1), ToInt(minuteFromComboBox1), ToInt(minuteToComboBox1), closedDay1.Checked),
                new OpeningHoursRequest(0, "TUESDAY", ToInt(hourFromComboBox2), ToInt(hourToComboBox2), ToInt(minuteFromComboBox2), ToInt(minuteToComboBox2), closedDay2.Checked),
                new OpeningHoursRequest(0, "WEDNESDAY", ToInt(hourFromComboBox3), ToInt(hourToComboBox3), ToInt(minuteFromComboBox3), ToInt(minuteToComboBox3), closedDay3.Checked),
                new OpeningHoursRequest(0, "THURSDAY", ToInt(hourFromComboBox4), ToInt(hourToComboBox4), ToInt(minuteFromComboBox4), ToInt(minuteToComboBox4), closedDay4.Checked),
                new OpeningHoursRequest(0, "FRIDAY", ToInt(hourFromComboBox5), ToInt(hourToComboBox5), ToInt(minuteFromComboBox5), ToInt(minuteToComboBox5), closedDay5.Checked),
                new OpeningHoursRequest(0, "SATURDAY", ToInt(hourFromComboBox6), ToInt(hourToComboBox6), ToInt(minuteFromComboBox6), ToInt(minuteToComboBox6), closedDay6.Checked),
                new OpeningHoursRequest(0, "SUNDAY", ToInt(hourFromComboBox7), ToInt(hourToComboBox7), ToInt(minuteFromComboBox7), ToInt(minuteToComboBox7), closedDay7.Checked)
            };

            string json = JsonConvert.SerializeObject(hospitalRequest);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync(url + "/hospital", httpContent);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to add hospital",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }
        private int ToInt(ComboBox obj)
        {
            return Int32.Parse(obj.SelectedItem.ToString());
        }
    }
}
