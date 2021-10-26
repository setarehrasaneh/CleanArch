using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Mvc.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseServise;

        public CourseController(ICourseService courseServise)
        {
            _courseServise = courseServise;
        }
        public IActionResult Index()
        {
            CourseViewModel model = _courseServise.GetCourses();
            return View(model);
        }

        public IActionResult ShowCourse(int id)
        {
            Course course = _courseServise.GetCourseById(id);
            if(course == null)
            {
                return NotFound();
            }
            return View(course);
        }

    }
}
