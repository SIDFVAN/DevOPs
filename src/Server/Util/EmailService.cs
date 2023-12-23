 using Blanche.Server.Pages.Templates; 
using Blanche.Server.Services.Util;
using Blanche.Server.Services.Util.Mail;
using Blanche.Shared.Invoices;
using Blanche.Shared.Reservations;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text; 

namespace Blanche.Server.Util
{
    public class EmailService : IEmailService
    {
        private readonly IHtmlGenerationService _htmlGenerationService;
        private readonly IConfigurationSection _smtpSettings;
        private readonly IPdfGeneratorService _pdfGenerationService;

        public EmailService(IHtmlGenerationService htmlGenerationService,
            IPdfGeneratorService pdfGenerationService,
            IConfiguration configuration)
        {
            _htmlGenerationService = htmlGenerationService;
            _pdfGenerationService = pdfGenerationService;
            _smtpSettings = configuration.GetSection("SmtpSettings");
        }

        public async Task SendReservationConfirmationMailAsync(ReservationDto reservationDto)
        {
             
            string htmlBody = await _htmlGenerationService.Generate(nameof(ConfirmationMailTemplate), reservationDto);
            SendEmailAsync(reservationDto.Customer.Email.Value, "Bevestiging reservatie", htmlBody, null);

            await Task.CompletedTask;
        }

        public async Task SendInvoiceMailWithPdf(InvoiceDto invoice, byte[] pdf, string htmlBody, string pdfFileName)
        {
            var attachmentPart = new MimePart("application", "pdf")
            {
                Content = new MimeContent(new MemoryStream(pdf), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = pdfFileName
            };
             

            if (invoice.IsQuote) {
                SendEmailAsync(invoice.Reservation.Customer.Email.Value, $"Offerte - {invoice.InvoiceNumber}", htmlBody, attachmentPart);
            }
            else
                SendEmailAsync(invoice.Reservation.Customer.Email.Value, $"Factuur - {invoice.InvoiceNumber}", htmlBody, attachmentPart);


            await Task.CompletedTask;
        }

        public async Task SendReservationPrePayedMailAsync(InvoiceDto invoice)
        {
            string htmlBody = await _htmlGenerationService.Generate(nameof(ReservationPayedMailTemplate), invoice.Reservation);
            SendEmailAsync(invoice.Reservation.Customer.Email.Value, $"Betaling voorschot bevestigd", htmlBody, null);
              
            await Task.CompletedTask;
        }

        private async Task SendEmailAsync(string to, string subject, string body, MimePart attachment)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_smtpSettings.GetValue<string>("SenderEmail")));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                var multipart = new Multipart("mixed");
                multipart.Add(new TextPart(TextFormat.Html) { Text = body });

                if (attachment != null)
                {
                    multipart.Add(attachment);
                }

                email.Body = multipart;
                 

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.GetValue<string>("Server"),
                        _smtpSettings.GetValue<int>("Port"), false);  

                    await client.AuthenticateAsync(_smtpSettings.GetValue<string>("Username"),
                        _smtpSettings.GetValue<string>("Password"));

                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
             
        }
         
    }
}
