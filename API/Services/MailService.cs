using API.DTOs;
using Data.Models;
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
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "ueavapjhpiyxwaeg");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }

        public void SendVerificationMail(string mail, string id, string token)
        {
            using (MailMessage mailer = new MailMessage())
            {
                mailer.From = new MailAddress("merisahmatovic@gmail.com");
                mailer.To.Add(new MailAddress(mail));
                mailer.Subject = "Verifikacija naloga";
                mailer.Body = "" +
                    "<div>" +
                    "<h4 style='font-weight:400'>Poštovani,<br></br><br></br>Molimo Vas da kliknete na link ispod kako biste uspešno finalizirali registraciju na RealEstate sistem. Hvala unapred!<br></br><br></br>S' poštovanjem,<br></br>RealEstate tim.</h3><br></br><br></br>" +
                    "https://nimble-jelly-be1f14.netlify.app/verifikacija/?user=" + id + "&token=" + token +
                    "</div>";
                mailer.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "ueavapjhpiyxwaeg");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }

        public void SendNewPassword(string mail, string pwd)
        {
            using (MailMessage mailer = new MailMessage())
            {
                mailer.From = new MailAddress("merisahmatovic@gmail.com");
                mailer.To.Add(new MailAddress(mail));
                mailer.Subject = "Obnova lozinke";
                mailer.Body = "" +
                    "<div>" +
                    "<h4 style='font-weight:400'>Poštovani,<br></br><br></br>RealEstate sistem je za Vas generisao novi password:<br></br><br></br>" + pwd + "<br></br><br></br>S' poštovanjem,<br></br>RealEstate tim.</h3><br></br><br></br>" +
                    "</div>";
                mailer.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "ueavapjhpiyxwaeg");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }

        public void ContactUser(string subject, string content, AppUser userToContact, AppUser initiator)
        {
            var email = "mejlkorisnika";
            var brojT = "12421352";
            using (MailMessage mailer = new MailMessage())
            {
                mailer.From = new MailAddress("merisahmatovic@gmail.com");
                //mailer.To.Add(new MailAddress(userToContact.Email));
                mailer.To.Add(userToContact.Email);
                mailer.Subject = "Uspostavljanje kontakta";
                mailer.Body = "" +
                    "<div>" +
                        "<h4 style='font-weight:400'>" +
                            "Poštovani,<br></br><br></br>" +
                            "korisnik"+ initiator.FirstName + " " + initiator.LastName + " želi uspostaviti kontakt sa Vama.<br></br> Šalje inicijalnu poruku sa sadržajem:<br></br><br></br>" +
                            "<span style='font-weight:600'>Tema: '" + subject + "'<br></br>" +
                            "Sadržaj: '" + content + "'</span><br></br>" +
                            "<br></br>Ukoliko je potrebna dalja konverzacija sa korisnikom, molimo Vas da kontakt uspostavite preko dostavljenih informacija:" +
                            "<br></br>E-mail korisnika:" + initiator.Email +
                            "<br></br><br></br>S' poštovanjem,<br></br>RealEstate tim.</h3><br></br><br></br>" +
                    "</div>";
                mailer.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "ueavapjhpiyxwaeg");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }

        public void AccountDeletedNotify(string mail)
        {
            using (MailMessage mailer = new MailMessage())
            {
                mailer.From = new MailAddress("merisahmatovic@gmail.com");
                mailer.To.Add(new MailAddress(mail));
                mailer.Subject = "Deaktivacija naloga";
                mailer.Body = "" +
                    "<div>" +
                        "<h4 style='font-weight:400'>" +
                            "Poštovani,<br></br><br></br>zbog zloupotrebe RealEstate sistema i njegovog sadržaja, adminstrativni tim je odlučio da deaktivira Vaš profil. Hvala na razumevanju..." +
                        "</h4>" +
                    "</div>";
                mailer.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "ueavapjhpiyxwaeg");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }

        public void SendReport(string username, string mail, ReportInfo r)
        {
            using (MailMessage mailer = new MailMessage())
            {
                mailer.From = new MailAddress("merisahmatovic@gmail.com");
                mailer.To.Add(new MailAddress("merisahmatovic@gmail.com"));
                mailer.Subject = "Prijava korisnika";
                mailer.Body = "" +
                    "<div>" +
                        "<h4 style='font-weight:400'>" +
                            "Sistem je registrovao prijavu korisnika @" + username + ".<br></br>" +
                            "E-mail adresa prijavljenog korisnika: " + mail + "." +
                        "</h4>" +
                        "<h4 style='font-weight:400'>" +
                            "Navedeni razlozi prijave:<br></br>" +
                            (r.InvalidContent ? "- Neprikladan sadrzaj<br></br>" : "" ) +
                            (r.Spam ? "- Spamovanje korisnika<br></br>" : "" ) +
                            (r.InvalidInfo ? "- Objava nevalidnih informacija<br></br>" : "" ) +
                            (r.Identity ? "- Krađa identiteta<br></br>" : "" ) +
                        "</h4>" +
                        "<h4 style='font-weight:400'>" +
                            "Molimo administrativni tim da izvrši kontrolu profila." +
                        "</h4>" +
                    "</div>";
                mailer.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("merisahmatovic@gmail.com", "ueavapjhpiyxwaeg");

                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(mailer);
                    smtp.Send(mailer);
                }
            }
        }
    }
}
