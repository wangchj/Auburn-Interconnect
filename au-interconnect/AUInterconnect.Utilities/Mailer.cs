using System;
using System.Net.Mail;

namespace AUInterconnect.Utilities
{
    public class Mailer
    {
        public const string DefaultServer = "tigerout.auburn.edu";

        /// <summary>
        /// Sends an email using default server and sender email address.
        /// </summary>
        /// <param name="receiverAddr">Email address of the receiver.</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email content</param>
        public static void Send(string receiverAddr, string subject, string message)
        {
            Send(DefaultServer, "vininlj@auburn.edu", receiverAddr, subject, message);
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="serverName">SMTP server host name.</param>
        /// <param name="senderAddr">Email address of the sender.</param>
        /// <param name="receiverAddr">Email address of the receiver</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="message">Email content.</param>
        public static void Send(string serverName, string senderAddr,
            string receiverAddr, string subject, string message)
        {
            SmtpClient client = new SmtpClient(serverName);
            MailMessage mail = new MailMessage(new MailAddress(senderAddr),
                new MailAddress(receiverAddr));
            mail.Body = message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            client.Send(mail);
        }
    }
}
