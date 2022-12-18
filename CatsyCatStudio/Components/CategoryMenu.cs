using CatsyCatStudio.Interface;
using CatsyCatStudio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ShoppingCart ShoppingCart,ICategoryRepository CategoryRepository)
        {
            _shoppingCart = ShoppingCart;
            _categoryRepository = CategoryRepository;
        }

        public IViewComponentResult Invoke() 
        {
            var categories = _categoryRepository.AllCategories.OrderBy(c=>c.CategoryName);
            return View(categories);
        }
    }
}
