using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendFlow.Domain.Models;

namespace SendFlow.Application.Interfaces;

public interface IEmailSender
{
    Task<EmailSendResult> SendAsync(
        EmailRequest request,
        CancellationToken ct = default
    );
}