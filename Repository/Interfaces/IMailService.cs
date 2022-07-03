using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IMailService
    {
        void SendMail();
        void SendVerificationMail(string mail, string id, string token);
        void SendNewPassword(string mail, string pwd);
        void ContactUser(string subject, string content, AppUser userToContact, AppUser initiator);
        void AccountDeletedNotify(string mail);
        void SendReport(string username,string mail, ReportInfo r);
    }
}
