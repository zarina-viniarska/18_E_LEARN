using _18_E_LEARN.DataAccess.Data.IRepository;
using _18_E_LEARN.DataAccess.Data.Models.Courses;
using _18_E_LEARN.DataAccess.Data.ViewModels.Course;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public CourseService(IConfiguration configuration, IHostingEnvironment hostEnvironment, ICourseRepository courseRepository, ICategoryRepository categoryRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
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

        public async Task<ServiceResponse> Create(AddCourseVM model)
        {
            string webPath = _hostEnvironment.WebRootPath;
            if(model.Files != null)
            {
                var files = model.Files;
                string upload = webPath + Settings.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                model.Image = fileName + extension;
            }

        }
    }
}
