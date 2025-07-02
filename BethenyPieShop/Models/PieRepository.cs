using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace BethenyPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethenysPieShopDbContext _bethenysPieShopDbContext;

        public PieRepository(BethenysPieShopDbContext bethenysPieShopDbContext)
        {
            _bethenysPieShopDbContext = bethenysPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _bethenysPieShopDbContext.Pies.Include(c => c.Category);
            }
        }
        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bethenysPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }
        public Pie? GetPieById(int pieId)
        {
            return _bethenysPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _bethenysPieShopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }
    }
}
