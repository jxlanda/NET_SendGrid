namespace NET_SendGrid.Models
{
    public class ResetPasswordEventArgs
    {
        public IEnumerable<string>? To { get; set; }
        public string? Email { get; set; }
        public string? CallbackUri { get; set; }
    }
}
