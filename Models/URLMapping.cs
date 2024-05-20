namespace URLShortener.Models
{
    public class URLMapping
    {
        public int Id { get; set; }
        public string ShortURL { get; set; }
        public string OriginalURL { get; set; }
    }
}
