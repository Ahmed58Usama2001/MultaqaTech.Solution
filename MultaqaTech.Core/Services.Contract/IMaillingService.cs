namespace MultaqaTech.Core.Services.Contract;

public interface IMaillingService
{
    Task<bool> SendEmailAsync(string mailTo, string subject, string body);
}
