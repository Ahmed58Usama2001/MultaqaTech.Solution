namespace MultaqaTech.Service;

public class MailService : IMaillingService
{
    private readonly IOptions<MailSettings> _mailSetting;
    private readonly ILoggerFactory _logger;

    public MailService(IOptions<MailSettings> mailSetting, ILoggerFactory logger)
    {
        _mailSetting = mailSetting;
        _logger = logger;
    }
    public async Task<bool> SendEmailAsync(string mailTo, string subject, string body)
    {
        try
        {
            using var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_mailSetting.Value.Email),
                Subject = subject
            };

            email.From.Add(new MailboxAddress(_mailSetting.Value.DisplayName, _mailSetting.Value.Email));

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();
            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSetting.Value.Host, _mailSetting.Value.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSetting.Value.Email, _mailSetting.Value.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return true;

        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return false;
        }
    }
}
