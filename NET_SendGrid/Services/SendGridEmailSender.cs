using Microsoft.Extensions.Options;
using NET_SendGrid.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NET_SendGrid.Services
{
    public class SendGridEmailSender : ICustomEmailSender
    {
        public SendGridEmailSenderOptions Options { get; set; }

        public SendGridEmailSender(IOptions<SendGridEmailSenderOptions> options)
        {
            this.Options = options.Value;
        }
        public async Task<Response> OnResetPassword(ResetPasswordEventArgs eventArgs)
        {
            // Template
            EmailMessage message = new(eventArgs.To)
            {
                SendGridTemplateID = Options?.Template?.ResetPassword,
                SendGridTemplateData = new ResetPasswordSG { Email = eventArgs.Email, CallbackUri = eventArgs.CallbackUri }
            };
            return await SendEmailAsync(message);
        }

        public async Task<Response> SendEmailAsync(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);

            return await Send(emailMessage);
        }

        private SendGridMessage CreateEmailMessage(EmailMessage message)
        {

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName)
            };

            if (message.SendGridTemplateID != null)
            {
                msg.SetTemplateId(message.SendGridTemplateID);
                msg.SetTemplateData(message.SendGridTemplateData);

            }
            else
            {
                msg.Subject = message.Subject;
                msg.PlainTextContent = message.Content;
                msg.HtmlContent = message.Content;
            }

            List<EmailAddress> emails = new();
            message?.To?.ForEach(x => emails.Add(new EmailAddress { Name = x.Name, Email = x.Address }));

            msg.AddTos(emails);

            return msg;
        }

        private async Task<Response> Send(SendGridMessage sendGridMessage)
        {
            SendGridClient client = new(Options.ApiKey);
            sendGridMessage.SetClickTracking(false, false);
            sendGridMessage.SetOpenTracking(false);
            sendGridMessage.SetGoogleAnalytics(false);
            sendGridMessage.SetSubscriptionTracking(false);

            Response result = await client.SendEmailAsync(sendGridMessage);
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine(result.Body.ToString());
            }
            return result;
        }
    }
}
