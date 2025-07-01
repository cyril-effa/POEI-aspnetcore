using System.Net.Mail;

namespace ex10bis.Infrastructure.Services
{
    public class EmailService
    {
        public static void SendMailWithAttachment(string to, string subject, string body, byte[] attachment, string attachmentName)
        {
            using (var client = new SmtpClient("smtp.example.com", 587)) // Remplacez par votre serveur SMTP
            {
                client.Credentials = new System.Net.NetworkCredential("username", "password"); // Remplacez par vos identifiants
                client.EnableSsl = true;
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(""),
                    Subject = subject,
                    Body = body
                };
                mailMessage.To.Add(to);
                mailMessage.Attachments.Add(new Attachment(new MemoryStream(attachment), attachmentName, "application/pdf"));
                client.Send(mailMessage);
                client.Dispose();
            }
        }

        public static void SendFakeMailWithAttachment(string to, string subject, string body, byte[] attachment, string attachmentName)
        {
            // Simule l'envoi d'un email sans le faire réellement
            Console.WriteLine($"Email envoyé à : {to}");
            Console.WriteLine($"Sujet : {subject}");
            Console.WriteLine($"Corps : {body}");
            Console.WriteLine($"Pièce jointe : {attachmentName} ({attachment.Length} octets)");
        }
    }
}
