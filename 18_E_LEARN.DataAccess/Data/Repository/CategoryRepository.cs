using _18_E_LEARN.DataAccess.Data.Context;
using _18_E_LEARN.DataAccess.Data.IRepository;
using _18_E_LEARN.DataAccess.Data.Models.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.DataAccess.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<List<Category>> GetAllAsync()
        {
            using (var _context = new AppDbContext())
            {
                List<Category> categories = await _context.Categories.ToListAsync();
                return categories;
            }
        }
    }
}
