namespace SendFlow.Api.Dtos
{
    public sealed class SendSaleCompletedEmailDto
    {
        public string To { get; init; } = string.Empty;

        public string Nome { get; init; } = string.Empty;
        public string Referencia { get; init; } = string.Empty;
        public string Usuario { get; init; } = string.Empty;
        public string Servico { get; init; } = string.Empty;

        public decimal Valor { get; init; }
        public DateTime Data { get; init; } = DateTime.Now;

        public string LogoUrl { get; init; } = "https://via.placeholder.com/160x40?text=SendFlow";
        public string LinkUrl { get; init; } = "https://github.com/";
        public string LinkTexto { get; init; } = "Acessar detalhes";

        public string SiteUrl { get; init; } = "https://github.com/";
        public string SiteTexto { get; init; } = "https://github.com/";
    }

}