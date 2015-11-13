using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Take.Gamification.Models
{
    public class SendEmail
    {
        public static void SendMerito(string destination, Dictionary<string, string> parameters)
        {
            using (var client = new SmtpClient("smtp.mandrillapp.com", 587))
            {
                var cred = new NetworkCredential("brenoqueiroz@gmail.com", "eXpCM5Y4ZLoJt_KgrrJoXA");
                client.Credentials = cred;
                var from = new MailAddress("brenoqueiroz@gmail.com", "Take.Valoriza", System.Text.Encoding.UTF8);
                var to = new MailAddress(destination);
                using (var message = new MailMessage(from, to))
                {
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.Subject = "Take.Valoriza Mérito recebido";
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;

                    string html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/merito.html"));

                    foreach (var item in parameters)
                    {
                        html = html.Replace(item.Key, item.Value);
                    }

                    message.Body = html;
                    client.Send(message.From.ToString(), message.To.ToString(), message.Subject, message.Body);
                }
            }
        }
    }
}