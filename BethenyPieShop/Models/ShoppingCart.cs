using Microsoft.EntityFrameworkCore;

namespace BethenyPieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly BethenysPieShopDbContext _bethenysPieShopDbContext;

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private ShoppingCart(BethenysPieShopDbContext bethenysPieShopDbContext)
        {
            _bethenysPieShopDbContext = bethenysPieShopDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            BethenysPieShopDbContext context = services.GetService<BethenysPieShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem =
                    _bethenysPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _bethenysPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethenysPieShopDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _bethenysPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _bethenysPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _bethenysPieShopDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _bethenysPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _bethenysPieShopDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bethenysPieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _bethenysPieShopDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _bethenysPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}

