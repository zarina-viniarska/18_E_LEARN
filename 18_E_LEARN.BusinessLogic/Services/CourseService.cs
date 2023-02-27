using _18_E_LEARN.DataAccess.Data.IRepository;
using _18_E_LEARN.DataAccess.Data.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.BusinessLogic.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CourseService(ICourseRepository courseRepository, ICategoryRepository categoryRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            IEnumerable<Course> courses = await _courseRepository.GetAllAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "All courses loaded.",
                Payload = courses
            };
        }
    }
}
