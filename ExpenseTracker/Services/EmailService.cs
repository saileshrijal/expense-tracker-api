using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Config;
using ExpenseTracker.Services.Interface;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ExpenseTracker.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptionsMonitor<EmailConfig> optionmonitor)
        {
            _emailConfig = optionmonitor.CurrentValue;
        }
        public Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailConfig.From));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = content
            };
            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
            client.Authenticate(_emailConfig.Username, _emailConfig.Password);
            client.Send(message);
            client.Disconnect(true);
            return Task.CompletedTask;
        }
    }
}