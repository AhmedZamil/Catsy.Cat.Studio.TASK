using CatsyCatStudio.Interface;
using CatsyCatStudio.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IPieRepository PieRepository,ICategoryRepository CategoryRepository)
        {
            _pieRepository = PieRepository;
            _categoryRepository = CategoryRepository;
        }
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.PiesOfTheWeek = _pieRepository.PiesOfTheWeek;

            return View(model);
        }
    }
}
