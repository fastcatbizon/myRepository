using System;
using System.Net.Mail;


namespace MailEngine
{
    public class MailSender
    {

        private string _email;
        private string _password;
        private string _smtp;
        private int _port;

        public string Email
        {
            get { return _email; }
            set { _email = value; }

        }

         public string Password
        {
            get { return _password; }
            set { _password = value; }

        }

         public string Smtp
         {
             get { return _smtp; }
             set { _smtp = value; }

         }

         public int Port
         {
             get { return _port; }
             set { _port = value; }

         }

        public MailSender (string email, string password, string smtp, int port)
        {

            Email = email;
            Password = password;
            Smtp = smtp;
            Port = port;
        }

        public void SendAnEmail(string toEmail, string subject, string text)
        {
            
            var mail = new MailMessage(_email, toEmail, subject, text);
            var client = new SmtpClient(_smtp, _port)
            {
                Credentials = new System.Net.NetworkCredential(_email, _password),
                EnableSsl = true
            };
            client.Send(mail);


        }

    }
}
