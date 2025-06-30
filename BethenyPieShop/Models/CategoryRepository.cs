namespace BethenyPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethenysPieShopDbContext _bethenysPieShopDbContext;

        public CategoryRepository(BethenysPieShopDbContext bethenysPieShopDbContext)
        {
            _bethenysPieShopDbContext = bethenysPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _bethenysPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
