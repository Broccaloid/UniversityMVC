using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniversityMVC.Models;

namespace UniversityMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork UnitOfWork { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(UnitOfWork.Courses.GetAll());
        }

        public IActionResult SelectedCourse(int id)
        {
            ViewBag.CourseName = $"{UnitOfWork.Courses.GetById(id).Name}";
            return View(UnitOfWork.Groups.GetByCourse(id));
        }

        public IActionResult SelectedGroup(int id)
        {
            ViewBag.GroupName = $"{UnitOfWork.Groups.GetById(id).Name}";
            return View(UnitOfWork.Students.GetByGroup(id));
        }

        [HttpGet]
        public IActionResult ChangeGroup(int id)
        {
            return View(UnitOfWork.Groups.GetById(id));
        }

        [HttpPost]
        public IActionResult ChangeGroup(string name, int id)
        {
            UnitOfWork.Groups.GetById(id).Name = name;
            UnitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChangeStudent(int id)
        {
            return View(UnitOfWork.Students.GetById(id));
        }

        [HttpPost]
        public IActionResult ChangeStudent(string firstName, string lastName, int id)
        {
            UnitOfWork.Students.GetById(id).FirstName = firstName;
            UnitOfWork.Students.GetById(id).LastName = lastName;
            UnitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
