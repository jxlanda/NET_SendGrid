using MimeKit;

namespace NET_SendGrid.Models
{
    public class EmailMessage
    {
        public List<MailboxAddress>? To { get; set; }
        public string? SendGridTemplateID { get; set; }
        public object? SendGridTemplateData { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }

        public IFormFileCollection? Attachments { get; set; }


        public EmailMessage(IEnumerable<string>? to)
        {
            To = new List<MailboxAddress>();

            if (to != null)
            {
                To.AddRange(to.Select(x => new MailboxAddress(x, x)));
            }
        }

        public EmailMessage(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x, x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }

    public class EmailMessageDTO
    {
        public List<string>? To { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
    }
}
