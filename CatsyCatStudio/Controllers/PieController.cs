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
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository PieRepository ,ICategoryRepository CategoryRepository)
        {
            _pieRepository = PieRepository;
            _categoryRepository = CategoryRepository;
        }

        public IActionResult List(string category)
        {
            IEnumerable<Pie> Pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                Pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All Pies";
            }
            else
            {
                Pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category).OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            PieViewModel model = new PieViewModel() { 
                Pies=Pies,
                CurrentCategory = currentCategory
            };

            return View(model);
        }

        public IActionResult Shop() {

            return View();
        }
        //public IActionResult List() {

        //    PieViewModel model = new PieViewModel();
        //    model.Pies = _pieRepository.AllPies;
        //    model.CurrentCategory = "Cheese Pies";
        //    return View(model);

        //}
    }
}
