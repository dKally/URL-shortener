using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data;
using UrlShortenerApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortenerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly AppDbContext _context;
        private static readonly string _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly int _shortCodeLength = 7; // Tamanho do código curto

        public UrlController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para encurtar um URL (POST api/url/shorten)
        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Url) || !Uri.TryCreate(request.Url, UriKind.Absolute, out Uri? uriResult))
            {
                return BadRequest("URL inválida.");
            }

            // Verificar se o URL já foi encurtado para evitar duplicidade
            var existingUrl = await _context.Urls
                                            .FirstOrDefaultAsync(u => u.OriginalUrl == request.Url);

            if (existingUrl != null)
            {
                // Retorna o link encurtado existente
                return Ok(new { ShortenedUrl = $"{Request.Scheme}://{Request.Host}/{existingUrl.ShortCode}" });
            }

            string shortCode;
            do
            {
                shortCode = GenerateShortCode(_shortCodeLength);
            } while (await _context.Urls.AnyAsync(u => u.ShortCode == shortCode)); // Garante que o código é único

            var urlItem = new URLItem
            {
                OriginalUrl = request.Url,
                ShortCode = shortCode,
                CreatedDate = DateTime.UtcNow
            };

            _context.Urls.Add(urlItem);
            await _context.SaveChangesAsync();

            return Ok(new { ShortenedUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}" });
        }

        // Método auxiliar para gerar um código curto aleatório e único
        private string GenerateShortCode(int length)
        {
            var result = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                for (int i = 0; i < length; i++)
                {
                    result.Append(_chars[bytes[i] % _chars.Length]);
                }
            }
            return result.ToString();
        }
    }
}