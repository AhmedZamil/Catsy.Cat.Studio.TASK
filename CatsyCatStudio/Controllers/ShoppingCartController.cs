using CatsyCatStudio.Interface;
using CatsyCatStudio.Models;
using CatsyCatStudio.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IPieRepository PieRepository,ShoppingCart ShoppingCart )
        {
            _pieRepository = PieRepository;
            _shoppingCart = ShoppingCart;
        }
        public IActionResult Index()
        {
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = shoppingCartItems;

            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel() { 
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal =  _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCart);
        }

        public RedirectToActionResult AddToCart(int pieId) {

            var pie = _pieRepository.AllPies.FirstOrDefault(p=>p.PieId==pieId);
            if (pie != null) {
                _shoppingCart.AddToCart(pie,1);
            }
            return RedirectToAction("Index");
        
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId) 
        {
            var pie = _pieRepository.AllPies.FirstOrDefault(p=>p.PieId==pieId);
            _shoppingCart.RemoveFromCart(pie);

            return RedirectToAction("Index");
        }
    }
}
