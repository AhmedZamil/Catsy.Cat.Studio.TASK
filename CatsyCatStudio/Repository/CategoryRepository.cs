using CatsyCatStudio.Data;
using CatsyCatStudio.Interface;
using CatsyCatStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext AppDbContext)
        {
            _appDbContext = AppDbContext;
        }

        public IEnumerable<Category> AllCategories {
            get {
                return _appDbContext.Categories;
            }
        }
    }
}
