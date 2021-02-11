using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniversityMVC.Models;

namespace UniversityMVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private IUnitOfWork UnitOfWork { get; set; }

        public CourseController(ILogger<CourseController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
        }
        public IActionResult SelectedCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.CourseName = $"{UnitOfWork.Courses.GetById(id).Name}";
            return View(UnitOfWork.Groups.GetByCourse(id));
        }
    }
}
