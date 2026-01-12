using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SendFlow.Application.Interfaces;
using SendFlow.Domain.Models;
using SendFlow.Infrastructure.Settings;
using SendFlow.Domain;
using Microsoft.Extensions.Options;


namespace SendFlow.Infrastructure.Email;

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly SmtpSettings _settings;

    public SmtpEmailSender(IOptions<SmtpSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<EmailSendResult> SendAsync(
        EmailRequest request,
        CancellationToken ct = default)
    {
        try
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.IsHtml
            };

            message.To.Add(request.To);

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(
                    _settings.Username,
                    _settings.Password)
            };

            await client.SendMailAsync(message);

            return EmailSendResult.Ok();
        }
        catch (Exception ex)
        {
            return EmailSendResult.Fail(ex.Message);
        }
    }
}
