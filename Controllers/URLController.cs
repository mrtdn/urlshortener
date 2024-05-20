using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Data;
using URLShortener.Models;
using URLShortener.Services;

namespace URLShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors]
    public class URLController : ControllerBase
    {
        private readonly URLShorteningService _urlShorteningService;
        private readonly URLRepository _urlRepository;

        public URLController(URLShorteningService urlShorteningService, URLRepository urlRepository)
        {
            _urlShorteningService = urlShorteningService;
            _urlRepository = urlRepository;
        }

        [HttpPost("shorten")]
        public IActionResult ShortenURL([FromBody] string originalURL)
        {
            if (!_urlShorteningService.IsValidURL(originalURL))
            {
                return BadRequest("Invalid URL format.");
            }

            string shortURL = _urlShorteningService.ShortenURL(originalURL);
            return Ok(new { ShortURL = shortURL });
        }



        [HttpGet("{shortURL}", Name = "RedirectToOriginalURL")]
        public IActionResult RedirectToOriginalURL(string shortURL)
        {
            var urlMapping = _urlRepository.GetURLMapping($"{shortURL}");
            if (urlMapping == null)
            {
                return NotFound();
            }

            return Redirect(urlMapping.OriginalURL);
        }
    }

}
