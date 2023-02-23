using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    internal class Sample04
    {
        static void Main(string[] args)
        {
            new MailMassegeBuilder()
                .From("sample@gmail.com")
                .To("Gb@gmail.com")
                //.Subject("___")
                .Body("Privet")
                .Build();

            
        }
    }

    public class MailMassage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class MailMassegeBuilder
    {
        public readonly MailMassage _mailMassage= new MailMassage();

        public MailMassage Build()
        {
            if (string.IsNullOrEmpty(_mailMassage.To))
                throw new InvalidOperationException("Can't create massage");
            return _mailMassage;
        }
        public MailMassegeBuilder From (string address)
        {
            _mailMassage.From = address;
            return this;
        }
        public MailMassegeBuilder To(string address)
        {
            _mailMassage.From = address;
            return this;
        }
        public MailMassegeBuilder Subject(string address)
        {
            _mailMassage.From = address;
            return this;
        }
        public MailMassegeBuilder Body(string body)
        {
            _mailMassage.From = body;
            return this;
        }


    }


}
