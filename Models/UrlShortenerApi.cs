namespace UrlShortenerApi.Models
{
    public class URLItem
    {
        public int Id { get; set; } // ID para o banco de dados (será gerado automaticamente)
        public string OriginalUrl { get; set; } = string.Empty; // Link original completo
        public string ShortCode { get; set; } = string.Empty; // Código curto gerado (ex: "abcde")
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Data de criação
    }

    public class ShortenRequest
    {
        public string Url { get; set; } = string.Empty; // Objeto para receber o link longo do frontend
    }
}