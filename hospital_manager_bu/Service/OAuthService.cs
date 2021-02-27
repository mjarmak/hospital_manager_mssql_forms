using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace hospital_manager_bl.Service
{
    public class OAuthService
    {
        private readonly string url = "https://localhost:44321";

        public bool UserExists(string username)
        {
            var client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(url + "/user/exists?username=" + username);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetUserEmail(string username)
        {
            var client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(url + "/user/email?username=" + username);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            else
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                JObject jObject = JObject.Parse(result);
                return jObject.GetValue("email").ToString();
            }
        }

        public string RegisterUser(UserAccountRequest userAccountRequest, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonConvert.SerializeObject(userAccountRequest);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync(url + "/register/doctor", httpContent);
            response.Wait();
            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidUserRequest(response.Result.ToString());
            }
            string result = response.Result.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(result);
            return jObject.GetValue("username").ToString();
        }
    }
}
