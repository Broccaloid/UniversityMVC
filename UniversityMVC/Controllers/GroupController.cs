using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniversityMVC.Models;

namespace UniversityMVC.Controllers
{
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private IUnitOfWork UnitOfWork { get; set; }

        public GroupController(ILogger<GroupController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
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
            return RedirectToAction("Index", "Home");
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
    }
}
