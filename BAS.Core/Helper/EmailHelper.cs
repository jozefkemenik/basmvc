using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Core.Helper
{
    public class EmailHelper
    {
        public static bool SendEmail(string subject, string message, out string errorMessage)
        {
            errorMessage = string.Empty;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add("kemenikova@gmail.com");
            mail.From = new MailAddress("admin@admin.com");
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;           
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("jozef@kemenik.eu", "P4kemenik");
            client.Port = 587;
            client.Host = "mail.kemenik.eu";
            client.EnableSsl = false;
            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
              
            }
            return false;
        }
    }
}
