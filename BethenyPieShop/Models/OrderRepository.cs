namespace BethenyPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BethenysPieShopDbContext _bethenysPieShopDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(BethenysPieShopDbContext bethenysPieShopDbContext, IShoppingCart shoppingCart)
        {
            _bethenysPieShopDbContext = bethenysPieShopDbContext;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _bethenysPieShopDbContext.Orders.Add(order);

            _bethenysPieShopDbContext.SaveChanges();
        }
    }
}
