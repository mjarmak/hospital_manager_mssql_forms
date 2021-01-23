using hospital_manager_models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_manager_ui.Forms
{
    public partial class EditHospital : Form
    {
        private protected string url = ApplicationConfiguration.hospitalManagerApiUrl;
        long hospitalId;
        public EditHospital(long hospitalId)
        {
            InitializeComponent();
            HospitalResponse hospitalResponse = GetHospital(hospitalId);
            if (hospitalResponse == null)
            {
                return;
            }
            this.hospitalId = hospitalId;
            textBoxEditHospitalName.Text = hospitalResponse.Name;
            textBoxEditHospitalCity.Text = hospitalResponse.Address.City;
            textBoxEditHospitalCountry.Text = hospitalResponse.Address.Country;
            textBoxEditHospitalStreet.Text = hospitalResponse.Address.Street;
            textBoxEditHospitalPostCode.Text = hospitalResponse.Address.PostalCode;
            textBoxEditHospitalBox.Text = hospitalResponse.Address.BoxNumber;

            comboBoxEditHospitalFrom11.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "MONDAY").HourFrom.ToString();
            comboBoxEditHospitalFrom12.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "MONDAY").MinuteFrom.ToString();
            comboBoxEditHospitalTo11.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "MONDAY").HourTo.ToString();
            comboBoxEditHospitalTo12.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "MONDAY").MinuteTo.ToString();
            comboBoxEditHospitalFrom21.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "TUESDAY").HourFrom.ToString();
            comboBoxEditHospitalFrom22.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "TUESDAY").MinuteFrom.ToString();
            comboBoxEditHospitalTo21.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "TUESDAY").HourTo.ToString();
            comboBoxEditHospitalTo22.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "TUESDAY").MinuteTo.ToString();
            comboBoxEditHospitalFrom31.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "WEDNESDAY").HourFrom.ToString();
            comboBoxEditHospitalFrom32.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "WEDNESDAY").MinuteFrom.ToString();
            comboBoxEditHospitalTo31.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "WEDNESDAY").HourTo.ToString();
            comboBoxEditHospitalTo32.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "WEDNESDAY").MinuteTo.ToString();
            comboBoxEditHospitalFrom41.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "THURSDAY").HourFrom.ToString();
            comboBoxEditHospitalFrom42.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "THURSDAY").MinuteFrom.ToString();
            comboBoxEditHospitalTo41.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "THURSDAY").HourTo.ToString();
            comboBoxEditHospitalTo42.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "THURSDAY").MinuteTo.ToString();
            comboBoxEditHospitalFrom51.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "FRIDAY").HourFrom.ToString();
            comboBoxEditHospitalFrom52.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "FRIDAY").MinuteFrom.ToString();
            comboBoxEditHospitalTo51.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "FRIDAY").HourTo.ToString();
            comboBoxEditHospitalTo52.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "FRIDAY").MinuteTo.ToString();
            comboBoxEditHospitalFrom61.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SATURDAY").HourFrom.ToString();
            comboBoxEditHospitalFrom62.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SATURDAY").MinuteFrom.ToString();
            comboBoxEditHospitalTo61.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SATURDAY").HourTo.ToString();
            comboBoxEditHospitalTo62.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SATURDAY").MinuteTo.ToString();
            comboBoxEditHospitalFrom71.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SUNDAY").HourFrom.ToString();
            comboBoxEditHospitalFrom72.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SUNDAY").MinuteFrom.ToString();
            comboBoxEditHospitalTo71.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SUNDAY").HourTo.ToString();
            comboBoxEditHospitalTo72.Text = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SUNDAY").MinuteTo.ToString();

            checkBoxEditHospitalClosed1.Checked = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "MONDAY").Closed;
            checkBoxEditHospitalClosed2.Checked = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "TUESDAY").Closed;
            checkBoxEditHospitalClosed3.Checked = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "WEDNESDAY").Closed;
            checkBoxEditHospitalClosed4.Checked = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "THURSDAY").Closed;
            checkBoxEditHospitalClosed5.Checked = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "FRIDAY").Closed;
            checkBoxEditHospitalClosed6.Checked = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SATURDAY").Closed;
            checkBoxEditHospitalClosed7.Checked = hospitalResponse.OpeningHours.Find(openingHour => openingHour.Day == "SUNDAY").Closed;
        }

        private HospitalResponse GetHospital(long hospitalId)
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url + "/hospital/" + hospitalId);
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
                return JsonConvert.DeserializeObject<ResponseEnvelope<HospitalResponse>>(result).data;

            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();

            HospitalRequest hospitalRequest = new HospitalRequest();
            hospitalRequest.Id = hospitalId;
            hospitalRequest.Address = new AddressRequest();
            hospitalRequest.Name = textBoxEditHospitalName.Text;
            hospitalRequest.Address.Street = textBoxEditHospitalStreet.Text;
            hospitalRequest.Address.City = textBoxEditHospitalCity.Text;
            hospitalRequest.Address.Country = textBoxEditHospitalCountry.Text;
            hospitalRequest.Address.PostalCode = textBoxEditHospitalPostCode.Text;
            hospitalRequest.Address.BoxNumber = textBoxEditHospitalBox.Text;

            hospitalRequest.OpeningHours = new List<OpeningHoursRequest>
            {
                new OpeningHoursRequest("MONDAY", ToInt(comboBoxEditHospitalFrom11), ToInt(comboBoxEditHospitalTo11), ToInt(comboBoxEditHospitalFrom12), ToInt(comboBoxEditHospitalTo12), checkBoxEditHospitalClosed1.Checked),
                new OpeningHoursRequest("TUESDAY", ToInt(comboBoxEditHospitalFrom21), ToInt(comboBoxEditHospitalTo21), ToInt(comboBoxEditHospitalFrom22), ToInt(comboBoxEditHospitalTo22), checkBoxEditHospitalClosed2.Checked),
                new OpeningHoursRequest("WEDNESDAY", ToInt(comboBoxEditHospitalFrom31), ToInt(comboBoxEditHospitalTo31), ToInt(comboBoxEditHospitalFrom32), ToInt(comboBoxEditHospitalTo32), checkBoxEditHospitalClosed3.Checked),
                new OpeningHoursRequest("THURSDAY", ToInt(comboBoxEditHospitalFrom41), ToInt(comboBoxEditHospitalTo41), ToInt(comboBoxEditHospitalFrom42), ToInt(comboBoxEditHospitalTo42), checkBoxEditHospitalClosed4.Checked),
                new OpeningHoursRequest("FRIDAY", ToInt(comboBoxEditHospitalFrom51), ToInt(comboBoxEditHospitalTo51), ToInt(comboBoxEditHospitalFrom52), ToInt(comboBoxEditHospitalTo52), checkBoxEditHospitalClosed5.Checked),
                new OpeningHoursRequest("SATURDAY", ToInt(comboBoxEditHospitalFrom61), ToInt(comboBoxEditHospitalTo61), ToInt(comboBoxEditHospitalFrom62), ToInt(comboBoxEditHospitalTo62), checkBoxEditHospitalClosed6.Checked),
                new OpeningHoursRequest("SUNDAY", ToInt(comboBoxEditHospitalFrom71), ToInt(comboBoxEditHospitalTo71), ToInt(comboBoxEditHospitalFrom72), ToInt(comboBoxEditHospitalTo72), checkBoxEditHospitalClosed7.Checked)
            };

            string json = JsonConvert.SerializeObject(hospitalRequest);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(url + "/hospital", httpContent);
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
