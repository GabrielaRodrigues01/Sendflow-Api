using Microsoft.AspNetCore.Mvc;
using SendFlow.Api.Dtos;
using SendFlow.Application.Interfaces;
using SendFlow.Domain.Models;
using SendFlow.Infrastructure.Email;

namespace SendFlow.Api.Controllers;

[ApiController]
[Route("api/emails")]
public sealed class EmailsController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly IWebHostEnvironment _env;

    public EmailsController(IEmailSender emailSender, IWebHostEnvironment env)
    {
        _emailSender = emailSender;
        _env = env;
    }

    [HttpPost("send-sale-completed")]
    public async Task<IActionResult> SendSaleCompleted([FromBody] SendSaleCompletedEmailDto dto, CancellationToken ct)
    {
        
        var templatePath = Path.Combine(_env.ContentRootPath, "..", "SendFlow.Infrastructure", "Email", "Templates", "SaleCompleted.html");

        if (!System.IO.File.Exists(templatePath))
            return BadRequest(new { message = $"Template não encontrado em: {templatePath}" });

        var templateHtml = await System.IO.File.ReadAllTextAsync(templatePath, ct);

        var html = TemplateRenderer.RenderSaleCompletedTemplate(
            templateHtml: templateHtml,
            nome: dto.Nome,
            referencia: dto.Referencia,
            data: dto.Data,
            usuario: dto.Usuario,
            servico: dto.Servico,
            valor: dto.Valor,
            logoUrl: dto.LogoUrl,
            linkUrl: dto.LinkUrl,
            linkTexto: dto.LinkTexto,
            siteUrl: dto.SiteUrl,
            siteTexto: dto.SiteTexto
        );

        var request = new EmailRequest
        {
            To = dto.To,
            Subject = "Notificação SendFlow",
            Body = html,
            IsHtml = true
        };

        var result = await _emailSender.SendAsync(request, ct);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "E-mail enviado com sucesso." });
    }
}
