using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Global.Global
{
    ///<summary>
    /// Helper class that provides basic functionalities for sending mails.
    ///</summary>
    public class Mail : IDisposable
    {
        private readonly SmtpClient _smtpClient;

        private MailMessage _message;
        ///<summary>
        /// Defines the message settings such as from, to, subject, etc. These fields will be set automatically by calling the respective methods.
        ///</summary>
        public MailMessage Message
        {
            get { return _message ?? (_message = new MailMessage()); }
        }

        ///<summary>
        /// Creates a new instance of the Mail class.
        ///</summary>
        public Mail()
        {
            _smtpClient = new SmtpClient();
        }
        ///<summary>
        /// Creates a new instance of the Mail class.
        ///</summary>
        ///<param name="host">The host from where to send the mail from.</param>
        public Mail(string host)
        {
            _smtpClient = new SmtpClient(host);
        }
        ///<summary>
        /// Creates a new instance of the Mail class.
        ///</summary>
        ///<param name="host">The host from where to send the mail from.</param>
        ///<param name="port">The specified port.</param>
        public Mail(string host, int port)
        {
            _smtpClient = new SmtpClient(host, port);
        }

        ///<summary>
        /// Defines the from address.
        ///</summary>
        ///<param name="fromMail">The from mail address.</param>
        ///<returns>The same instance of Mail.</returns>
        public virtual Mail From(string fromMail)
        {
            Message.From = new MailAddress(fromMail);
            return this;
        }
        ///<summary>
        /// Defines the from address.
        ///</summary>
        ///<param name="fromDisplayName">The from display name.</param>
        ///<param name="fromMail">The from mail address.</param>
        ///<returns>The same instance of Mail.</returns>
        public virtual Mail From(string fromDisplayName, string fromMail)
        {
            Message.From = new MailAddress(fromMail, fromDisplayName);
            return this;
        }
        ///<summary>
        /// Adds a collection of mail addresses to send to.
        ///</summary>
        ///<param name="mailAddresses">The specified addresses to send to.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail To(Func<MailAddresses, MailAddresses> mailAddresses)
        {
            foreach (var address in mailAddresses(new MailAddresses()).AddressCollection)
                Message.To.Add(address);

            return this;
        }
        ///<summary>
        /// Adds a collection of mail addresses to send to.
        ///</summary>
        ///<param name="mailAddresses">The specified addresses to send to.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Cc(Func<MailAddresses, MailAddresses> mailAddresses)
        {
            foreach (var address in mailAddresses(new MailAddresses()).AddressCollection)
                Message.CC.Add(address);

            return this;
        }
        ///<summary>
        /// Adds a collection of mail addresses to send to.
        ///</summary>
        ///<param name="mailAddresses">The specified addresses to send to.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Bcc(Func<MailAddresses, MailAddresses> mailAddresses)
        {
            foreach (var address in mailAddresses(new MailAddresses()).AddressCollection)
                Message.Bcc.Add(address);

            return this;
        }
        ///<summary>
        /// Sets the message subject.
        ///</summary>
        ///<param name="subject">The subject.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Subject(string subject)
        {
            Message.Subject = subject;
            return this;
        }
        ///<summary>
        /// Sets the message body.
        ///</summary>
        ///<param name="body">The body.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Body(string body)
        {
            Message.Body = body;
            return this;
        }
        ///<summary>
        /// Sets the message subject encoding.
        ///</summary>
        ///<param name="subjectEncoding">The subject encoding.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail SubjectEncoding(Encoding subjectEncoding)
        {
            Message.SubjectEncoding = subjectEncoding;
            return this;
        }
        ///<summary>
        /// Sets the message body encoding.
        ///</summary>
        ///<param name="bodyEncoding">The body encoding.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail BodyEncoding(Encoding bodyEncoding)
        {
            Message.BodyEncoding = bodyEncoding;
            return this;
        }
        ///<summary>
        /// Sets the message body as html or not.
        ///</summary>
        ///<param name="isBodyHtml">Message body html flag.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail IsBodyHtml(bool isBodyHtml)
        {
            Message.IsBodyHtml = isBodyHtml;
            return this;
        }
        ///<summary>
        /// The host from where to send the mail from.
        ///</summary>
        ///<param name="host">The host from where to send the mail from.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Host(string host)
        {
            _smtpClient.Host = host;
            return this;
        }
        ///<summary>
        /// The specified port.
        ///</summary>
        ///<param name="port">The specified port.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Port(int port)
        {
            _smtpClient.Port = port;
            return this;
        }
        ///<summary>
        /// Specifies if ssl should be enabled.
        ///</summary>
        ///<param name="enableSsl">Flag to enable ssl.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Ssl(bool enableSsl)
        {
            _smtpClient.EnableSsl = enableSsl;
            return this;
        }
        ///<summary>
        /// Defines the credentials to use from for sending the mail (NetworkCredentials).
        ///</summary>
        ///<param name="username">The specified username.</param>
        ///<param name="password">The specified passord.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Credentials(string username, string password)
        {
            _smtpClient.Credentials = new NetworkCredential(username, password);
            return this;
        }
        ///<summary>
        /// Defines the credentials to use from for sending the mail (NetworkCredentials).
        ///</summary>
        ///<param name="username">The specified username.</param>
        ///<param name="password">The specified passord.</param>
        ///<param name="domain">The specified domain.</param>
        ///<returns>The same instance of the mail helper class.</returns>
        public virtual Mail Credentials(string username, string password, string domain)
        {
            _smtpClient.Credentials = new NetworkCredential(username, password, domain);
            return this;
        }

        /// <summary>
        /// Inserts an attachement in the mail.
        /// </summary>
        /// <param name="filePath">The path of the file to be attached.</param>
        /// <param name="type">The of file that is being sent. Check MediaTypeNames enum for available content types.</param>
        /// <returns>The same instance of the mail helper class.</returns>
        public virtual Mail Attachment(string filePath, string type)
        {
            var attachment = new Attachment(filePath, type);
            var disposition = attachment.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);
            Message.Attachments.Add(attachment);

            return this;
        }
        ///<summary>
        /// Sends the specified message defined.
        ///</summary>
        ///<returns>If no error was found it returns true.</returns>
        public virtual bool Send()
        {
            _smtpClient.Send(Message);
            return true;
        }

        /// <summary>
        /// Disposes the all the attachements if exist.
        /// </summary>
        public void Dispose()
        {
            if (Message != null && Message.Attachments.Count > 0)
            {
                foreach (var attachment in Message.Attachments)
                {
                    if(attachment != null) attachment.Dispose();
                }
            }
        }
    }
}
