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
            _logger.LogInformation("ChangeStudent method was called with an id = {studentId}", id);
            return View(UnitOfWork.Students.GetById(id));
        }

        [HttpPost]
        public IActionResult ChangeStudent(string firstName, string lastName, int id)
        {
            UnitOfWork.Students.GetById(id).FirstName = firstName;
            UnitOfWork.Students.GetById(id).LastName = lastName;
            UnitOfWork.Save();
            _logger.LogInformation("Student's (id = {groupId}) first name was changed to {newFirstName}, last name was changed to {newLastName}", id, firstName, lastName);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("DeleteStudent method was called but got no id");
                return NotFound();
            }
            try
            {
                _logger.LogInformation("DeleteStudent method was called with an id = {studentId}", id);
                return View(UnitOfWork.Students.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred");
                return NotFound();
            }
        }

        [HttpPost, ActionName("DeleteStudent")]
        public IActionResult ConfirmDeleteStudent(int id)
        {
            try
            {
                _logger.LogInformation("Attempt to delete a student with an id = {studentId}", id);
                UnitOfWork.Students.Remove(UnitOfWork.Students.GetById(id));
                UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Attempt failed");
                ViewBag.Message = "Во время выполнения запроса произошла ошибка! Изменения не были внесены! ";
                return View("ReturnHomePage");
            }
            _logger.LogInformation("Student was successfully deleted");
            ViewBag.Message = "Студент был удалён";
            return View("ReturnHomePage");
        }
    }
}
