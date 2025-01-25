using InvoiceArchive.Application.Interfaces;
using InvoiceArchive.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace InvoiceArchive.Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings ?? throw new ArgumentNullException(nameof(emailSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task SendInvoiceInformationEmail(string invoiceId)
        {
            string subject = $"Invoice  for {invoiceId}";
            string text = $"Invoice with Id: '{invoiceId}' has been stored successfully.";

            try
            {
                await sendEmail(_emailSettings.Value.ReceiverEmail ?? "", subject, text);
                _logger.LogInformation($"Email sent for the Invoice: '{invoiceId}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send Email for the Invoice: '{invoiceId}'. ErrorMessage: {ex.ToString()}");
            }
        }


        private async Task sendEmail(string receiverEmail, string subject, string text)
        {
            MailMessage mail = new()
            {
                From = new MailAddress(_emailSettings.Value.SenderEmail ?? ""),
                Subject = subject,
                Body = text,
                IsBodyHtml = true
            };

            mail.To.Add(receiverEmail);

            SmtpClient smtp = new()
            {
                Credentials = new NetworkCredential(_emailSettings.Value.SenderEmail ?? "", _emailSettings.Value.SenderPassword ?? ""),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            smtp.Send(mail);
        }
    }
}
