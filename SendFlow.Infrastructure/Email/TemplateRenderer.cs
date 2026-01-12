using System.Globalization;

namespace SendFlow.Infrastructure.Email;

public static class TemplateRenderer
{
    public static string RenderSaleCompletedTemplate(
        string templateHtml,
        string nome,
        string referencia,
        DateTime data,
        string usuario,
        string servico,
        decimal valor,
        string logoUrl,
        string linkUrl,
        string linkTexto,
        string siteUrl,
        string siteTexto)
    {
        return templateHtml
            .Replace("#TITULO", "Notificação SendFlow")
            .Replace("#NOME", Escape(nome))
            .Replace("#REFERENCIA", Escape(referencia))
            .Replace("#DATA", data.ToString("dd/MM/yyyy HH:mm", CultureInfo.GetCultureInfo("pt-BR")))
            .Replace("#USUARIO", Escape(usuario))
            .Replace("#SERVICO", Escape(servico))
            .Replace("#VALOR", valor.ToString("C", CultureInfo.GetCultureInfo("pt-BR")))
            .Replace("#LOGO_URL", logoUrl)
            .Replace("#LINK_URL", linkUrl)
            .Replace("#LINK_TEXTO", Escape(linkTexto))
            .Replace("#SITE_URL", siteUrl)
            .Replace("#SITE_TEXTO", Escape(siteTexto))
            .Replace("#ANO", DateTime.Now.Year.ToString());
    }

    private static string Escape(string input)
        => System.Net.WebUtility.HtmlEncode(input ?? string.Empty);
}
