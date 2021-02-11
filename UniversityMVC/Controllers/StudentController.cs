using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniversityMVC.Models;

namespace UniversityMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private IUnitOfWork UnitOfWork { get; set; }

        public StudentController(ILogger<StudentController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
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
            return RedirectToAction("Index", "Home");
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
    }
}
