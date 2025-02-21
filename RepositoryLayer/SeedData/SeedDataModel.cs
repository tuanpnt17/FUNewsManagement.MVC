using RepositoryLayer.Entities;

namespace RepositoryLayer.SeedData
{
    public class SeedDataModel
    {
        public List<SystemAccount>? SystemAccounts { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<NewsArticle>? NewsArticles { get; set; }
        public List<NewsTag>? NewsTags { get; set; }
    }
}
