using Repository.Interfaces;
using System.Net;
using System.Net.Mail;

namespace API.Services
{
    public class MailService : IMailService
    {
        public void SendMail()
        {
            using (MailMessage mailer = new MailMessage())
            {
                mailer.From = new MailAddress("merisahmatovic@gmail.com");
                mailer.To.Add(new MailAddress("merisahmatovic@gmail.com"));
                mailer.Subject = "Verifikacija naloga";
                mailer.Body = "" +
                    "<div>" +
                    "<h4 style='font-weight:400'>Hello World!</h3>" +
                    "</div>";
                mailer.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "fili123fili");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }

        public void SendVerificationMail(string id, string token)
        {
            using (MailMessage mailer = new MailMessage())
            {
                mailer.From = new MailAddress("merisahmatovic@gmail.com");
                mailer.To.Add(new MailAddress("merisahmatovic@gmail.com"));
                mailer.Subject = "Verifikacija naloga";
                mailer.Body = "" +
                    "<div>" +
                    "<h4 style='font-weight:400'>Poštovani,<br></br><br></br>Molimo Vas da kliknete na link ispod kako biste uspešno finalizirali registraciju na RealEstate sistem. Hvala unapred!<br></br><br></br>S' poštovanjem,<br></br>RealEstate tim.</h3><br></br><br></br>" +
                    "http://localhost:3000/prijava/?user=" + id + "&token=" + token +
                    "</div>";
                mailer.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "fili123fili");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }
    }
}
