using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace KEShop_Api_N_Tier_Art.PL.utils
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("alnabelsifull@gmail.com", "ffpa cusx ghcl dgds")
            };

            return client.SendMailAsync(
                new MailMessage(from: "alnabelsifull@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                { IsBodyHtml = true }
                );
        }
    }
}
