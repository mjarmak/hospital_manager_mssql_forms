using hospital_manager_models.Models;
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
    public partial class EditDeleteHospitalRooms : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        private List<SpecialityResponse> specialities;
        private List<RoomRequest> roomRequests = new List<RoomRequest>();
        long hospitalId;
        public EditDeleteHospitalRooms(long hospitalId)
        {
            this.hospitalId = hospitalId;
            InitializeComponent();
            RefreshSpecialities();
            RefreshRooms();
        }
        private void RefreshRooms()
        {
            roomRequests = GetRooms(hospitalId).Select(room => new RoomRequest(room.Name, room.Specialities.Select(spec => spec.Id).ToList())).ToList();
            RefreshRoomList();
        }
        private List<RoomResponse> GetRooms(long hospitalId)
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/room/hospital/" + hospitalId);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to fetch hospital with ID " + hospitalId,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ResponseEnvelope<List<RoomResponse>>>(result).data;

            }
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
                checkedListBoxRoomSpecialities.Items.Clear();
                checkedListBoxRoomSpecialities.Items.AddRange(specialitiesResponse.Select(speciality =>
                {
                    return speciality.Name;
                }).ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxRoomName.Text) || checkedListBoxRoomSpecialities.CheckedItems.Count < 1)
            {
                MessageBox.Show("Please choose a name and atleast 1 speciality", "No name of specialities selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                List<long> specialityIds = specialities.Where(speciality => checkedListBoxRoomSpecialities.CheckedItems.Contains(speciality.Name))?.Select(speciality => speciality.Id).ToList();

                if (roomRequests.Find(room => room.Name == textBoxRoomName.Text) != null)
                {
                    MessageBox.Show("Choose a different name", "Room name already taken for this hospital",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                roomRequests.Add(new RoomRequest(textBoxRoomName.Text, specialityIds));
                RefreshRoomList();
            }
        }
        private void RefreshRoomList()
        {
            listViewRooms.Items.Clear();
            listViewRooms.Items.AddRange(roomRequests.Select(room =>
            {
                return new ListViewItem(new[] { room.Name, String.Join(", ", specialities.Where(speciality => room.SpecialityIds.Contains(speciality.Id))?.Select(speciality => speciality.Name).ToList()) });
            }).ToArray());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(roomRequests);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(url + "/hospital/" + hospitalId + "/room", httpContent);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result, "Failed to add rooms",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewRooms.SelectedIndices;
            if (indices.Count > 0)
            {
                long roomId = roomRequests[indices[0]].Id;
                var client = new HttpClient();
                Task<HttpResponseMessage> response = client.DeleteAsync(url + "/hospital/room/" + roomId);
                response.Wait();
                roomRequests.Remove(roomRequests[indices[0]]);
                RefreshRoomList();
            }
        }
    }
}
