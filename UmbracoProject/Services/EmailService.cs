using Azure;
using Azure.Communication.Email;

namespace UmbracoProject.Services;

public interface IEmailService
{
    Task<bool> SendOkMailToUser(string toEmail, string name, string selectedOption);
    Task<bool> QuestionFormOkMailToUser(string toEmail, string name, string messsage);
    Task<bool> ContactCardOkMailToUser(string toEmail);
}

public class EmailService(EmailClient emailClient) : IEmailService
{
    private readonly EmailClient _emailClient = emailClient;
    private const string sender = "DoNotReply@10871802-21d5-40a7-8fde-7ad220254d0e.azurecomm.net";

    public async Task<bool> SendOkMailToUser(string toEmail, string name, string selectedOption)
    {
        try
        {
            if (string.IsNullOrEmpty(toEmail))
                return false;

            var subject = "Form Request Received - Onatrix";
            var plain = @$"Dear {name},

                Thank you for your request regarding {selectedOption}. We have received your submission and will review it shortly.

                Best regards,
                The Onatrix Team";

            var html = @$"<p>Dear {name},</p>
                <p>Thank you for your request regarding <strong>{selectedOption}</strong>. We have received your submission and will review it shortly.</p>
                <p>Best regards, <br />The Onatrix Team</p>";

            var content = new EmailContent(subject)
            {
                PlainText = plain,
                Html = html
            };

            var message = new EmailMessage(
                senderAddress: sender,
                recipients: new EmailRecipients([new EmailAddress(toEmail)]),
                content: content
            );
            var successResponse = await _emailClient.SendAsync(WaitUntil.Started, message);

            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email to user: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> QuestionFormOkMailToUser(string toEmail, string name, string messsage)
    {
        try
        {
            if (string.IsNullOrEmpty(toEmail))
                return false;
            var subject = "We Received Your Question - Onatrix";
            var plain = @$"Dear {name},
                Thank you for reaching out with your question:{messsage}. We have received your inquiry and will get back to you shortly.
                Best regards,
                The Onatrix Team";
            var html = @$"<p>Dear {name},</p>
                <p>Thank you for reaching out with your question: </p>
                <br /><p><strong>{messsage}</strong><p><br />.
                <p>We have received your inquiry and will get back to you shortly.</p>
                <p>Best regards, <br />The Onatrix Team</p>";
            var content = new EmailContent(subject)
            {
                PlainText = plain,
                Html = html
            };
            var message = new EmailMessage(
                senderAddress: sender,
                recipients: new EmailRecipients([new EmailAddress(toEmail)]),
                content: content
            );
            var successResponse = await _emailClient.SendAsync(WaitUntil.Started, message);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email to user: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ContactCardOkMailToUser(string toEmail)
    {
        try
        {
            if (string.IsNullOrEmpty(toEmail))
                return false;

            var subject = "Contact Request Received - Onatrix";
            var plain = @$"Dear User,
                Thank you for contacting us. We have received your email address ( {toEmail} ) and will get back to you shortly.
                Best regards,
                The Onatrix Team";
            var html = @$"<p>Dear User,</p>
                <p>Thank you for contacting us. We have received your email address (<strong> {toEmail} </strong> )and will get back to you shortly.</p>
                <p>Best regards, <br />The Onatrix Team</p>";
            var content = new EmailContent(subject)
            {
                PlainText = plain,
                Html = html
            };
            var message = new EmailMessage(
                senderAddress: sender,
                recipients: new EmailRecipients([new EmailAddress(toEmail)]),
                content: content
            );
            var successResponse = await _emailClient.SendAsync(WaitUntil.Started, message);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email to user: {ex.Message}");
            return false;
        }
    }

}