namespace PetAdopt.Infrastructure.External;

public class EmailService
{
    private readonly string _server;
    private readonly int _port;
    private readonly string _user;
    private readonly string _pass;

    public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
    {
        _server = smtpServer;
        _port = smtpPort;
        _user = smtpUser;
        _pass = smtpPass;
    }

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            using (var smtp = new SmtpClient(_server, _port))
            {
                smtp.Credentials = new NetworkCredential(_user, _pass);
                smtp.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress(_user);
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                await smtp.SendMailAsync(message);
                return true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}