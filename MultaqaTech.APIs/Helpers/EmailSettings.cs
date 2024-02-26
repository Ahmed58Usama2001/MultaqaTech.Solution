namespace MultaqaTech.APIs.Helpers;

public static class EmailSettings
{
    public static void SendEmail(Email email)
    {
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential("multaqatech3@gmail.com", "fojtfxghywlyxqvy");
        client.Send("multaqatech3@gmail.com", email.To, email.Title, email.Body);
    }
}
