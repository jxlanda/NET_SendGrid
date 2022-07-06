using Microsoft.AspNetCore.Mvc;
using NET_SendGrid.Models;
using NET_SendGrid.Services;

namespace NET_SendGrid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendGridController : ControllerBase
    {
        private readonly ILogger<SendGridController> _logger;
        private readonly ICustomEmailSender _sendGrid;
        public SendGridController(ILogger<SendGridController> logger, ICustomEmailSender sendGrid)
        {
            _logger = logger;
            _sendGrid = sendGrid;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EmailMessageDTO email)
        {
            EmailMessage emailData = new(email.To)
            {
                Content = email.Content,
                Subject = email.Subject
            };
            await _sendGrid.SendEmailAsync(emailData);
            return Ok(email);
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult> PostTemplate(ResetPasswordEventArgs email)
        {
            await _sendGrid.OnResetPassword(email);
            return Ok(email);
        }
    }
}
