using CatsyCatStudio.Data;
using CatsyCatStudio.Interface;
using CatsyCatStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext AppDbContext,ShoppingCart ShoppingCart)
        {
            _context = AppDbContext;
            _shoppingCart = ShoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.UtcNow;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            var items = _shoppingCart.GetShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in items)
            {
                var orderDetail = new OrderDetail()
                {
                    PieId= item.Pie.PieId,
                    Price = item.Pie.Price,
                    Amount = item.Amount

                };
                order.OrderDetails.Add(orderDetail);
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
