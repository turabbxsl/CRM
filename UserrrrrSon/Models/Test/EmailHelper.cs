using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.Test
{
    public class EmailHelper
    {

        public bool SendEmail(string userEmail, string confirmationLink)
        {

            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            Msg.From = new MailAddress("turabbl@gmail.com");
            // Recipient e-mail address.
            Msg.To.Add(userEmail);
            Msg.Subject = "Confirm Email";
            Msg.Body = "Click Link" + confirmationLink;
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("turabbl@gmail.com", "abaqiyfxnjyvtjlj");
            smtp.EnableSsl = true;
            smtp.Send(Msg);

            return true;
        }

        public bool SendPersonEmail(string userEmail)
        {

            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            Msg.From = new MailAddress("turabbl@gmail.com");
            // Recipient e-mail address.
            Msg.To.Add(userEmail);
            Msg.Subject = "Decord Tech";
            Msg.Body = "Hello New Person";
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("turabbl@gmail.com", "abaqiyfxnjyvtjlj");
            smtp.EnableSsl = true;
            smtp.Send(Msg);

            return true;
        }

    }
}
