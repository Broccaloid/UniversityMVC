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

        public IActionResult SelectedCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.CourseName = $"{UnitOfWork.Courses.GetById(id).Name}";
            return View(UnitOfWork.Groups.GetByCourse(id));
        }

        public IActionResult SelectedGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
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

        [HttpGet]
        public IActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return View(UnitOfWork.Students.GetById(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("DeleteStudent")]
        public IActionResult ConfirmDeleteStudent(int id)
        {
            try
            {
                UnitOfWork.Students.Remove(UnitOfWork.Students.GetById(id));
                UnitOfWork.Save();
            }
            catch (Exception)
            {
                ViewBag.Message = "Во время выполнения запроса произошла ошибка! Изменения не были внесены! ";
                return View("ReturnHomePage");
            }
            ViewBag.Message = "Студент был удалён";
            return View("ReturnHomePage");
        }

        [HttpGet]
        public IActionResult DeleteGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return View(UnitOfWork.Groups.GetById(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("DeleteGroup")]
        public IActionResult ConfirmDeleteGroup(int id)
        {
            try
            {
                UnitOfWork.Groups.Remove(UnitOfWork.Groups.GetById(id));
                UnitOfWork.Save();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                ViewBag.Message = "Невозможно удалить группу, в которой есть студенты!";
                return View("ReturnHomePage");
            }
            catch (Exception)
            {
                ViewBag.Message = "Во время выполнения запроса произошла ошибка! Изменения не были внесены! ";
                return View("ReturnHomePage");
            }

            ViewBag.Message = "Группа была удалена!";
            return View("ReturnHomePage");
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
