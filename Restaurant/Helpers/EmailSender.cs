using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace Restaurant.Helpers
{
    public class EmailSender(string username, string password)
    {
        public async Task SendEmailAsync(string emailTO, string subject, string body)
        {

            // Config mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(username);
            mail.To.Add(emailTO);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            // Config smtp to send mail => Can be change into a function
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendEmailWithQRCodeAsync(string emailTO, string subject, string body, byte[] qrCodeImage)
        {

            // Config mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(username);
            mail.To.Add(emailTO);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            // Config smtp to send mail => Can be change into a function
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                using (var ms = new MemoryStream(qrCodeImage))
                {
                    ms.Position = 0;

                    var attachment = new Attachment(ms, "order-tracking.png", MediaTypeNames.Image.Png);
                    mail.Attachments.Add(attachment);

                    await smtpClient.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
