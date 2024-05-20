using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using URLShortener.Data;
using URLShortener.Models;

namespace URLShortener.Services
{
    public class URLShorteningService
    {
        private readonly URLRepository _urlRepository;

        public URLShorteningService(URLRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public string ShortenURL(string originalURL)
        {
            // Check if the original URL already exists in the database
            var existingMapping = _urlRepository.GetURLMapping(originalURL);
            if (existingMapping != null)
            {
                return existingMapping.ShortURL;
            }

            string shortURL = GenerateShortURL(originalURL);
            var urlMapping = new URLMapping { ShortURL = shortURL, OriginalURL = originalURL };
            _urlRepository.AddURLMapping(urlMapping);
            return shortURL;
        }

        private string GenerateShortURL(string originalURL)
        {
            // Generate a unique hash based on the original URL
            string hash = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(originalURL)))
                .Replace("+", "")
                .Replace("/", "")
                .Substring(0, 4);

            return $"{hash}";
        }

        public bool IsValidURL(string url)
        {
            // Regular expression pattern for URL validation
            string pattern = @"^(http|https)://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,}(:[0-9]{1,5})?(/\S*)?$";

            bool isValidFormat = Regex.IsMatch(url, pattern);

            return (isValidFormat ? true : false);
        }

        public string CreateCustomShortURL(string originalURL, string customShortURL)
        {
            // Check if the custom short URL already exists in the database
            var existingMapping = _urlRepository.GetURLMapping(customShortURL);
            if (existingMapping != null)
            {
                throw new Exception("Custom short URL already exists.");
            }

            var urlMapping = new URLMapping { ShortURL = customShortURL, OriginalURL = originalURL };
            _urlRepository.AddURLMapping(urlMapping);
            return customShortURL;
        }
    }
}
