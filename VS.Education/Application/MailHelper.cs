using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using MoreAll;
using System.IO;

public class MailHelper
{
    public void SendMail(string toEmailAddress, string subject, string content)
    {
        var fromEmailAddress = Email.email();
        var fromEmailPassword = Email.password();
        var smtpHost = Email.host();
        var smtpPort = Convert.ToInt32(Email.port()).ToString();

        // bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

        string body = content;
        MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, subject), new MailAddress(toEmailAddress));
        message.Subject = subject;
        message.IsBodyHtml = true;
        message.Body = body;

        var client = new SmtpClient();
        client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
        client.Host = smtpHost;
        client.EnableSsl = true;
        client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
        client.Send(message);
    }
}
