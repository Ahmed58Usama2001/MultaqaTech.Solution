namespace MultaqaTech.Service;

public class MailingService : IMaillingService
{
    private readonly IOptions<MailSettings> _mailSetting;
    private readonly IConfiguration _configuration ;


    public MailingService(IOptions<MailSettings> mailSetting, IConfiguration configuration)
    {
        _mailSetting = mailSetting;
        _configuration = configuration;
    }
    public async Task<bool> SendEmailAsync(string mailTo, string subject, string body)
    {
        try
        {
            using var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_configuration["MailSettings:Email"]),
                Subject = subject
            };

            email.From.Add(new MailboxAddress(_configuration["MailSettings:DisplayName"], _configuration["MailSettings:Email"]));

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();
            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["MailSettings:Host"], int.Parse(_configuration["MailSettings:Port"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["MailSettings:Email"], _configuration["MailSettings:Password"]);
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
