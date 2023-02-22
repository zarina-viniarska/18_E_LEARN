using _18_E_LEARN.DataAccess.Data.Context;
using _18_E_LEARN.DataAccess.Data.IRepository;
using _18_E_LEARN.DataAccess.Data.Models.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<Category> GetByIdAsync(int id)
        {
            using (var _context = new AppDbContext())
            {
                Category result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                return result;
            }
        }

        public string UpdateCategory(Category category)
        {
            using (var _context = new AppDbContext())
            {
                var result =  _context.Categories.Update(category);
                _context.SaveChanges();
                return result.State.ToString();
            }
        }
    }
}
