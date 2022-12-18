using CatsyCatStudio.Data;
using CatsyCatStudio.Interface;
using CatsyCatStudio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Repository
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICategoryRepository _categoryRepository;

        public PieRepository(AppDbContext AppDbContext,ICategoryRepository CategoryRepository)
        {
            _appDbContext = AppDbContext;
            _categoryRepository = CategoryRepository;
        }
        public IEnumerable<Pie> AllPies {
            get {
                return _appDbContext.Pies.Include(c=>c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek {
            get {
                return _appDbContext.Pies.Include(c => c.Category).Where(p=>p.IsPieOfTheWeek == true);
            }
        }

        public Pie PieById(int pieId) {

            return _appDbContext.Pies.Include(c => c.Category).FirstOrDefault(p=>p.PieId== pieId);
        
        }
    }
}
