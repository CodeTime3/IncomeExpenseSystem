using IncomeExpenseSystemDomain.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace IncomeExpenseSystemApplication;

public class SendEmailConfirmService
{
    public void SendEmail(EmailModel model, string userMail, string token)
    {
        var email = CreateEmail(model.UserEmail, model.EmailUsername, token);

        var smpt = new SmtpClient();
        smpt.Connect(model.EmailHost, model.EmailPort, SecureSocketOptions.StartTls);
        smpt.Authenticate(model.EmailUsername, model.EmailPassword);
        smpt.Send(email);
        smpt.Disconnect(true);
    }

    private MimeMessage CreateEmail(string userMail, string emailUsername, string token)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailUsername));
        email.To.Add(MailboxAddress.Parse(userMail));
        email.Subject = "Confirm your email";
        email.Body = new TextPart(TextFormat.Html)
            { Text = $"<a href=\"http://localhost:5021/api/account/conferma-email?token={token}\"> Click here to confirm your email </a>" };

        return email;
    }
}