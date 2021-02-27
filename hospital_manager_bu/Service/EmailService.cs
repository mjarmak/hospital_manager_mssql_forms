using System.Net;
using System.Net.Mail;

namespace hospital_manager_bl.Service
{
    public class EmailService
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential("hospitalmanagercontact@gmail.com", "houhHOH421HOU!"),
                EnableSsl = true,
            };
            smtpClient.Send("hospitalmanagercontact@gmail.com", recipient, subject, body);
        }
    }
}
