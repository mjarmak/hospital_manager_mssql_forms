using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using hospital_manager_models.Models;
using hospital_manager_ui.Configuration;
using hospital_manager_ui.Util;
using Newtonsoft.Json.Linq;

namespace hospital_manager_ui.Forms
{
    public partial class Login : Form
    {
        public object JsonConvert { get; private set; }

        private readonly AuthUtil authUtil;

        public Login()
        {
            AddAppointment f = new AddAppointment();
            f.FormClosed += new FormClosedEventHandler(Form_Closed);
            void Form_Closed(object sender, FormClosedEventArgs e)
            {
            }
            f.Show();
            InitializeComponent();
            authUtil = new AuthUtil();
        }

        private void Click_Login(object sender, EventArgs e)
        {
            if (text_username.Text != null && text_password.Text != null) {

                var url = ApplicationConfiguration.oauthUrl;
                var client = new HttpClient();
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("scope", ApplicationConfiguration.scope),
                    new KeyValuePair<string, string>("username", text_username.Text),
                    new KeyValuePair<string, string>("password", text_password.Text),
                    new KeyValuePair<string, string>("client_id", ApplicationConfiguration.client_id),
                    new KeyValuePair<string, string>("client_secret", ApplicationConfiguration.client_secret)
                });

                Task<HttpResponseMessage> response = client.PostAsync(url, formContent);
                response.Wait();
                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Wrong username or password", "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                else
                {
                    string result = response.Result.Content.ReadAsStringAsync().Result;
                    JObject jObject = JObject.Parse(result);
                    string token = jObject.GetValue("access_token").ToString();
                    authUtil.DecodeToken(token);

                    if (AuthConfiguration.Role.Contains("ADMIN"))
                    {
                        AdminHomePage f = new AdminHomePage();
                        f.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void Click_Register(object sender, EventArgs e)
        {
            UserRegister f = new UserRegister();
            f.Show();
            this.Hide();
        }


        private void TextChanged_username(object sender, EventArgs e)
        {
        }

        private void TextChanged_password(object sender, EventArgs e)
        {
        }
    }
}
