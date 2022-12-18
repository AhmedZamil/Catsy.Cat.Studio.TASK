using CatsyCatStudio.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext AppDbContext)
        {
            _context = AppDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services) {

            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var _ctx = services.GetService<AppDbContext>();

            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId",cartId);

            return new ShoppingCart(_ctx) { ShoppingCartId  = cartId};
        }

        public void AddToCart(Pie pie,int amount) {

            var shoppingCartItems = _context.ShoppingCartItems.SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItems == null)
            {

                ShoppingCartItem item = new ShoppingCartItem()
                {
                    Pie = pie,
                    ShoppingCartId = ShoppingCartId,
                    Amount = amount
                };

                _context.ShoppingCartItems.Add(item);

            }
            else
            {
                shoppingCartItems.Amount++;
            }

            _context.SaveChanges();       
        }

        public void RemoveFromCart(Pie pie) {

            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(sp => sp.Pie.PieId == pie.PieId && sp.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                shoppingCartItem.Amount--;

            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
          
            }

            _context.SaveChanges();
        }

        public void ClearCart() {
            var shoppingCartItems = _context.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId);
            
            if (shoppingCartItems != null) {

                _context.ShoppingCartItems.RemoveRange(shoppingCartItems);
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems() {
            return ShoppingCartItems??(ShoppingCartItems= _context.ShoppingCartItems
                .Where(s=>s.ShoppingCartId== ShoppingCartId)
                .Include(p=>p.Pie)
                .ToList());
        }

        public decimal GetShoppingCartTotal() {
            var total = _context.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(p => p.Pie.Price * p.Amount)
                .Sum();
            return total;
        }
    }
}
