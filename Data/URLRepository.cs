using URLShortener.Models;

namespace URLShortener.Data
{
    public class URLRepository
    {
        private readonly URLDBContext _dbContext;

        public URLRepository(URLDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddURLMapping(URLMapping urlMapping)
        {
            _dbContext.URLMappings.Add(urlMapping);
            _dbContext.SaveChanges();
        }

        public URLMapping GetURLMapping(string shortURL)
        {
            return _dbContext.URLMappings.FirstOrDefault(u => u.ShortURL == shortURL);
        }
    }

}
