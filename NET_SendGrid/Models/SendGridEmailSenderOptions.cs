namespace NET_SendGrid.Models
{
    public class SendGridEmailSenderOptions
    {
        public string? ApiKey { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderName { get; set; }
        public SendGridEmailTemplateOptions? Template { get; set; }
    }

    public class SendGridEmailTemplateOptions
    {
        public string? ResetPassword { get; set; }
        public string? RequisitionAuthRequest { get; set; }
        public string? AuthRequisitionCancelled { get; set; }
        public string? AuthRequisitionAuthorized { get; set; }
        public string? AuthPaymentRequest { get; set; }
        public string? PaymentRequestCancelled { get; set; }
        public string? PaymentRequestAuthorized { get; set; }
    }
}
